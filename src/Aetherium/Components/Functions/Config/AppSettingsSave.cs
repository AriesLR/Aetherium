using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class AppSettingsSave
    {
        public static void SaveConfigNames()
        {
            string appSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium", "appsettings.json");
            if (File.Exists(appSettingsPath))
            {
                try
                {
                    // Read the existing appsettings.json file
                    string json = File.ReadAllText(appSettingsPath);
                    var appSettingsObject = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                    // Update the ConfigNames property with the updated list of config names
                    if (appSettingsObject.ContainsKey("ConfigNames"))
                    {
                        var existingConfigNames = ((JsonElement)appSettingsObject["ConfigNames"]).EnumerateArray().Select(x => x.GetString()).ToList();
                        foreach (var newConfigName in AppConfig.ConfigNames)
                        {
                            if (!existingConfigNames.Contains(newConfigName))
                            {
                                existingConfigNames.Add(newConfigName);
                            }
                        }
                        appSettingsObject["ConfigNames"] = existingConfigNames;
                    }
                    else
                    {
                        appSettingsObject.Add("ConfigNames", AppConfig.ConfigNames);
                    }

                    // Update the "SelectedConfig" property
                    if (appSettingsObject.ContainsKey("SelectedTheme"))
                    {
                        appSettingsObject["SelectedTheme"] = AppConfig.SelectedTheme;

                        // Write the updated JSON back to the appsettings.json file
                        json = JsonSerializer.Serialize(appSettingsObject, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(appSettingsPath, json);
                        Debug.WriteLine("SelectedTheme updated in appsettings.json.");
                    }
                    else
                    {
                        Debug.WriteLine("SelectedTheme property not found in appsettings.json.");
                        ToastService.Toast("SelectedTheme property not found in appsettings.json.", "");
                    }

                    // Write the updated JSON back to the appsettings.json file
                    json = JsonSerializer.Serialize(appSettingsObject, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(appSettingsPath, json);

                    Debug.WriteLine("ConfigNames updated in appsettings.json.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating ConfigNames in appsettings.json: {ex.Message}");
                    ToastService.Toast("Error updating ConfigNames in appsettings.json:", ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("appsettings.json file not found.");
                ToastService.Toast("appsettings.json file not found.", "");
            }
        }
    }
}