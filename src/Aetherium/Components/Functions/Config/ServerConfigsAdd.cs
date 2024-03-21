using Aetherium.Components.Functions.Services;
using System.Diagnostics;

namespace Aetherium.Components.Functions.Config
{
    public class ServerConfigsAdd
    {
        public static void AddNewConfig()
        {
            if (!string.IsNullOrWhiteSpace(AppConfig.NewConfigName))
            {
                if (!AppConfig.ConfigNames.Contains(AppConfig.NewConfigName))
                {
                    AppConfig.ConfigNames.Add(AppConfig.NewConfigName);
                    AppSettingsSave.SaveConfigNames(); // Save the updated list of configuration names
                    ServerConfigsNew.CreateNewConfigFile(AppConfig.NewConfigName); // Create a new configuration file

                    // Reload configurations to update the ConfigNames list
                    AppSettingsLoad.LoadAppConfig();


                    // Set the newly added configuration as the selected one
                    var config = Configuration.Instance;
                    config.ConfigName = AppConfig.NewConfigName;

                    // Update the UI [ADD CODE HERE IF FUNKY]
                }
                else
                {
                    Debug.WriteLine($"Configuration '{AppConfig.NewConfigName}' already exists.");
                    ToastService.Toast($"Configuration '{AppConfig.NewConfigName}' already exists.", "");
                }
            }
            else
            {
                Debug.WriteLine("Please enter a valid configuration name.");
                ToastService.Toast("Please enter a valid configuration name.", "");
            }
            AppConfig.NewConfigName = string.Empty;
        }
    }
}