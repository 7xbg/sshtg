static class Data {
    public static class Session {
        public static long chatId;
    }
    public static List<Tuple<string, string, List<object>>> simpresp = new List<Tuple<string, string, List<object>>>(); // list of commands and file data to respond with and a list of strings to put in while formating
    public static string cdir = Directory.GetCurrentDirectory(); // current directory bot is editing
    public static string wlfn = "whitelist"; // whitelist file name
    public static string idfn = "ids"; // id list file name
    public static string dlfn = "devs"; // dev list file name
    public static char prefix = '-'; // prefix for commands
    public static bool useWhitelist = true; // bool for if only specific users can use the bot
    public static bool useDevWhitelist = true; // only devs can execute dev commands
    public static bool inSession = false;
    public static bool isDev(long userId) {
        foreach(string s in File.ReadAllLines(dlfn)) {
            if(s == userId.ToString())
                return true;
        }

        return false;
    }
}