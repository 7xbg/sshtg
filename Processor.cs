using Telegram.Bot;
using Telegram.Bot.Types;

static class Processor {
    public static void LoadCommands() {
        Data.simpresp.Add(new Tuple<string, string, List<object>>(
            "/start", "resp/start", new List<object>() {
                Data.prefix, Data.cdir
            }
        ));
    }

    public static async Task ProcessMsg(string msg, long chatId, CancellationToken cancellationToken, ITelegramBotClient botClient) {
        // checks if user is in session
        if(Data.inSession) {
            if(!msg.StartsWith("-s stop")) {
                Console.WriteLine($"{chatId}: {msg}");
                return;
            }
        }

        // checks if message is start message
        if(msg == "/start") {
            foreach(var t in Data.simpresp) {
                if(t.Item1 == "/start") {
                    await SendMessage(FormatStr(System.IO.File.ReadAllText(t.Item2), t.Item3), chatId, botClient);
                    return;
                }
            }
        }

        // exits if message isnt a command
        if(!msg.StartsWith(Data.prefix)) return;
        foreach(var c in CommandHandler.GetCommands()) {
            if(msg.Split(' ')[0] == Data.prefix.ToString()) {
                await SendMessage("Enter a valid command", chatId, botClient);
                return;
            }

            string cmd = msg.Split(Data.prefix)[1];
            if(cmd.Split(' ')[0] == c.ExecutionPhrase) {
                // checks if user has permission to use command
                if(Data.useDevWhitelist) {
                    if(c.DevCommand) {
                        if(Data.isDev(chatId)) {
                            await c.Execute(msg, chatId, botClient);
                            return;
                        }
                        else {
                            await SendMessage("You dont have permission to use this command", chatId, botClient);
                            return;
                        }
                    }
                }

                await c.Execute(msg, chatId, botClient);
                return;
            }
        }

        await SendMessage($"Command {msg.Split(' ')[0]} cannot be found", chatId, botClient);
        return;
    }

    private static string FormatStr(string str, List<object> strs) {
        return string.Format(str, strs.Select(s => s.ToString()).ToArray());
    }

    public static async Task SendMessage(string msg, long chatId, ITelegramBotClient botClient) {
        await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: msg);
    }
}