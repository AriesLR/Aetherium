# Palhub
 Palhub simplifies server management by configuring, starting, and stopping game servers seamlessly. With intuitive features, it offers a user-friendly experience for administrators, whether setting up new servers or maintaining existing ones.

 > [!CAUTION]  
> Palhub is currently in a pre-release stage.

## Prerequisites

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.201-windows-x64-installer)
- [WebView2](https://developer.microsoft.com/en-us/microsoft-edge/webview2/consumer/?form=MA13LH)

 > [!NOTE]  
> You don't need to manually install these, if you don't have them Palhub will prompt you to download them.

## Features

- Start and stop server processes with ease.
- Monitor server output in real-time.
- Configure automatic server restarts.
- User-friendly interface.

## Planned Features

- Save Backups
- Light Theme (Currently dark by default.)
- UI Overhaul, as of right now (v0.0.1) Palhub is in a pre-release stage.

## Known Issues

- Server output disappears when switching pages (Server stays running.)
- Rcon output disappears when switching pages.
- Rcon disconnects when switching pages.
- App icon is default .NET icon (tried for far too long to figure this out, and still haven't.)
- Folder is bloated with various logo sizes (I think this is part of the appicon as well.)
- I used .NET Maui and that was my first mistake.

## Getting Started

To get started with Palhub, simply download the latest release from the [Releases](https://github.com/arieslr/palhub/releases) page. Then, follow these steps:

1. **Download the Latest Release**: Download Palhub from the [Releases](https://github.com/arieslr/palhub/releases) page.

2. **Move the Palhub Folder**: After downloading Palhub, move the extracted Palhub folder to your desired location on your machine.

3. **Run Palhub**: Once downloaded, run the Palhub executable (Palhub.exe) on your machine.

4. **Configure Your Server**: Navigate to the configuration page within Palhub to set up your server settings.

5. **Start Your Server**: Once configured, start your server with the click of a button and monitor its output in real-time.

## License

[MIT License](LICENSE)
