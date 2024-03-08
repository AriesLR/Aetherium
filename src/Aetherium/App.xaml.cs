using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Aetherium.Components.Pages;
using System.Diagnostics;
using Aetherium.Components.Layout;
using static Aetherium.Components.Layout.ProcessMonitor;
using Microsoft.Maui.Devices;

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
                // Create an instance of the Server class
                var server = new Server();

                // Access the backupTimer field through the instance
                server.backupTimer?.Stop();
                server.backupTimer?.Dispose();
                server.backupTimer = null;

                // Stop the server process if it's running
                if (Server.serverProcess != null && !Server.serverProcess.HasExited)
                {
                    // Kill the server process
                    Server.serverProcess.Kill();
                    Server.serverProcess.Dispose();
                    Server.serverProcess = null;
                }
            };

            return window;
        }
    }
}
