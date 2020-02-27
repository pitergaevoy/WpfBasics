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
                var item = new TreeViewItem();
                
                //St the header and path
                item.Header = drive;
                item.Tag = drive;

                //Empty Item
                item.Items.Add(null);

                //Add it ti the main tree-view
                FolderView.Items.Add(item);
            }
        }
        #endregion
    }
}
