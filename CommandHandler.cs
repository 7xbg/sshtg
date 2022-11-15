class CommandHandler {
    private static List<ICommand> cmds = new List<ICommand>() {
        new ChangeDirectory(),
        new RemoveFile(),
        new Whitelist(),
        new ListFiles(),
        new MakeFile(),
        new Terminal(),
        new Session(),
        new Read(),
        new List(),
        new Edit()
    }; // list of commands

    public static List<ICommand> GetCommands() { return cmds; }
}

// TODO: make sessions where you can type from the console to telegram and vise versa