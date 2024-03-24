using CoreRCON;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Config
{
    public static class AppConfig
    {
        public static string? AppVersion = "0.1.8";
        public static List<string> ConfigNames = [];
        public static string? NewConfigName;
        public static string? SelectedConfig;
        public static string? SelectedTheme;
        public static bool automaticRestarts;
        public static bool saveBackupsEnabled;
        public static string serverPath = "";
        public static string? launchParams;
        public static Process? serverProcess;
        public static Timer? restartTimer;
        public static System.Timers.Timer? backupTimer;
        public static string serverInput = "";
        public static string? serverOutput = "";
        public static bool serverStarting = false;
        public static bool serverStopping = false;
        public static RCON? rcon;
        public static string? rconIp = "";
        public static string? rconPort = "";
        public static string? rconPassword = "";
        public static string rconCommand = "";
        public static string? rconOutput = "";
        public static bool isConnected = false;
        public static bool advRestartType;
    }
}