using CommunityToolkit.Maui.Storage;
using System.Diagnostics;



namespace Aetherium.Components.Functions.Config
{
    public class FilepickerService
    {
        public static async Task<string?> PickFileAsync()
        {
            try
            {
                var fileResult = await FilePicker.Default.PickAsync();
                if (fileResult != null)
                {
                    return fileResult.FullPath;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while picking the file: {ex.Message}");
            }
            return null;
        }

        public static async Task<string?> PickFolderAsync()
        {
            try
            {
                var folderResult = await FolderPicker.Default.PickAsync();
                if (folderResult.IsSuccessful)
                {
                    return folderResult.Folder.Path;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while picking the folder: {ex.Message}");
            }
            return null;
        }

        public static async Task PickServerPath()
        {
            var filePath = await PickFileAsync();
            if (!string.IsNullOrEmpty(filePath))
            {
                Configuration.Instance.ServerPath = filePath;
                ServerConfigsSave.SaveConfig();
            }
        }

        public static async Task PickSavePath()
        {
            var folderPath = await PickFolderAsync();
            if (!string.IsNullOrEmpty(folderPath))
            {
                Configuration.Instance.SavePath = folderPath;
                ServerConfigsSave.SaveConfig();
            }
        }

        public static async Task PickBackupPath()
        {
            var folderPath = await PickFolderAsync();
            if (!string.IsNullOrEmpty(folderPath))
            {
                Configuration.Instance.BackupPath = folderPath;
                ServerConfigsSave.SaveConfig();
            }
        }
    }
}