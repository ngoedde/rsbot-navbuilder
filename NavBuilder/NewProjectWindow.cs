using System;
using System.Windows.Forms;

namespace NavBuilder
{
    public partial class NewProjectWindow : Form
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the save path.
        /// </summary>
        /// <value>
        /// The save path.
        /// </value>
        public string SavePath { get; set; }

        /// <summary>
        /// Gets or sets the silkroad path.
        /// </summary>
        /// <value>
        /// The silkroad path.
        /// </value>
        public string SilkroadPath { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectWindow"/> class.
        /// </summary>
        public NewProjectWindow()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            ProjectName = txtProjectName.Text;
            SilkroadPath = pathBrowserSilkroad.SelectedPath;
            SavePath = pathBrowserProject.SelectedPath;
        }

        #endregion
    }
}