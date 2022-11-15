using Telegram.Bot;
using CliWrap;
using System.Text;
using System.Diagnostics;

class Terminal : ICommand
{
    public string Name => "Terminal Execute";

    public string ExecutionPhrase => "t";

    public string Help => "Executes command in computers terminal";

    public string Usage => "t [command]";
    public bool DevCommand => true;


    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added command
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter a command to execute", chatId, botClient);
            return;
        }

        // gets everything after -t
        StringBuilder sb = new StringBuilder();
        int strl = str.Split(' ').Length;
        for(int i = 0; i < strl; i++) {
            if(i > 0) {
                if(i != strl - 1)
                    sb.Append(str.Split(' ')[i] + " ");
                else
                    sb.Append(str.Split(' ')[i]);
            }
        }

        string sbs = sb.ToString();
        Process p = new Process();
        ProcessStartInfo pi = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{sbs}\"",
            CreateNoWindow = true,
        };

        p.StartInfo = pi;
        p.Start();

        Console.WriteLine($"{chatId} executed {sbs} in terminal");
    }
}
