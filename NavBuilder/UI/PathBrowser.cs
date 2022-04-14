using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NavBuilder.UI
{
    public enum BrowsingType
    {
        File,
        Directory
    }

    public partial class PathBrowser : UserControl
    {
        #region Fields

        /// <summary>
        /// The selected path
        /// </summary>
        private string _selectedPath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the browsing.
        /// </summary>
        /// <value>
        /// The type of the browsing.
        /// </value>
        [Browsable(true)]
        public BrowsingType BrowsingType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Browsable(true)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow new]; otherwise, <c>false</c>.
        /// </value>
        [Browsable(true)]
        public bool AllowNew { get; set; }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        /// <value>
        /// The selected path.
        /// </value>
        [Browsable(false)]
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                txtPath.Text = value;
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PathBrowser"/> class.
        /// </summary>
        public PathBrowser()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Creates the browser.
        /// </summary>
        private void CreateBrowser()
        {
            if (BrowsingType == BrowsingType.Directory)
                BrowseForDirectory();
            else if (BrowsingType == BrowsingType.File)
                BrowseForFile();
        }

        /// <summary>
        /// Browses for directory.
        /// </summary>
        private void BrowseForDirectory()
        {
            var folderBrowserDiag = new FolderBrowserDialog
            {
                Description = Description,
                ShowNewFolderButton = AllowNew
            };

            if (folderBrowserDiag.ShowDialog() == DialogResult.OK)
                SelectedPath = folderBrowserDiag.SelectedPath;
        }

        /// <summary>
        /// Browses for file.
        /// </summary>
        private void BrowseForFile()
        {
            var fileBrowserDiag = new SaveFileDialog();

            if (fileBrowserDiag.ShowDialog() == DialogResult.OK)
                SelectedPath = fileBrowserDiag.FileName;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the btnBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            CreateBrowser();
        }

        #endregion
    }
}