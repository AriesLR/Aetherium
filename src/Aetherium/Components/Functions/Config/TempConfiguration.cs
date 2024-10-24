namespace Aetherium.Components.Functions.Config
{
    public class TempConfiguration
    {
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
    }
}
