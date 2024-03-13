using Aetherium.Components.Functions.Config;
using Microsoft.AspNetCore.Components;

namespace Aetherium.Components.Functions.Server
{
    public class BackupToggle
    {
        public static void ToggleBackup(ChangeEventArgs e)
        {
            AppConfig.saveBackupsEnabled = (bool)(e?.Value ?? false);
            Configuration.Instance.SaveBackupsEnabled = AppConfig.saveBackupsEnabled;
            ServerConfigsSave.SaveConfig();
        }
    }
}