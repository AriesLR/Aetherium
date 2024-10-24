using Aetherium.Components.Functions.Config;
using System.Diagnostics;
using WindowsAPICodePack.Dialogs;



namespace Aetherium.Components.Functions.Services
{
    public class FilepickerService
    {
        public static string PickFile()
        {
            try
            {
                using CommonOpenFileDialog fileResult = new()
                {
                    IsFolderPicker = false,
                };
                fileResult.Filters.Add(new CommonFileDialogFilter("Supported file types", "*.exe, *.jar"));
                fileResult.Filters.Add(new CommonFileDialogFilter("Executable program file", "*.exe"));
                fileResult.Filters.Add(new CommonFileDialogFilter("Java architecture file", "*.jar"));

                if (fileResult.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    _ = fileResult.FileName;
                    if (fileResult != null)
                    {
                        return fileResult.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while picking the file: {ex.Message}");
            }
            return null;
        }

        public static string PickFolder()
        {
            try
            {
                using CommonOpenFileDialog folderResult = new()
                {
                    IsFolderPicker = true,
                };

                if (folderResult.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    _ = folderResult.FileName;
                    if (folderResult != null)
                    {
                        return folderResult.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while picking the folder: {ex.Message}");
            }
            return null;
        }

        public static void PickServerPath()
        {
            var filePath = PickFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                Configuration.Instance.ServerPath = filePath;
                ServerConfigsSave.SaveConfig();
            }
        }

        public static void PickSavePath()
        {
            var folderPath = PickFolder();
            if (!string.IsNullOrEmpty(folderPath))
            {
                Configuration.Instance.SavePath = folderPath;
                ServerConfigsSave.SaveConfig();
            }
        }

        public static void PickBackupPath()
        {
            var folderPath = PickFolder();
            if (!string.IsNullOrEmpty(folderPath))
            {
                Configuration.Instance.BackupPath = folderPath;
                ServerConfigsSave.SaveConfig();
            }
        }

        public static void PickJavaPath()
        {
            var filePath = PickFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                Configuration.Instance.McJavaVersion = filePath;
                ServerConfigsSave.SaveConfig();
            }
        }
    }
}