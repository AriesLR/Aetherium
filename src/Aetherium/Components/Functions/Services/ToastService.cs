using Microsoft.Toolkit.Uwp.Notifications;

namespace Aetherium.Components.Functions.Services
{
    public class ToastService
    {
        public static void Toast(string primaryText, string secondaryText)
        {
            new ToastContentBuilder()
                .AddText(primaryText)
                .AddText(secondaryText)
                .AddAudio(null, silent: true)
                .Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddMilliseconds(5);
                });
        }

        public static async Task Alert(string titleText, string messageText)
        {
            var currentPage = Application.Current?.MainPage;
            if (currentPage != null)
            {
                await currentPage.DisplayAlert(titleText, messageText, "OK");
            }
        }

    }

}