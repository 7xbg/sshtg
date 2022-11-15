using Telegram.Bot;

class List : ICommand
{
    public string Name => "List";

    public string ExecutionPhrase => "list";

    public string Help => "Lists all commands";
    public string Usage => "list";
    public bool DevCommand => false;


    public async Task Execute(string str, long chatId,  ITelegramBotClient botClient)
    {
        string s = "Commands:\n\n";
        foreach(var c in CommandHandler.GetCommands()) {
            // wont print if command is dev only and user isnt dev
            if(c.DevCommand) {
                if(!Data.isDev(chatId))
                    return;
            }

            s += $"{c.Name}, Usage: {Data.prefix}{c.Usage}\n ^ {c.Help}\n\n";
        }   

        await Processor.SendMessage(s, chatId, botClient);
    }
}
