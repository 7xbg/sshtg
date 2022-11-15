using Telegram.Bot;

class Session : ICommand
{
    public string Name => "Session";

    public string ExecutionPhrase => "s";

    public string Help => "Starts chat session between user and console";

    public string Usage => "s [start / stop]";

    public bool DevCommand => false;

    public async Task Execute(string str, long chatId, ITelegramBotClient botClient)
    {
        // checks if user added argument
        if(str.Split(' ').Length <= 1) {
            await Processor.SendMessage("Enter an argument, [start / stop]", chatId, botClient);
            return;
        }

        string arg = str.Split(' ')[1];
        if(arg == "start") {
            // checks if session is already started
            if(Data.inSession) {
                await Processor.SendMessage("Terminal is already in session", chatId, botClient);
                return;
            }

            Data.inSession = true;
            Console.WriteLine($"Session started with {chatId}");
            await Processor.SendMessage("Started session with terminal", chatId, botClient);

            // updates session data
            Data.Session.chatId = chatId;
        }
        else if(arg == "stop") {
            // checks if terminal is in session
            if(!Data.inSession) {
                await Processor.SendMessage("Terminal isnt in session", chatId, botClient);
                return;
            }

            Data.inSession = false;
            Console.WriteLine($"Session with {chatId} ended");
            await Processor.SendMessage("Session ended with terminal", chatId, botClient);

            // resets session data
            Data.Session.chatId = 0;
        }
    }
}
