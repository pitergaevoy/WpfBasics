namespace WpfApp_TreeView_Learn
{
    /// <summary>
    /// Information about directory drive or a folder
    /// </summary>
    public class DirectoryItem
    {
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name => DirectoryStructure.GetFileFolderName(this.FullPath);
    }
}
