using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Server
{
    public class ServerStop
    {
        public static void StopServer()
        {
            if (AppConfig.serverStopping)
            {
                return; // Prevent stopping multiple times
            }

            if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
            {
                AppConfig.serverStopping = true; // Disable stop button while stopping

                Debug.WriteLine("[DEBUG]: Stopping Server Process");

                AppConfig.serverProcess.Kill();
                AppConfig.serverProcess.Dispose();
                AppConfig.serverProcess = null;
                AppConfig.serverOutput = null;

                // Stop the restart timer if automatic restarts are enabled
                if (AppConfig.automaticRestarts)
                {
                    RestartTimerStop.StopRestartTimer();
                    Debug.WriteLine("[DEBUG]: Stopping Restart Timer");
                }
                // Stop the backup timer if save backups are enabled
                if (AppConfig.saveBackupsEnabled)
                {
                    BackupTimerStop.StopBackupTimer();
                    Debug.WriteLine("[DEBUG]: Stopping Backup Timer");
                }

                AppConfig.serverStopping = false; // Re-enable stop button
            }
            else
            {
                Debug.WriteLine("[DEBUG]: Server is not running.");
                ToastService.Toast("Server is not running.", "");
            }
        }
    }
}