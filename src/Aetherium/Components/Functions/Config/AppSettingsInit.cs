using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class AppSettingsInit
    {
        public static void InitAppSettings()
        {
            string appSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium", "appsettings.json");
            string appSettingsDir = Path.GetDirectoryName(appSettingsPath);

            // Create the Aetherium directory if it doesn't exist
            if (!Directory.Exists(appSettingsDir))
            {
                Directory.CreateDirectory(appSettingsDir);
            }

            if (!File.Exists(appSettingsPath))
            {
                // Create appsettings.json with default configuration names and selected config
                var settings = new
                {
                    ConfigNames = new List<string> { "Default" },
                    SelectedConfig = "Default",
                    SelectedTheme = "a-dark"
                };
                var json = JsonSerializer.Serialize(settings);
                File.WriteAllText(appSettingsPath, json);

                // Create default configuration file if it doesn't exist
                ServerConfigsNew.CreateNewConfigFile("Default");
            }
            else
            {
                // Check if the default configuration exists, if not, create it
                string defaultConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium", "Default.json");
                if (!File.Exists(defaultConfigPath))
                {
                    ServerConfigsNew.CreateNewConfigFile("Default");
                }
            }
        }
    }
}