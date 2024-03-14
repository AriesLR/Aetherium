using Aetherium.Components.Pages;
using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Server;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Aetherium
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        // Override the CreateWindow method to subscribe to the Destroying event
        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            window.Height = 750;
            window.Width = 1450;
            window.MinimumHeight = 750;
            window.MinimumWidth = 1450;
            window.MaximumHeight = 750;
            window.MaximumWidth = 1450;
            window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
            window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;

            window.Destroying += (s, e) =>
            {
                // Unsubscribe from the server events
                Server server = new Server();
                ServerStart.OnOutputDataReceived -= server.HandleOutputDataReceived;
                ServerStart.OnErrorDataReceived -= server.HandleErrorDataReceived;
                ServerStart.OnProcessExited -= server.HandleProcessExited;

                AppConfig.backupTimer?.Stop();
                AppConfig.backupTimer?.Dispose();
                AppConfig.backupTimer = null;

                // Stop the server process if it's running
                if (AppConfig.serverProcess != null && !AppConfig.serverProcess.HasExited)
                {
                    // Kill the server process
                    AppConfig.serverProcess.Kill();
                    AppConfig.serverProcess.Dispose();
                    AppConfig.serverProcess = null;
                }
            };

            return window;
        }
    }
}
