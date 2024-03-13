using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class ServerConfigsNew
    {
        public static void CreateNewConfigFile(string configName)
        {
            string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium");
            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            string configFilePath = Path.Combine(configFolderPath, $"{configName}.json");
            if (!File.Exists(configFilePath))
            {
                try
                {
                    // Create a new configuration file with default settings
                    var defaultConfig = new TempConfiguration
                    {
                        ConfigName = configName
                        // Initialize other properties as necessary
                    };
                    var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(configFilePath, json);
                    Debug.WriteLine($"Configuration file '{configName}.json' created.");

                    // Add the new config name to the list of config names
                    AppConfig.ConfigNames.Add(configName);
                    AppSettingsSave.SaveConfigNames(); // Save the updated list of configuration names

                    // Load the newly created configuration
                    ServerConfigsLoad.LoadServerConfig(configName); // Load the newly added configuration
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error creating configuration file '{configName}.json': {ex.Message}");
                    AppServices.ToastService.ShowError($"Error creating configuration file '{configName}.json': {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine($"Configuration file '{configName}.json' already exists.");
                AppServices.ToastService.ShowError($"Configuration file '{configName}.json' already exists.");
            }
        }
    }
}
