using Telegram.Bot;

class ListFiles : ICommand
{
    public string Name => "List files";

    public string ExecutionPhrase => "ls";

    public string Help => "Lists all files & directories in current directory";

    public string Usage => "ls";
    public bool DevCommand => false;


    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        string s = $"Directories in {Data.cdir}:\n\n";
        int dc = Directory.GetDirectories(Data.cdir).Length; // directory count
        string[] dna = Directory.GetFiles(Data.cdir); // directory name array

        for(int i = 0; i < dc; i++) {
            if(i != dc - 1)
                s += "/" + dna[i].Split('/').Last() + "\n";
            else
                s += "/" + dna[i].Split('/').Last();
        }

        s += $"\n\nFiles in {Data.cdir}:\n\n";
        int fc = Directory.GetFiles(Data.cdir).Length; // file count
        string[] lns = Directory.GetFiles(Data.cdir); // file name array

        for(int i = 0; i < fc; i++) {
            if(i != fc - 1)
                s += lns[i].Split('/').Last() + "\n";
            else
                s += lns[i].Split('/').Last();
        }

        await Processor.SendMessage(s, chatId, botClient);
    }
}
