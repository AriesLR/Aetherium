using Aetherium.Components.Functions.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Aetherium
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Orbitron-VariableFont_wght.ttf", "Orbitron");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<ThemeService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
