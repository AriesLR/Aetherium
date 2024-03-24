using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Services;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Server
{
    public class ServerInput
    {
        public static async Task SendServerInput()
        {
            try
            {
                if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
                {
                    await AppConfig.serverProcess.StandardInput.WriteLineAsync(AppConfig.serverInput);
                    await AppConfig.serverProcess.StandardInput.FlushAsync();
                    AppConfig.serverInput = "";
                }
                else
                {
                    // Handle the case when the server process is not running
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG]: Error sending server input: {ex.Message}");
                ToastService.Alert($"Error sending server input:\n{ex.Message}");
            }
        }
    }
}