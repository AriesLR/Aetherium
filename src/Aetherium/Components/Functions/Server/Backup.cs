using Aetherium.Components.Functions.Config;
using Alphaleonis.Win32.Vss;
using System.Diagnostics;
using System.IO.Compression;

namespace Aetherium.Components.Functions.Server
{
    public static class Backup
    {
        public static void PerformBackup()
        {
            string sourceDir = Configuration.Instance.SavePath;
            string tempBackupFolder = Path.Combine(Configuration.Instance.BackupPath, "Aetherium_Backup_" + Guid.NewGuid().ToString());
            string backupRoot = tempBackupFolder;

            using (VssBackup vss = new VssBackup())
            {
                vss.Setup(Path.GetPathRoot(sourceDir));
                string snapDirPath = vss.GetSnapshotPath(sourceDir);

                // Assuming you have a CopyDirectory method implemented
                CopyDirectory(snapDirPath, backupRoot);

                // Compress the backup folder
                string zipFileName = $"{Configuration.Instance.ConfigName}_backup_{DateTime.Now:MM-dd-yyyy-HHmm-ss}.zip";
                string zipFilePath = Path.Combine(Configuration.Instance.BackupPath, zipFileName);
                ZipFile.CreateFromDirectory(tempBackupFolder, zipFilePath);

                // Remove the temporary backup folder
                Directory.Delete(tempBackupFolder, true);
            }
        }

        private static void CopyDirectory(string sourceDirName, string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }

        // Anything below this point is *Magic*
        public class VssBackup : IDisposable
        {
            bool ComponentMode = false;
            IVssBackupComponents _backup;
            Snapshot _snap;

            public VssBackup()
            {
                InitializeBackup();
            }

            public void Setup(string volumeName)
            {
                Discovery(volumeName);
                PreBackup();
            }

            public void Dispose()
            {
                try { Complete(true); } catch { }

                if (_snap != null)
                {
                    _snap.Dispose();
                    _snap = null;
                }

                if (_backup != null)
                {
                    _backup.Dispose();
                    _backup = null;
                }
            }

            void InitializeBackup()
            {
                IVssFactory vss = VssFactoryProvider.Default.GetVssFactory();
                _backup = vss.CreateVssBackupComponents();
                _backup.InitializeForBackup(null);
                _backup.GatherWriterMetadata();
            }

            void Discovery(string fullPath)
            {
                if (ComponentMode)
                    ExamineComponents(fullPath);
                else
                    _backup.FreeWriterMetadata();

                _snap = new Snapshot(_backup);
                _snap.AddVolume(Path.GetPathRoot(fullPath));
            }


            void ExamineComponents(string fullPath)
            {
                IList<IVssExamineWriterMetadata> writer_mds = _backup.WriterMetadata;

                foreach (IVssExamineWriterMetadata metadata in writer_mds)
                {
                    Trace.WriteLine("Examining metadata for " + metadata.WriterName);

                    foreach (IVssWMComponent cmp in metadata.Components)
                    {
                        Trace.WriteLine("  Component: " + cmp.ComponentName);
                        Trace.WriteLine("  Component info: " + cmp.Caption);

                        foreach (VssWMFileDescriptor file in cmp.Files)
                        {

                            Trace.WriteLine("    Path: " + file.Path);
                            Trace.WriteLine("       Spec: " + file.FileSpecification);

                        }
                    }
                }
            }

            void PreBackup()
            {
                Debug.Assert(_snap != null);

                _backup.SetBackupState(ComponentMode,
                      true, VssBackupType.Full, false);
                _backup.PrepareForBackup();

                _snap.Copy();
            }

            public string GetSnapshotPath(string localPath)
            {
                Trace.WriteLine("New volume: " + _snap.Root);

                if (Path.IsPathRooted(localPath))
                {
                    string root = Path.GetPathRoot(localPath);
                    localPath = localPath.Replace(root, String.Empty);
                }
                string slash = Path.DirectorySeparatorChar.ToString();
                if (!_snap.Root.EndsWith(slash) && !localPath.StartsWith(slash))
                    localPath = localPath.Insert(0, slash);
                localPath = localPath.Insert(0, _snap.Root);

                Trace.WriteLine("Converted path: " + localPath);

                return localPath;
            }


            public System.IO.Stream GetStream(string localPath)
            {
                return File.OpenRead(GetSnapshotPath(localPath));
            }

            void Complete(bool succeeded)
            {
                if (ComponentMode)
                {
                    IList<IVssExamineWriterMetadata> writers = _backup.WriterMetadata;
                    foreach (IVssExamineWriterMetadata metadata in writers)
                    {
                        foreach (IVssWMComponent component in metadata.Components)
                        {
                            _backup.SetBackupSucceeded(
                                  metadata.InstanceId, metadata.WriterId,
                                  component.Type, component.LogicalPath,
                                  component.ComponentName, succeeded);
                        }
                    }

                    _backup.FreeWriterMetadata();
                }

                try
                {
                    _backup.BackupComplete();
                }
                catch (VssBadStateException) { }
            }

            string FileToPathSpecification(VssWMFileDescriptor file)
            {
                string path = Environment.ExpandEnvironmentVariables(file.Path);

                if (!String.IsNullOrEmpty(file.AlternateLocation))
                    path = Environment.ExpandEnvironmentVariables(
                          file.AlternateLocation);

                string spec = file.FileSpecification.Replace("*.*", "*");


                return Path.Combine(path, file.FileSpecification);
            }
        }

        class Snapshot : IDisposable
        {
            IVssBackupComponents _backup;

            VssSnapshotProperties _props;

            Guid _set_id;

            Guid _snap_id;


            public Snapshot(IVssBackupComponents backup)
            {
                _backup = backup;
                _set_id = backup.StartSnapshotSet();
            }

            public void Dispose()
            {
                try { Delete(); } catch { }
            }

            public void AddVolume(string volumeName)
            {
                if (_backup.IsVolumeSupported(volumeName))
                    _snap_id = _backup.AddToSnapshotSet(volumeName);
                else
                    throw new VssVolumeNotSupportedException(volumeName);
            }

            public void Copy()
            {
                _backup.DoSnapshotSet();
            }

            public void Delete()
            {
                _backup.DeleteSnapshotSet(_set_id, false);
            }

            public string Root
            {
                get
                {
                    if (_props == null)
                        _props = _backup.GetSnapshotProperties(_snap_id);
                    return _props.SnapshotDeviceObject;
                }
            }
        }
    }
}
