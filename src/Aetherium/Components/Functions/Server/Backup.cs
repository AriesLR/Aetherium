using Aetherium.Components.Functions.Config;
using Aetherium.Components.Functions.Toasts;
using System.Diagnostics;
using System.IO.Compression;

namespace Aetherium.Components.Functions.Server
{
    public class Backup
    {
        public static async Task PerformBackup()
        {
            try
            {
                // Get the backup path
                string backupFolderPath = Configuration.Instance.BackupPath;

                // Construct the save folder path
                string saveFolderPath = Configuration.Instance.SavePath;

                // Check if the save folder exists
                if (Directory.Exists(saveFolderPath))
                {
                    // Create a temporary folder for backup
                    string tempBackupFolder = Path.Combine(Path.GetTempPath(), "Aetherium_Backup_" + Guid.NewGuid().ToString());

                    // Copy the save folder to the temporary location
                    Directory.CreateDirectory(tempBackupFolder);
                    DirectoryCopy(saveFolderPath, tempBackupFolder, true);

                    // Compress the backup folder
                    string zipFileName = $"{Configuration.Instance.ConfigName}_backup_{DateTime.Now:MM-dd-yyyy-HHmm}.zip";
                    string zipFilePath = Path.Combine(backupFolderPath, zipFileName);
                    ZipFile.CreateFromDirectory(tempBackupFolder, zipFilePath);

                    // Remove the temporary backup folder
                    Directory.Delete(tempBackupFolder, true);

                }
                else
                {
                    Debug.WriteLine("[DEBUG]: Save folder not found.");
                    ToastService.Toast("Save folder not found.", "");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG]: Error performing save backups: {ex.Message}");
                ToastService.Toast("Error performing save backups:", ex.Message);
            }
        }

        // Helper method to copy directory
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}