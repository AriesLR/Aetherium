using Aetherium.Components.Functions.Config;
using Microsoft.AspNetCore.Components;

namespace Aetherium.Components.Functions.Server
{
    public class RestartToggle
    {
        public static void ToggleRestart(ChangeEventArgs e)
        {
            AppConfig.automaticRestarts = (bool)(e?.Value ?? false);
            Configuration.Instance.AutomaticRestarts = AppConfig.automaticRestarts;
            ServerConfigsSave.SaveConfig();
        }
    }
}