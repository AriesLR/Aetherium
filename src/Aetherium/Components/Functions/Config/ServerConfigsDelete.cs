using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class ServerConfigsDelete
    {
        public static void DeleteConfig()
        {
            if (!string.IsNullOrWhiteSpace(AppConfig.NewConfigName))
            {
                if (AppConfig.ConfigNames.Contains(AppConfig.NewConfigName))
                {
                    // Remove the configuration from the list
                    AppConfig.ConfigNames.Remove(AppConfig.NewConfigName);

                    // Update appsettings.json with the updated ConfigNames list
                    string appSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium", "appsettings.json");
                    if (File.Exists(appSettingsPath))
                    {
                        try
                        {
                            // Read the existing appsettings.json file
                            string json = File.ReadAllText(appSettingsPath);
                            var appSettingsObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

                            // Update the ConfigNames property with the updated list of config names
                            if (appSettingsObject.ContainsKey("ConfigNames"))
                            {
                                // Convert the updated ConfigNames list to JsonElement
                                var updatedConfigNamesJson = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(AppConfig.ConfigNames));
                                appSettingsObject["ConfigNames"] = updatedConfigNamesJson;
                            }
                            else
                            {
                                Debug.WriteLine("ConfigNames property not found in appsettings.json.");
                                AppServices.ToastService.ShowError("ConfigNames property not found in appsettings.json.");
                            }

                            // Write the updated JSON back to the appsettings.json file
                            json = JsonSerializer.Serialize(appSettingsObject, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(appSettingsPath, json);
                            Debug.WriteLine("ConfigNames updated in appsettings.json.");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error updating ConfigNames in appsettings.json: {ex.Message}");
                            AppServices.ToastService.ShowError($"Error updating ConfigNames in appsettings.json: {ex.Message}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("appsettings.json file not found.");
                        AppServices.ToastService.ShowError("appsettings.json file not found.");
                    }

                    // Delete the configuration file
                    string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium");
                    string configFilePath = Path.Combine(configFolderPath, $"{AppConfig.NewConfigName}.json");
                    if (File.Exists(configFilePath))
                    {
                        File.Delete(configFilePath);
                    }

                    // If the deleted config is the currently loaded one, clear the configuration
                    if (Configuration.Instance.ConfigName == AppConfig.NewConfigName)
                    {
                        Configuration.Instance.Reset();
                    }


                    // Automatically select the Default config
                    Configuration.Instance.ConfigName = "Default";

                    // Load the Default config
                    ServerConfigsLoad.LoadServerConfig(Configuration.Instance.ConfigName);

                    // Update the UI [ADD CODE HERE IF FUNKY]
                }
                else
                {
                    Debug.WriteLine($"Configuration '{AppConfig.NewConfigName}' does not exist.");
                    AppServices.ToastService.ShowError($"Configuration '{AppConfig.NewConfigName}' does not exist.");
                }
            }
            else
            {
                Debug.WriteLine("Please enter a valid configuration name.");
                AppServices.ToastService.ShowError("Please enter a valid configuration name.");
            }
            AppConfig.NewConfigName = string.Empty;
        }
    }
}