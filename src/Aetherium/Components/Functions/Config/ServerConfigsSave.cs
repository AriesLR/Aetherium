using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Config
{
    public class ServerConfigsSave
    {
        public static void SaveConfig()
        {
            string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Aetherium");
            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            // Use Configuration.Instance to ensure you're working with the singleton instance
            string configFilePath = Path.Combine(configFolderPath, $"{Configuration.Instance.ConfigName}.json");

            // Serialize the current state of the Configuration instance
            var json = JsonSerializer.Serialize(Configuration.Instance, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configFilePath, json);
            Debug.WriteLine("Configuration saved successfully");
            ToastService.Toast("Configuration saved successfully", "");
        }
    }
}
