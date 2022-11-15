using System.Text;
using Telegram.Bot;

class ChangeDirectory : ICommand
{
    public string Name => "Change Directory";

    public string ExecutionPhrase => "cd";

    public string Help => "Changes current directory";

    public string Usage => "cd [directory / ..]";

    public bool DevCommand => false;

    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        string[] strsp = str.Split(' ');

        // checks if user added directory
        if(strsp.Length <= 1) {
            await Processor.SendMessage("Enter a directory", chatId, botClient);
            return;
        }

        // checks if arg is ..
        if(strsp[1] == "..") {
            string[] cdsp = Data.cdir.Split('/');
            string ndir = string.Empty;
            for(int i = 0; i < cdsp.Length - 1; i++) {
                if(i != cdsp.Length - 2)
                    ndir += $"{cdsp[i]}/";
            }

            Data.cdir = ndir;
            await Processor.SendMessage($"Changed current directory to {ndir}", chatId, botClient);

            return;
        }

        // gets everything after cd
        StringBuilder sb = new StringBuilder();
        int strl = strsp.Length;
        for(int i = 0 ; i < strl; i++) {
            if(i > 0) {
                if(i != strl - 1)
                    sb.Append(strsp[i] + " ");
                else 
                    sb.Append(strsp[i]);
            }
        }

        string dir = sb.ToString();
        if(Directory.Exists(dir)) {
            Data.cdir = dir;
            await Processor.SendMessage($"Changed current directory to {dir}", chatId, botClient);

            return;
        }

        if(!Directory.Exists($"{Data.cdir}/{dir}")) {
            Data.cdir = dir;
            await Processor.SendMessage($"Directory {dir} doesnt exist", chatId, botClient);

            return;
        }

        Data.cdir = $"{Data.cdir}/{dir}";
        await Processor.SendMessage($"Changed current directory to {Data.cdir}", chatId, botClient);
    }
}
