using Microsoft.AspNetCore.Components;

namespace Aetherium.Components.Functions.Config
{
    public class AdvRestart
    {
        public static void AdvRestartToggle(ChangeEventArgs e)
        {
            AppConfig.advRestartType = (bool)(e?.Value ?? false);
            Configuration.Instance.AdvRestartType = AppConfig.advRestartType;
            ServerConfigsSave.SaveConfig();
        }
    }
}