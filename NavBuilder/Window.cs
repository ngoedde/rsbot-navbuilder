using NavBuilder.Client;
using NavBuilder.Core;
using NavBuilder.Core.Map;
using NavBuilder.Core.Mesh;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavBuilder
{
    internal struct ResourceSummary
    {
        public int FileCount;
        public long TotalSize;
        public long TotalTime;
        public List<string> Files;
    }

    public partial class Window : Form
    {
        #region Constants

        private readonly string[] _defaultRegions = new[] { "25000|Jangan", "26265|Donwhang", "23687|Hotan", "27244|Samarkand", "26959|Constantinople", "23603|Alexandria (North)", "23088|Alexandria (South)" };

        #endregion Constants

        #region Fields

        private ResourceSummary _resourceSummary;
        private bool _isLoadingRegion;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        public Window()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            Size = new Size(1034, 912);

            map.RegionLoaded += Map_RegionLoaded;
            map.BeforeLoadRegion += Map_BeforeLoadRegion;
            FileEvents.OnFileLoaded += FileEvents_OnFileLoaded;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Creates the project.
        /// </summary>
        private void CreateProject()
        {
            var newProjectDiag = new NewProjectWindow();

            if (newProjectDiag.ShowDialog() != DialogResult.OK) return;

            var configFilePath = Path.Combine(newProjectDiag.SavePath, Project.ConfigFileName);
            File.Create(Path.Combine(newProjectDiag.SavePath, Project.ConfigFileName)).Dispose();

            var projectConfig = new Config(configFilePath);

            projectConfig.Set("NavBuilder.Name", newProjectDiag.ProjectName);
            projectConfig.Set("NavBuilder.SilkroadPath", newProjectDiag.SilkroadPath);
            projectConfig.SetArray("NavBuilder.SavedRegions", _defaultRegions);
            projectConfig.Save();

            var project = new Project { Config = projectConfig };

            if (Globals.CurrentProject == null)
            {
                Globals.CurrentProject = project;

                ReloadCurrentProject();
            }
        }

        /// <summary>
        /// Reloads the current project.
        /// </summary>
        private void ReloadCurrentProject()
        {
            File.WriteAllText(Environment.CurrentDirectory + "\\last.dat", Globals.CurrentProject.Path);
            Cursor = Cursors.WaitCursor;
            statusBarMain.Panels[0].Text = $@"Loading project {Globals.CurrentProject.Name} ...";

            var mediaPk2Path = Path.Combine(Globals.CurrentProject.SilkroadPath,
                Globals.CurrentProject.Config.Get<string>("NavBuilder.Media", "media.pk2"));

            var dataPk2Path = Path.Combine(Globals.CurrentProject.SilkroadPath,
                Globals.CurrentProject.Config.Get<string>("NavBuilder.Data", "data.pk2"));

            var mapPk2Path = Path.Combine(Globals.CurrentProject.SilkroadPath,
                Globals.CurrentProject.Config.Get<string>("NavBuilder.Data", "data.pk2"));

            var taskList = new Task[3];
            taskList[0] = Task.Run(() => Pk2Controller.LoadMediaArchive(mediaPk2Path));
            taskList[1] = Task.Run(() => Pk2Controller.LoadDataArchive(dataPk2Path));
            taskList[2] = Task.Run(() => Pk2Controller.LoadMapArchive(mapPk2Path));

            Task.WaitAll(taskList);

            ObjectIndexManager.LoadObjectIndex();
            MapInfoManager.LoadMapInfo();

            statusBarMain.Panels[0].Text = $@"Project: {Globals.CurrentProject.Name}";

            var externalFileName = Globals.CurrentProject.Config.Get<string>("NavBuilder.ExternalCollisionFile");
            var loadColFromFile = Globals.CurrentProject.Config.Get<bool>("NavBuilder.UseExternalCollisionFile", "false");
            if (!File.Exists(externalFileName))
            {
                radioFile.Enabled = false;
                radioFile.Text = "File not found!";
                loadColFromFile = false;
            }
            else
            {
                radioFile.Text = Path.GetFileName(externalFileName);
                radioFile.Enabled = true;
            }

            var lastRegion = Globals.CurrentProject.Config.Get<ushort>("NavBuilder.LastRegionId", "0");

            if (lastRegion == 0)
                map.LoadRegion(new Core.Map.Region(113, 89));
            else
                map.LoadRegion(new Core.Map.Region(lastRegion));

            btnLoadRoadsharkCollision.Enabled = true;
            comboSavedRegions.Enabled = true;

            var savedRegions = Globals.CurrentProject.Config.GetArray<string>("NavBuilder.SavedRegions");
            comboSavedRegions.Items.Clear();
            foreach (var savedRegion in savedRegions)
                comboSavedRegions.Items.Add(savedRegion.Split('|')[1]);

            comboSavedRegions.Enabled = true;
            radioFile.Checked = loadColFromFile;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Browses the project.
        /// </summary>
        private void BrowseProject()
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = @"Please select a project directory..."
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Globals.CurrentProject = Project.Load(folderBrowserDialog.SelectedPath);

                Task.Run(() => ReloadCurrentProject());
            }
        }

        /// <summary>
        /// Goes up.
        /// </summary>
        private void GoUp()
        {
            if (Globals.CurrentProject != null && map.CenterRegion != null)
                map.LoadRegion(map.Regions[1]);
        }

        /// <summary>
        /// Goes the left.
        /// </summary>
        private void GoLeft()
        {
            if (Globals.CurrentProject != null && map.CenterRegion != null)
                map.LoadRegion(map.Regions[3]);
        }

        /// <summary>
        /// Goes the right.
        /// </summary>
        private void GoRight()
        {
            if (Globals.CurrentProject != null && map.CenterRegion != null)
                map.LoadRegion(map.Regions[5]);
        }

        /// <summary>
        /// Goes down.
        /// </summary>
        private void GoDown()
        {
            if (Globals.CurrentProject != null && map.CenterRegion != null)
                map.LoadRegion(map.Regions[7]);
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the menuNewProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuNewProject_Click(object sender, EventArgs e)
        {
            CreateProject();
        }

        /// <summary>
        /// Handles the Click event of the menuLoadProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuLoadProject_Click(object sender, EventArgs e)
        {
            BrowseProject();
        }

        /// <summary>
        /// Handles the Click event of the menuRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuRefresh_Click(object sender, EventArgs e)
        {
            map.LoadRegion(map.CenterRegion);
        }

        /// <summary>
        /// Handles the Click event of the menuLoadLast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuLoadLast_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\last.dat"))
            {
                MessageBox.Show("Could not find a last project.", "No last project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var last = File.ReadAllText(Environment.CurrentDirectory + "\\last.dat");

            if (!Directory.Exists(last))
            {
                MessageBox.Show("Could not load last directory: It does not exist", "No last directory",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            Globals.CurrentProject = Project.Load(last);

            Task.Run(() => ReloadCurrentProject());
        }

        /// <summary>
        /// Handles the Click event of the btnLeft control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            GoLeft();
        }

        /// <summary>
        /// Handles the Click event of the btnUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            GoUp();
        }

        /// <summary>
        /// Handles the Click event of the btnRight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            GoRight();
        }

        /// <summary>
        /// Handles the Click event of the btnDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            GoDown();
        }

        /// <summary>
        /// Handles the Click event of the menuExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuExport_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentProject == null) return;

            var exporter = new ExportWindow();
            exporter.Show();
        }

        private void btnLoadRoadsharkCollision_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentProject == null) return;

            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Roadshark collision map (*.rsc)|*.rsc",
                Title = "Open existing Roadshark collision file...",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var fileName = openFileDialog.FileName;

            map.LoadCollisionsFromFile(fileName);
            radioFile.Text = Path.GetFileName(fileName);
            radioFile.Enabled = true;
            radioFile.Checked = true;

            Globals.CurrentProject.Config.Set("NavBuilder.ExternalCollisionFile", fileName);
        }

        private void radioFile_CheckedChanged(object sender, EventArgs e)
        {
            Globals.CurrentProject.Config.Set("NavBuilder.UseExternalCollisionFile", radioFile.Checked);

            if (radioFile.Checked)
            {
                map.LoadCollisionsFromFile(Globals.CurrentProject.Config.Get<string>("NavBuilder.ExternalCollisionFile"));
                map.LoadRegion(map.CenterRegion);
                return;
            }

            map.LoadCollisionsFromFile(null);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Globals.CurrentProject != null)
            {
                if (map.Regions.Length > 4)
                    Globals.CurrentProject.Config.Set("NavBuilder.LastRegionId", map.Regions[4].Id);

                Globals.CurrentProject.Save();
            }
        }

        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentProject == null) return;

            var newRegionDiag = new NewRegionDialog();

            if (newRegionDiag.ShowDialog() != DialogResult.OK) return;

            var savedRegions = Globals.CurrentProject.Config.GetArray<string>("NavBuilder.SavedRegions");
            var newSavedRegions = new string[savedRegions.Length + 1];
            savedRegions.CopyTo(newSavedRegions, 0);

            newSavedRegions[savedRegions.Length] = $"{map.Regions[4].Id}|{newRegionDiag.RegionName}";

            comboSavedRegions.Items.Clear();

            foreach (var savedRegion in newSavedRegions)
            {
                comboSavedRegions.Items.Add(savedRegion.Split('|')[1]);
            }

            comboSavedRegions.SelectedIndex = comboSavedRegions.Items.Count - 1;

            Globals.CurrentProject.Config.SetArray("NavBuilder.SavedRegions", newSavedRegions);
        }

        private void comboSavedRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSavedRegions.SelectedIndex < 0) return;

            var savedRegions = Globals.CurrentProject.Config.GetArray<string>("NavBuilder.SavedRegions");

            if (savedRegions.Length <= comboSavedRegions.SelectedIndex) return;

            var selectedRegion = savedRegions[comboSavedRegions.SelectedIndex];
            ushort.TryParse(selectedRegion.Split('|')[0], out var selectedRegionId);

            if (selectedRegionId == 0) return;

            map.LoadRegion(new Core.Map.Region(selectedRegionId));
        }

        private void Map_BeforeLoadRegion(Core.Map.Region region)
        {
            statusCurrentProject.Text = $"Loading region 0x{region.Id:X4}  ...";
            lvResources.Hide();

            _resourceSummary = new ResourceSummary
            {
                Files = new List<string>()
            };

            lvResources.Items.Clear();

            _isLoadingRegion = true;
        }

        private void FileEvents_OnFileLoaded(FileEvents.ResourceGroup group, string path, long size, long loadTimeMs, object propertyInfo)
        {
            if (!_isLoadingRegion) return;

            lock (lvResources)
            {
                var duplication = _resourceSummary.Files.Contains(path);
                if (duplication)
                    return;

                _resourceSummary.FileCount++;
                _resourceSummary.TotalSize += size;
                _resourceSummary.TotalTime += loadTimeMs;
                _resourceSummary.Files.Add(path);

                var fromMemory = loadTimeMs == 0;

                var item = new ListViewItem(path);
                if (fromMemory)
                    item.BackColor = Color.LightYellow;

                item.SubItems.Add(size / 1024 < 1 ? size + "B" : size / 1024 + "K");
                item.SubItems.Add(fromMemory ? "[Memory]" : loadTimeMs.ToString() + "ms");
                item.SubItems.Add("-");

                item.Tag = propertyInfo;

                lvResources.Items.Add(item);
            }
        }

        /// <summary>
        /// Maps the region loaded.
        /// </summary>
        /// <param name="region">The region.</param>
        private void Map_RegionLoaded(Core.Map.Region region)
        {
            lblRegionId.Text = $"{region.Id} ({region.XSector}, {region.YSector})";

            var cells = 0;
            var objects = 0;

            foreach (var loadedRegion in map.Regions)
            {
                if (loadedRegion.Terrain == null) continue;
                cells += loadedRegion.Terrain.Cells.Count;
                objects += loadedRegion.Terrain.Objects.Count;
            }

            lblNavmeshName.Text = region.NavmeshFileName;
            lblCellCount.Text = cells.ToString();
            lblObjectCount.Text = objects.ToString();
            lblLoadTime.Text = $"{_resourceSummary.TotalTime / 1000}s ({_resourceSummary.TotalTime}ms)";
            lblResourceSummary.Text = $"{_resourceSummary.FileCount} files, {_resourceSummary.TotalSize / 1024} KB, {_resourceSummary.TotalTime / 1000}s ({_resourceSummary.TotalTime}ms)";

            statusCurrentProject.Text = $"Idle";
            lvResources.Show();

            _isLoadingRegion = false;
        }

        private void lvResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvResources.SelectedItems == null || lvResources.SelectedItems.Count != 1) return;

            propGridResource.SelectedObject = lvResources.SelectedItems[0].Tag;
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ngoedde/rsbot-navbuilder");
        }

        private void picRSBot_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/SDClowen/RSBot");
        }

        #endregion Events
    }
}