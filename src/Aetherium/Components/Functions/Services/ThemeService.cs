using Aetherium.Components.Functions.Config;
using Microsoft.JSInterop;


namespace Aetherium.Components.Functions.Services
{
    public class ThemeService
    {
        private readonly IJSRuntime _jsRuntime;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetThemeAsync(string themeName)
        {
            await _jsRuntime.InvokeVoidAsync("setTheme", themeName);
        }
    }
}