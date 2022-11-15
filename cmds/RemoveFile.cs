using Telegram.Bot;

class RemoveFile : ICommand
{
    public string Name => "Delete file";

    public string ExecutionPhrase => "rf";

    public string Help => "Deletes file in current directory";

    public string Usage => "rf [file name]";
    public bool DevCommand => false;


    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added file
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter a file name to delete", chatId, botClient);
            return;
        }

        string fn = str.Split(' ')[1];
        File.Delete(fn);

        await Processor.SendMessage($"Deleted file {fn}", chatId, botClient);
        Console.WriteLine($"{chatId} deleted file {fn}");
    }
}