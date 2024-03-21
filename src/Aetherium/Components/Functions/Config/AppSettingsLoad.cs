using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;
using System.Text.Json;


namespace Aetherium.Components.Functions.Config
{
    public class AppSettingsLoad
    {
        public static void LoadAppConfig()
        {
            string appSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium", "appsettings.json");
            if (File.Exists(appSettingsPath))
            {
                try
                {
                    string json = File.ReadAllText(appSettingsPath);
                    var jsonObject = JsonDocument.Parse(json).RootElement;
                    var config = Configuration.Instance;
                    if (jsonObject.TryGetProperty("ConfigNames", out JsonElement configNamesElement))
                    {
                        AppConfig.ConfigNames = JsonSerializer.Deserialize<List<string>>(configNamesElement.GetRawText());
                    }
                    else
                    {
                        Debug.WriteLine("ConfigNames property not found in appsettings.json.");
                        ToastService.Toast("ConfigNames property not found in appsettings.json.", "");
                    }
                    if (jsonObject.TryGetProperty("SelectedConfig", out JsonElement selectedConfigElement))
                    {
                        config.ConfigName = selectedConfigElement.GetString();
                        AppConfig.SelectedConfig = selectedConfigElement.GetString(); // This isn't needed, change this to Configuration.Instance.ConfigName in other parts.
                    }
                    else
                    {
                        Debug.WriteLine("SelectedConfig property not found in appsettings.json.");
                        ToastService.Toast("SelectedConfig property not found in appsettings.json.", "");
                    }
                    if (jsonObject.TryGetProperty("SelectedTheme", out JsonElement selectedThemeElement))
                    {
                        AppConfig.SelectedTheme = selectedThemeElement.GetString();
                    }
                    else
                    {
                        Debug.WriteLine("SelectedTheme property not found in appsettings.json.");
                        ToastService.Toast("SelectedTheme property not found in appsettings.json.", "");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading appsettings file: {ex.Message}");
                    ToastService.Toast("Error loading appsettings file:", ex.Message);
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