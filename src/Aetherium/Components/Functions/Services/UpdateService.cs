using Aetherium.Components.Functions.Config;
using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Componenets.Functions.Services
{
    public class UpdateChecker
    {
        private const string UpdateCheckUrl = ""; // Url to Json

        public static async Task CheckForUpdatesAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                string jsonResponse = await httpClient.GetStringAsync(UpdateCheckUrl);

                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(jsonResponse);

                if (updateInfo != null)
                {
                    var currentVersion = new Version(AppConfig.AppVersion); // Your current app version
                    var latestVersion = new Version(updateInfo.LatestVersion);

                    if (latestVersion > currentVersion)
                    {
                        Debug.WriteLine($"A new version is available: {updateInfo.LatestVersion}");
                        Debug.WriteLine($"Download it here: {updateInfo.DownloadUrl}");
                        // Logic to prompt the user to download the new version
                    }
                    else
                    {
                        Debug.WriteLine("You are on the latest version.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking for updates: {ex.Message}");
                // Handle the error (logging, user notification, etc.)
            }
        }
    }

    public class UpdateInfo
    {
        public string LatestVersion { get; set; }
        public string DownloadUrl { get; set; }
    }
}