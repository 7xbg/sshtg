using Telegram.Bot;

class Read : ICommand
{
    public string Name => "Read File";

    public string ExecutionPhrase => "read";

    public string Help => "Reads file contents";

    public string Usage => "read [file name]";
    public bool DevCommand => false;


    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added file
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter a file name to read", chatId, botClient);
            return;
        }

        string fn = str.Split(' ')[1];
        await Processor.SendMessage(File.ReadAllText(fn), chatId, botClient);
    }
}
