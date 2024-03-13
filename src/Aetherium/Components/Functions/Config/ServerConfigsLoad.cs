using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class ServerConfigsLoad
    {
        public static void LoadServerConfig(string configName)
        {
            Debug.WriteLine($"Loading Configuration: {configName}");
            string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium");
            string configFilePath = Path.Combine(configFolderPath, $"{configName}.json");
            Debug.WriteLine($"Config file path: {configFilePath}");
            if (File.Exists(configFilePath))
            {
                try
                {
                    // Deserialize Json
                    string json = File.ReadAllText(configFilePath);
                    var tempConfig = JsonSerializer.Deserialize<TempConfiguration>(json);
                    if (tempConfig != null)
                    {
                        var config = Configuration.Instance;
                        config.UpdateFrom(tempConfig);
                        Debug.WriteLine("Configuration loaded successfully.");
                    }
                    // Load the automatic restarts status
                    AppConfig.automaticRestarts = Configuration.Instance.AutomaticRestarts;

                    // Load the save backups status
                    AppConfig.saveBackupsEnabled = Configuration.Instance.SaveBackupsEnabled;

                    // Load the launch parameters
                    AppConfig.launchParams = Configuration.Instance.LaunchParams;

                    // Load the server path
                    AppConfig.serverPath = Configuration.Instance.ServerPath;

                    // Load the rcon Ip
                    AppConfig.rconIp = Configuration.Instance.RconIp;

                    // Load the rcon Port
                    AppConfig.rconPort = Configuration.Instance.RconPort;

                    // Load the rcon Password
                    AppConfig.rconPassword = Configuration.Instance.AdminPassword;

                    // Update SelectedConfig in appsettings.json
                    string appSettingsPath = Path.Combine(configFolderPath, "appsettings.json");
                    if (File.Exists(appSettingsPath))
                    {
                        try
                        {
                            // Read the existing appsettings.json file
                            string appSettingsJson = File.ReadAllText(appSettingsPath);
                            var appSettingsObject = JsonSerializer.Deserialize<Dictionary<string, object>>(appSettingsJson);

                            // Update the "SelectedConfig" property
                            if (appSettingsObject.ContainsKey("SelectedConfig"))
                            {
                                appSettingsObject["SelectedConfig"] = configName;

                                // Write the updated JSON back to the appsettings.json file
                                appSettingsJson = JsonSerializer.Serialize(appSettingsObject, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(appSettingsPath, appSettingsJson);
                                Debug.WriteLine("SelectedConfig updated in appsettings.json.");
                            }
                            else
                            {
                                Debug.WriteLine("SelectedConfig property not found in appsettings.json.");
                                AppServices.ToastService.ShowError("SelectedConfig property not found in appsettings.json.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error updating SelectedConfig in appsettings.json: {ex.Message}");
                            AppServices.ToastService.ShowError($"Error updating SelectedConfig in appsettings.json: {ex.Message}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("appsettings.json file not found.");
                        AppServices.ToastService.ShowError("appsettings.json file not found.");

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading config file: {ex.Message}");
                    AppServices.ToastService.ShowError($"Error loading config file: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("Config file does not exist.");
                AppServices.ToastService.ShowError("Config file does not exist.");
            }
        }
    }
}