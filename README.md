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
- Save folder backups.
- RCON integration.

## Planned Features

- Light Theme (Currently dark by default.)
- UI Overhaul

## Known Issues

- Server output disappears when switching pages (Server stays running.)
- Rcon output disappears when switching pages.
- Rcon disconnects when switching pages.

## Getting Started

To get started with Palhub, simply download the latest release from the [Releases](https://github.com/arieslr/palhub/releases) page. Then, follow these steps:

1. **Download the Latest Release**: Download Palhub from the [Releases](https://github.com/arieslr/palhub/releases) page.

2. **Move the Palhub Folder**: After downloading Palhub, move the extracted Palhub folder to your desired location on your machine.

3. **Run Palhub**: Once downloaded, run the Palhub executable (Palhub.exe) on your machine.

4. **Configure Your Server**: Navigate to the configuration page within Palhub to set up your server settings.

5. **Start Your Server**: Once configured, start your server with the click of a button and monitor its output in real-time.

## Configuration

While this project has started as a server launcher for just Palworld, I've already made some changes to make this work with almost any game server.
That being said, below I will post example configs for various games as I test them.

### Palworld
[Palworld Config](https://raw.githubusercontent.com/AriesLR/Palhub/main/docs/images/palworldconfig.png)



## License

[MIT License](LICENSE)

## Acknowledgements
- [CoreRCON](https://github.com/Challengermode/CoreRcon) - For the .NET Standard implementation of the Source RCON Protocol.

- [Challengermode](https://github.com/Challengermode) - For maintaining the CoreRCON package.

- [Darkbyte](https://github.com/darkbyte42) - For all the input they've given so far, their suggestions are giving me direction on what features to add.

## Tips
[Buy Me a Coffee](https://www.buymeacoffee.com/arieslr)
