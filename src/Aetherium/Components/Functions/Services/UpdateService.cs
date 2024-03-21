using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;
using System.Text.Json;

namespace Aetherium.Components.Functions.Services
{
    public class UpdateChecker
    {
        private const string UpdateCheckUrl = "https://raw.githubusercontent.com/AriesLR/Aetherium/main/docs/version/update.json";

        public static async Task CheckForUpdatesAsync()
        {
            Debug.WriteLine("[DEBUG]: Checking for updates...");
            try
            {
                using var httpClient = new HttpClient();
                string jsonResponse = await httpClient.GetStringAsync(UpdateCheckUrl);

                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(jsonResponse);



                if (updateInfo != null)
                {
                    var currentVersion = new Version(AppConfig.AppVersion);
                    var latestVersion = new Version(updateInfo.LatestVersion);

                    if (latestVersion > currentVersion)
                    {
                        var currentPage = Application.Current?.MainPage;
                        if (currentPage != null)
                        {
                            bool answer = await currentPage.DisplayAlert("Update Available", $"Current Version: v{AppConfig.AppVersion}\nLatest Version: v{updateInfo.LatestVersion}\n\nWould you like to update?", "Yes", "No");
                            if (answer == true)
                            {
                                try
                                {
                                    await Launcher.OpenAsync(new Uri(updateInfo.DownloadUrl));
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"An error occurred: {ex.Message}");
                                    ToastService.Toast($"An error occurred:", ex.Message);
                                }
                            }
                            if (answer == false)
                            {
                                // Do Nothing
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking for updates: {ex.Message}");
                ToastService.Toast($"Error checking for updates:", ex.Message);
            }
        }

        public static async Task ManualCheckForUpdatesAsync()
        {
            Debug.WriteLine("[DEBUG]: Checking for updates...");
            try
            {
                using var httpClient = new HttpClient();
                string jsonResponse = await httpClient.GetStringAsync(UpdateCheckUrl);

                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(jsonResponse);



                if (updateInfo != null)
                {
                    var currentVersion = new Version(AppConfig.AppVersion);
                    var latestVersion = new Version(updateInfo.LatestVersion);

                    if (latestVersion > currentVersion)
                    {
                        var currentPage = Application.Current?.MainPage;
                        if (currentPage != null)
                        {
                            bool answer = await currentPage.DisplayAlert("Update Available", $"Current Version: v{AppConfig.AppVersion}\nLatest Version: v{updateInfo.LatestVersion}\n\nWould you like to update?", "Yes", "No");
                            if (answer == true)
                            {
                                try
                                {
                                    await Launcher.OpenAsync(new Uri(updateInfo.DownloadUrl));
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"An error occurred: {ex.Message}");
                                    ToastService.Toast($"An error occurred:", ex.Message);
                                }
                            }
                            if (answer == false)
                            {
                                // Do Nothing
                            }
                        }
                    }
                    else
                    {
                        var currentPage = Application.Current?.MainPage;
                        if (currentPage != null)
                        {
                            await currentPage.DisplayAlert("Aetherium is up to date!", $"Current Version: v{AppConfig.AppVersion}\nLatest Version: v{updateInfo.LatestVersion}", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking for updates: {ex.Message}");
                ToastService.Toast($"Error checking for updates:", ex.Message);
            }
        }
    }



    public class UpdateInfo
    {
        public string LatestVersion { get; set; }
        public string DownloadUrl { get; set; }
    }
}