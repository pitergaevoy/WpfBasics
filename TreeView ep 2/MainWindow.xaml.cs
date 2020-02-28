using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp_TreeView_Learn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructor

        ///<summary>
        ///DefaultConstructor
        /// </summary>



        public MainWindow()
        {

        }

        #endregion

        #region OnLoaded

        /// <summary>
        /// When application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem()
                {
                    //Set the header
                    Header = drive,
                    //Add the full path
                    Tag = drive
                };

                //Empty Item
                item.Items.Add(null);

                //Listen out for the item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        /// <summary>
        /// When a folder is Expanded, finds sub-folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            var item = (TreeViewItem)sender;

            //If the item only contains dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();


            //Get full path
            var fullPath = (string)item.Tag;
            #endregion      

            #region Get Directories

            //Create a blank list for directories
            var directories = new List<string>();

            //Try and get directories from the folder
            //Ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //For each directory
            directories.ForEach((Action<string>)(directoryPath =>
            {
                //Create directory item
                var subItem = new TreeViewItem()
                {
                    // Just a name of the folder
                    Header = GetFileFolderName(directoryPath),
                    // Is an entire path
                    Tag = directoryPath
                };

                //Another dummy Item
                subItem.Items.Add(null);

                //Expander handler
                subItem.Expanded += Folder_Expanded;

                //Add this item to folder
                item.Items.Add(subItem);
            }));
            #endregion

            #region Get Files
            //Create a blank list for files
            var files = new List<string>();

            //Try and get files from the folder
            //Ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);
               

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //For each file
            files.ForEach((Action<string>)(filePath =>
            { 
                //Create file item
                var subItem = new TreeViewItem()
                {
                    // Just a name of the file
                    Header = GetFileFolderName(filePath),
                    // Is an entire path
                    Tag = filePath
                };

                //Add this item to folder
                item.Items.Add(subItem);
            }));


            #endregion
        }
        #endregion

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


