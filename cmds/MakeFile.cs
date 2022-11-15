using Telegram.Bot;

class MakeFile : ICommand
{
    public string Name => "Create file";

    public string ExecutionPhrase => "mf";

    public string Help => "Creates file in current directory";

    public string Usage => "mf [file name]";
    public bool DevCommand => false;


    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added file
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter a file name to create", chatId, botClient);
            return;
        }

        string fn = str.Split(' ')[1];
        FileStream fs = File.Create(fn);
        fs.Close();

        await Processor.SendMessage($"Created file {fn}", chatId, botClient);
        Console.WriteLine($"{chatId} created file {fn}");
    }
}
