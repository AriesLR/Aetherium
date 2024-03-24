using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Services;
using Aetherium.Components.Pages;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Server
{
    public class ServerStop
    {
        public static async Task StopServer()
        {
            if (string.IsNullOrEmpty(Configuration.Instance.AdvRestartCommand))
            {
                ForceStopServer();
            }
            else
            {
                // Send a command via server input or RCON based on advRestartType
                if (!AppConfig.advRestartType)
                {
                    // If advRestartType is false, send command via server input
                    SendCommandViaServerInput(Configuration.Instance.AdvRestartCommand);
                }
                else
                {
                    // If advRestartType is true, connect RCON and send command
                    await SendCommandViaRCONAsync(Configuration.Instance.AdvRestartCommand);
                }
            }
        }

        public static void ForceStopServer()
        {
            if (AppConfig.serverStopping)
            {
                return; // Prevent stopping multiple times
            }

            if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
            {
                AppConfig.serverStopping = true; // Disable stop button while stopping

                Debug.WriteLine("[DEBUG]: Force stopping Server Process");

                AppConfig.serverProcess.Kill();
                AppConfig.serverProcess.Dispose();
                AppConfig.serverProcess = null;
                AppConfig.serverOutput = null;

                StopTimers();

                AppConfig.serverStopping = false; // Re-enable stop button
            }
            else
            {
                Debug.WriteLine("[DEBUG]: Server is not running.");
                ToastService.Alert("Server is not running.");
            }
        }

        private static async Task SendCommandViaServerInput(string command)
        {
            Debug.WriteLine($"Sending command via server input: {command}");
            if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
            {
                await AppConfig.serverProcess.StandardInput.WriteLineAsync(command);
                await AppConfig.serverProcess.StandardInput.FlushAsync();
                AppConfig.serverOutput = "";
            }
        }

        private static async Task SendCommandViaRCONAsync(string command)
        {
            Debug.WriteLine($"Connecting RCON and sending command: {command}");
            if (!AppConfig.isConnected)
            {
                await Rcon.RconConnect();
            }

            if (AppConfig.isConnected)
            {
                AppConfig.rconCommand = command;
                await Rcon.RconSendCommand();
                AppConfig.rconCommand = "";
                AppConfig.serverOutput = "";
            }

            Rcon.RconDisconnect();
        }


        private static void StopTimers()
        {
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
        }
    }
}
