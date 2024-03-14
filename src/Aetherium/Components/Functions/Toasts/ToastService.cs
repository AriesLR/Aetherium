using Microsoft.Toolkit.Uwp.Notifications;

namespace Aetherium.Components.Functions.Toasts
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
    }

}