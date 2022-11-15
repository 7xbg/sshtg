using Telegram.Bot;

class Whitelist : ICommand
{
    public string Name => "Whitelist";

    public string ExecutionPhrase => "wl";

    public string Help => "[toggle] toggles whitelist and [list] lists all user ids";

    public string Usage => "wl [toggle / list]";

    public bool DevCommand => true;

    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added argument
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter an argument, [toggle / list]", chatId, botClient);
            return;
        }

        string arg = str.Split(' ')[1];
        if(arg == "toggle") {
            Data.useWhitelist = !Data.useWhitelist;
            await Processor.SendMessage($"useWhitelist is now {Data.useWhitelist}", chatId, botClient);
        }
        else if(arg == "list") {
            string s = "Users on whitelist:\n\n";
            foreach(string st in File.ReadAllLines(Data.wlfn)) {
                s += st + "\n";
            }

            await Processor.SendMessage(s, chatId, botClient);
        }
        else {
            await Processor.SendMessage("Enter valid argument, [toggle / list]", chatId, botClient);
        }
    }
}