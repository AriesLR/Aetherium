using Aetherium.Components.Functions.Services;

namespace Aetherium
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckForUpdates();
        }

        public async void CheckForUpdates()
        {
            await UpdateChecker.CheckForUpdatesAsync();
        }
    }
}
