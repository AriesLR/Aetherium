using System;

namespace Aetherium.Components.Shared
{
    public class Configuration
    {
        public string ConfigName { get; set; } = "";
        public string ServerPath { get; set; } = "";
        public string BackupPath { get; set; } = "";
        public string SavePath { get; set; } = "";
        public string RconIp { get; set; } = "";
        public string RconPort { get; set; } = "";
        public string AdminPassword { get; set; } = "";
        public string LaunchParams { get; set; } = "";
        public int BackupInterval { get; set; }
        public int RestartInterval { get; set; }
        public bool AutomaticRestarts { get; set; }
        public bool SaveBackupsEnabled { get; set; }
    }
}
