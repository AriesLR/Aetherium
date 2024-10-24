namespace Aetherium.Components.Functions.Config
{
    public class Configuration
    {
        private static Configuration _instance;

        public string? Prefix { get; set; } = "Aetherium";
        public string? ConfigName { get; set; } = "";
        public string ServerPath { get; set; } = "";
        public string BackupPath { get; set; } = "";
        public string SavePath { get; set; } = "";
        public string RconIp { get; set; } = "";
        public string RconPort { get; set; } = "";
        public string RconPassword { get; set; } = "";
        public string LaunchParams { get; set; } = "";
        public int BackupInterval { get; set; }
        public int RestartInterval { get; set; }
        public bool AutomaticRestarts { get; set; }
        public bool SaveBackupsEnabled { get; set; }
        public bool AdvRestartType { get; set; }
        public string? AdvRestartCommand { get; set; } = "";
        public string? McJavaVersion { get; set; } = "";


        // Private constructor ensures that a new instance cannot be created from outside the class
        private Configuration() { }

        // Public static method to get the instance of the class
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }
                return _instance;
            }
        }

        // Method to reset configuration to default values
        public void Reset()
        {
            ConfigName = null;
            // Reset other properties to their default values here
        }

        // Method to update properties from another Configuration instance
        public void UpdateFrom(TempConfiguration tempConfig)
        {
            // Update each property from the 'other' instance
            this.Prefix = tempConfig.Prefix;
            this.ConfigName = tempConfig.ConfigName;
            this.ServerPath = tempConfig.ServerPath;
            this.BackupPath = tempConfig.BackupPath;
            this.SavePath = tempConfig.SavePath;
            this.RconIp = tempConfig.RconIp;
            this.RconPort = tempConfig.RconPort;
            this.RconPassword = tempConfig.RconPassword;
            this.LaunchParams = tempConfig.LaunchParams;
            this.BackupInterval = tempConfig.BackupInterval;
            this.RestartInterval = tempConfig.RestartInterval;
            this.AutomaticRestarts = tempConfig.AutomaticRestarts;
            this.SaveBackupsEnabled = tempConfig.SaveBackupsEnabled;
            this.AdvRestartType = tempConfig.AdvRestartType;
            this.AdvRestartCommand = tempConfig.AdvRestartCommand;
            this.McJavaVersion = tempConfig.McJavaVersion;
        }
    }
}
