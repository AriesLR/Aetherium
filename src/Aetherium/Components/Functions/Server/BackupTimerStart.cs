using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Server
{
    public class BackupTimerStart
    {
        public static void StartBackupTimer()
        {
            // Check if save backups are enabled
            if (AppConfig.saveBackupsEnabled)
            {
                try
                {
                    // Only start backup if the backup timer is not running
                    if (AppConfig.backupTimer == null)
                    {
                        // Get the backup interval in minutes
                        int backupIntervalMinutes = Configuration.Instance.BackupInterval;

                        // Create a new backup timer
                        AppConfig.backupTimer = new System.Timers.Timer();
                        AppConfig.backupTimer.Interval = backupIntervalMinutes * 60000; // Convert minutes to milliseconds
                        AppConfig.backupTimer.Elapsed += async (sender, e) => await Backup.PerformBackup();
                        AppConfig.backupTimer.AutoReset = true;
                        AppConfig.backupTimer.Start();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DEBUG]: Error starting save backups: {ex.Message}");
                    ToastService.Toast("Error starting save backups:", ex.Message);
                }
            }
        }
    }
}