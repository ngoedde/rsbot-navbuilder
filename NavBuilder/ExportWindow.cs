using NavBuilder.Client;
using NavBuilder.Core.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NavBuilder.Core;
using NavBuilder.Core.Map;

namespace NavBuilder
{
    public partial class ExportWindow : Form
    {
        #region Fields

        private string[] _files;

        private CollisionExporter _exporter;

        private Thread _exportThread;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportWindow"/> class.
        /// </summary>
        public ExportWindow()
        {
            InitializeComponent();

            var files = Pk2Controller.Data.Files.Where(f => f.Name.EndsWith(".nvm")).Select(f => f.Name).ToArray();

            var activeFiles = new List<string>();

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                var regionId = int.Parse(fileName.Split('_')[1], NumberStyles.HexNumber);
           
                if (MapInfoManager.IsRegionActive(regionId))
                    activeFiles.Add(file);
            }

            _files = activeFiles.ToArray();

            outputPathBrowser.SelectedPath = Globals.CurrentProject.Path + "\\export\\map.rsc";

            progressMain.Maximum = _files.Length;
        }

        #endregion Constructor

        #region Form Events

        /// <summary>
        /// Handles the LoadMapInfo event of the ExportWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExportWindow_Load(object sender, EventArgs e)
        {
            PopulateFileList();
        }

        /// <summary>
        /// Handles the Click event of the btnStartExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStartExport_Click(object sender, EventArgs e)
        {
            if (btnStartExport.Text == "Start export")
            {
                btnStartExport.Text = "Cancel export";
                progressMain.Show();
                _exportThread = new Thread(StartExport);
                _exportThread.Start();
            }
            else
            {
                _exportThread.Abort();
                _exporter.Abort();

                btnStartExport.Text = "Start export";
                progressMain.Hide();

                lvFiles.BeginUpdate();
                lvFiles.Items.Clear();
                lvFiles.EndUpdate();

                PopulateFileList();
            }
        }

        #endregion Form Events

        #region Methods

        /// <summary>
        /// Populates the file list.
        /// </summary>
        private void PopulateFileList()
        {
            var listViewItems = new ListViewItem[_files.Length];

            var index = 0;
            foreach (var file in _files)
            {
                var listItem = new ListViewItem(file);
                listItem.SubItems.Add("Waiting");
                listItem.SubItems.Add("");
                listViewItems[index] = listItem;

                index++;
            }

            lvFiles.BeginUpdate();
            lvFiles.Items.AddRange(listViewItems);
            lvFiles.EndUpdate();
        }

        /// <summary>
        /// Starts the export.
        /// </summary>
        private void StartExport()
        {
            outputPathBrowser.Enabled = false;

            _exporter = new CollisionExporter(outputPathBrowser.SelectedPath, Path.Combine(Path.GetDirectoryName(outputPathBrowser.SelectedPath), Path.GetFileNameWithoutExtension(outputPathBrowser.SelectedPath) + ".rsci"));

            _exporter.BeginFileExport += _exporter_BeginFileExport;
            _exporter.EndFileExport += _exporter_EndFileExport;

            _exporter.ExportFiles(_files);
        }

        /// <summary>
        /// Gets the current fileName item.
        /// </summary>
        /// <param name="currentFile">The current fileName.</param>
        /// <returns></returns>
        private ListViewItem GetCurrentFileItem(string currentFile)
        {
            if (lvFiles.Items == null || lvFiles.IsDisposed || lvFiles.Disposing) return null;

            return lvFiles.FindItemWithText(currentFile);
        }

        #endregion Methods

        #region Exporter events

        /// <summary>
        /// Fired when the exporter has exported a file.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="message">The message.</param>
        private void _exporter_EndFileExport(bool success, string fileName, string message)
        {
            var lvItem = GetCurrentFileItem(fileName);
            if (lvItem == null) return;

            if (success)
            {
                lvItem.BackColor = Color.Lime;
                lvItem.SubItems[1].Text = "OK";
                lvItem.SubItems[2].Text = message;
            }
            else
            {
                lvItem.BackColor = Color.Red;
                lvItem.SubItems[1].Text = "FAILED";
                lvItem.SubItems[2].Text = message;
            }

            progressMain.Value++;

            if (lvItem.Index != _files.Length - 1) return;

            progressMain.Hide();
            btnStartExport.Text = "Start export";
        }

        /// <summary>
        /// Fired when the exporter begins to export a file
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void _exporter_BeginFileExport(string fileName)
        {
            var lvItem = GetCurrentFileItem(fileName);
            if (lvItem == null) return;

            lvFiles.EnsureVisible(lvItem.Index);

            lvItem.SubItems[1].Text = "...";
        }

        private void ExportWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_exportThread != null)
                _exportThread.Abort();
        }

        #endregion Exporter events

    }
}