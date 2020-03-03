using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfApp_TreeView_Learn
{
    /// <summary>
    /// A helper class to query information directories
    /// </summary>
    public class DirectoryStructure
    {
        public static List<DirectoryItem> GetlogicalDrives()
        {
            //Get every logical drive on machine
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem() {FullPath = drive, Type = DirectoryItemType.Drive}).ToList();
            { 
                
            }
        }

        #region Helpers

        /// <summary>
        ///Find the file or folder name from full path
        /// </summary>
        public static string GetFileFolderName(string path)
        {
            //C:/something/a file.png

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //Make all slashes into back-slashes
            var normalizedPath = path.Replace('/', '\\');

            //Find last back-slash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //Q: Is there is no back-slash? A: return the path itself.
            if (lastIndex <= 0)
                return path;

            //Return name after last back-slash
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
