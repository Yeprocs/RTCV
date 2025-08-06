namespace RTCV.UI
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Runtime.Serialization;

    internal static class Common
    {
        internal static void CopyFile(string sourcePath, string targetDirectory, string folderName = null, bool confirmOverwrite = true)
        {
            string shortPath = sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1);
            string targetPath = Path.Combine(targetDirectory, shortPath);
            string destinationName = new DirectoryInfo(targetDirectory).Name;
            ReplaceFile(sourcePath, targetPath, folderName ?? destinationName, confirmOverwrite);
        }

        internal static void ReplaceFile(string sourcePath, string targetPath, string folderName = null, bool confirmOverwrite = false)
        {
            string destinationName = new DirectoryInfo(Path.GetDirectoryName(targetPath)!).Name;
            folderName ??= destinationName;
            if (File.Exists(targetPath))
            {
                if (confirmOverwrite)
                {
                    var result = MessageBox.Show($"This file already exists in your {folderName} folder, do you want to overwrite it?", "Overwrite file?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        throw new OverwriteCancelledException();
                    }
                }

                File.Delete(targetPath);
            }

            File.Copy(sourcePath, targetPath);
        }

        [Serializable]
        public class OverwriteCancelledException : Exception
        {
            public OverwriteCancelledException() : base("File overwrite operation cancelled by user")
            {
            }

            public OverwriteCancelledException(string message) : base(message)
            {
            }

            public OverwriteCancelledException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected OverwriteCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
