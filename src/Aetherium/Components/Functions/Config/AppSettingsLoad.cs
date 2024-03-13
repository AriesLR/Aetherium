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
                        AppServices.ToastService.ShowError("ConfigNames property not found in appsettings.json.");
                    }
                    if (jsonObject.TryGetProperty("SelectedConfig", out JsonElement selectedConfigElement))
                    {
                        config.ConfigName = selectedConfigElement.GetString();
                    }
                    else
                    {
                        Debug.WriteLine("SelectedConfig property not found in appsettings.json.");
                        AppServices.ToastService.ShowError("SelectedConfig property not found in appsettings.json.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading appsettings file: {ex.Message}");
                    AppServices.ToastService.ShowError($"Error loading appsettings file: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("appsettings.json file not found.");
                AppServices.ToastService.ShowError("appsettings.json file not found.");
            }
        }
    }
}