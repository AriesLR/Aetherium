namespace Aetherium.Components.Functions.Services
{
    public class UrlService
    {
        public static async Task OpenUrl(string url)
        {
            await Launcher.OpenAsync(new Uri(url));
        }
    }
}