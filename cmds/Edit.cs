using System.Text;
using Telegram.Bot;

class Edit : ICommand
{
    public string Name => "Edit File";

    public string ExecutionPhrase => "edit";

    public string Help => "Edits file";
    public string Usage => "edit [file name] [content]";
    public bool DevCommand => false;

    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added file
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter a file name to edit", chatId, botClient);
            return;
        }

        // checks if file exists
        string fn = str.Split(' ')[1];
        if(!File.Exists(fn)) {
            await Processor.SendMessage($"File {fn} doesnt exist", chatId, botClient);
            return;
        }

        StringBuilder sb = new StringBuilder();
        int strl = str.Split(' ').Length;
        for(int i = 0; i < strl; i++) {
            if(i > 1) {
                if(i != strl - 1)
                    sb.Append(str.Split(' ')[i] + " ");
                else
                    sb.Append(str.Split(' ')[i]);
            }
        }

        string content = sb.ToString();
        File.WriteAllText(fn, content);
        
        await Processor.SendMessage($"Wrote \"{content}\" to {fn}", chatId, botClient);
        Console.WriteLine($"{chatId} wrote {content} to {fn}");
    }
}
