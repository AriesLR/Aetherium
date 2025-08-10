using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Services;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Server
{
    public class ServerStart
    {
        public static event Action<string>? OnOutputDataReceived;
        public static event Action<string>? OnErrorDataReceived;
        public static event Action? OnProcessExited;

        public static async Task StartServer()
        {
            AppConfig.serverOutput = null;

            if (AppConfig.serverStarting)
            {
                return; // Prevent starting multiple instances
            }

            if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
            {
                Debug.WriteLine("[DEBUG]: Server is already running.");
                ToastService.Alert("Server is already running.");
                return;
            }

            if (string.IsNullOrEmpty(AppConfig.serverPath))
            {
                Debug.WriteLine("[DEBUG]: Server path is empty.");
                ToastService.Alert("Server path is empty.");
                return;
            }

            if (!File.Exists(AppConfig.serverPath))
            {
                Debug.WriteLine("[DEBUG]: Server executable not found.");
                ToastService.Alert("Server executable not found.");
                return;
            }

            // Get the file extension
            string fileExtension = Path.GetExtension(AppConfig.serverPath).ToLower();

            // Get the directory path
            string directoryPath = Path.GetDirectoryName(AppConfig.serverPath);

            // Get the filename
            string fileName = Path.GetFileName(AppConfig.serverPath);

            // Minecraft filename (Java Install)
            string mcfileName = Configuration.Instance.McJavaVersion;

            if (fileExtension == ".exe")
            {
                try
                {
                    AppConfig.serverStarting = true; // Disable start button while starting

                    AppConfig.serverProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = AppConfig.serverPath,
                            Arguments = AppConfig.launchParams,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        },
                        EnableRaisingEvents = true
                    };

                    AppConfig.serverProcess.OutputDataReceived += (sender, e) => OnOutputDataReceived?.Invoke(e.Data);
                    AppConfig.serverProcess.ErrorDataReceived += (sender, e) => OnErrorDataReceived?.Invoke(e.Data);
                    AppConfig.serverProcess.Exited += (sender, e) => OnProcessExited?.Invoke();

                    AppConfig.serverProcess.Start();
                    AppConfig.serverProcess.BeginOutputReadLine(); // Begin asynchronous read of standard output
                    AppConfig.serverProcess.BeginErrorReadLine(); // Begin asynchronous read of standard error

                    // Start the restart timer if automatic restarts are enabled
                    if (AppConfig.automaticRestarts)
                    {
                        RestartTimerStart.StartRestartTimer();
                        Debug.WriteLine("[DEBUG]: Starting Restart Timer");
                    }
                    // Start the backup timer if save backups are enabled
                    if (AppConfig.saveBackupsEnabled)
                    {
                        BackupTimerStart.StartBackupTimer();
                        Debug.WriteLine("[DEBUG]: Starting Backup Timer");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error starting server: {ex.Message}");
                    ToastService.Alert($"Error starting server:\n{ex.Message}");
                }
                finally
                {
                    AppConfig.serverStarting = false; // Re-enable start button
                }
            }

            if (fileExtension == ".jar")
            {
                try
                {
                    AppConfig.serverStarting = true; // Disable start button while starting

                    AppConfig.serverProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = mcfileName,
                            WorkingDirectory = directoryPath,
                            Arguments = $"-server {AppConfig.launchParams} -jar {fileName} nogui",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        },
                        EnableRaisingEvents = true
                    };

                    AppConfig.serverProcess.OutputDataReceived += (sender, e) => OnOutputDataReceived?.Invoke(e.Data);
                    AppConfig.serverProcess.ErrorDataReceived += (sender, e) => OnErrorDataReceived?.Invoke(e.Data);
                    AppConfig.serverProcess.Exited += (sender, e) => OnProcessExited?.Invoke();

                    AppConfig.serverProcess.Start();
                    AppConfig.serverProcess.BeginOutputReadLine(); // Begin asynchronous read of standard output
                    AppConfig.serverProcess.BeginErrorReadLine(); // Begin asynchronous read of standard error

                    // Start the restart timer if automatic restarts are enabled
                    if (AppConfig.automaticRestarts)
                    {
                        RestartTimerStart.StartRestartTimer();
                        Debug.WriteLine("[DEBUG]: Starting Restart Timer");
                    }
                    // Start the backup timer if save backups are enabled
                    if (AppConfig.saveBackupsEnabled)
                    {
                        BackupTimerStart.StartBackupTimer();
                        Debug.WriteLine("[DEBUG]: Starting Backup Timer");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error starting server: {ex.Message}");
                    ToastService.Alert($"Error starting server:\n{ex.Message}");
                }
                finally
                {
                    AppConfig.serverStarting = false; // Re-enable start button
                }
            }
        }
    }
}