using Telegram.Bot;

interface ICommand {
    public string Name { get; }
    public string ExecutionPhrase { get; }
    public string Help { get; }
    public string Usage { get; }
    public bool DevCommand { get; }
    public Task Execute(string str, long chatId, ITelegramBotClient botClient);
}