using Aetherium.Components.Functions.Config;

namespace Aetherium.Components.Functions.Server
{
    public class RestartTimerStop
    {
        public static void StopRestartTimer()
        {
            AppConfig.restartTimer?.Dispose();
            AppConfig.restartTimer = null;
        }
    }
}