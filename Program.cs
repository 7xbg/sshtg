using System.Security.AccessControl;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

Processor.LoadCommands();
#region TG Stuff

string token = System.IO.File.ReadAllText("token");
ITelegramBotClient botClient = new TelegramBotClient(token);
var receiverOptions = new ReceiverOptions {
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(updateHandler: HandleUpdateAsync, pollingErrorHandler: HandlePollingErrorAsync, receiverOptions: receiverOptions);
var me = await botClient.GetMeAsync();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    // checks if user id is in file
    bool inf = false;
    foreach(string s in System.IO.File.ReadAllLines(Data.idfn)) {
        if(s == chatId.ToString()) {
            inf = true;
            break;
        }
    }

    if(!inf) {
        List<string> fc = System.IO.File.ReadAllLines(Data.idfn).ToList();
        fc.Add(chatId.ToString());
        System.IO.File.WriteAllLines(Data.idfn, fc);
        Console.WriteLine($"Added {chatId} to list");
    }

    if(Data.useWhitelist) {
        foreach(string s in System.IO.File.ReadAllLines(Data.wlfn)) {
            if(s == chatId.ToString()) {
                await Processor.ProcessMsg(messageText, chatId, cancellationToken, botClient);
                return;
            }
        }
    }
    else {
        await Processor.ProcessMsg(messageText, chatId, cancellationToken, botClient);
        return;
    }

    await Processor.SendMessage("You cannot use this because you arent whitelisted 🤷‍♂️", chatId, botClient);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

#endregion

Console.WriteLine($"@{me.Username} is online!");
await AnnounceMsg("SSH Bot is online!", botClient);
while(true) {
    if(Data.inSession) {
        string inp = Console.ReadLine();
        await Processor.SendMessage(inp, Data.Session.chatId, botClient);
    }
}

static async Task AnnounceMsg(string msg, ITelegramBotClient botClient) {
    foreach(string s in System.IO.File.ReadAllLines(Data.idfn)) {
        await botClient.SendTextMessageAsync(
            chatId: long.Parse(s),
            text: msg
        );
    }
}

//cts.Cancel();