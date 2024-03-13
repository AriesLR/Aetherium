using Aetherium.Components.Functions.Config;

namespace Aetherium.Components.Functions.Server
{
    public class BackupTimerStop
    {
        public static void StopBackupTimer()
        {
            AppConfig.backupTimer?.Stop();
            AppConfig.backupTimer?.Dispose();
            AppConfig.backupTimer = null;
        }
    }
}