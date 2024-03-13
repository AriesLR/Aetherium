using Aetherium.Components.Functions.Config;

namespace Aetherium.Components.Functions.Server
{
    public class RestartTimerStart
    {
        public static void StartRestartTimer()
        {
            // Calculate the restart interval in milliseconds
            int restartIntervalMs = Configuration.Instance.RestartInterval * 60 * 1000;
            AppConfig.restartTimer = new Timer(async (_) =>
            {
                if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
                {
                    AppConfig.serverProcess.Kill();
                    await Task.Delay(10000); // Wait for 10 seconds
                    await ServerStart.StartServer(); // Start the server
                }
            }, null, restartIntervalMs, restartIntervalMs);
        }
    }
}