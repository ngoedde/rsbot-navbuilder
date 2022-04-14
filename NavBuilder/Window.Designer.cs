namespace NavBuilder
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Navmesh", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Object", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Minimap", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Resources", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.menuMain = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuLoadLast = new System.Windows.Forms.MenuItem();
            this.menuNewProject = new System.Windows.Forms.MenuItem();
            this.menuLoadProject = new System.Windows.Forms.MenuItem();
            this.manuSaveProject = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuRefresh = new System.Windows.Forms.MenuItem();
            this.menuExport = new System.Windows.Forms.MenuItem();
            this.statusBarMain = new System.Windows.Forms.StatusBar();
            this.statusCurrentProject = new System.Windows.Forms.StatusBarPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAddRegion = new System.Windows.Forms.Label();
            this.comboSavedRegions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblObjectCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNavmeshName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblLoadTime = new System.Windows.Forms.Label();
            this.lblCellCount = new System.Windows.Forms.Label();
            this.lblRegionId = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadRoadsharkCollision = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.radioFile = new System.Windows.Forms.RadioButton();
            this.radioLive = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.map = new NavBuilder.UI.MapCanvas();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvResources = new System.Windows.Forms.ListView();
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLoadTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.propGridResource = new System.Windows.Forms.PropertyGrid();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblResourceSummary = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.linkGithub = new System.Windows.Forms.LinkLabel();
            this.picRSBot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.statusCurrentProject)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRSBot)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3,
            this.menuExport});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.manuSaveProject});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuLoadLast,
            this.menuNewProject,
            this.menuLoadProject});
            this.menuItem2.Text = "Project";
            // 
            // menuLoadLast
            // 
            this.menuLoadLast.Index = 0;
            this.menuLoadLast.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.menuLoadLast.Text = "Open last project";
            this.menuLoadLast.Click += new System.EventHandler(this.menuLoadLast_Click);
            // 
            // menuNewProject
            // 
            this.menuNewProject.Index = 1;
            this.menuNewProject.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuNewProject.Text = "New...";
            this.menuNewProject.Click += new System.EventHandler(this.menuNewProject_Click);
            // 
            // menuLoadProject
            // 
            this.menuLoadProject.Index = 2;
            this.menuLoadProject.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuLoadProject.Text = "Open...";
            this.menuLoadProject.Click += new System.EventHandler(this.menuLoadProject_Click);
            // 
            // manuSaveProject
            // 
            this.manuSaveProject.Enabled = false;
            this.manuSaveProject.Index = 1;
            this.manuSaveProject.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.manuSaveProject.Text = "Save project";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuRefresh});
            this.menuItem3.Text = "View";
            // 
            // menuRefresh
            // 
            this.menuRefresh.Index = 0;
            this.menuRefresh.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuRefresh.Text = "Refresh";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // menuExport
            // 
            this.menuExport.Index = 2;
            this.menuExport.Text = "Export";
            this.menuExport.Click += new System.EventHandler(this.menuExport_Click);
            // 
            // statusBarMain
            // 
            this.statusBarMain.Location = new System.Drawing.Point(0, 843);
            this.statusBarMain.Name = "statusBarMain";
            this.statusBarMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusCurrentProject});
            this.statusBarMain.ShowPanels = true;
            this.statusBarMain.Size = new System.Drawing.Size(1014, 22);
            this.statusBarMain.SizingGrip = false;
            this.statusBarMain.TabIndex = 1;
            this.statusBarMain.Text = "Idle";
            // 
            // statusCurrentProject
            // 
            this.statusCurrentProject.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusCurrentProject.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
            this.statusCurrentProject.MinWidth = 100;
            this.statusCurrentProject.Name = "statusCurrentProject";
            this.statusCurrentProject.Text = "Idle";
            this.statusCurrentProject.Width = 1014;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.btnAddRegion);
            this.panel2.Controls.Add(this.comboSavedRegions);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnUp);
            this.panel2.Controls.Add(this.btnLeft);
            this.panel2.Controls.Add(this.btnRight);
            this.panel2.Controls.Add(this.btnDown);
            this.panel2.Location = new System.Drawing.Point(803, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 200);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 196);
            this.panel4.TabIndex = 8;
            // 
            // btnAddRegion
            // 
            this.btnAddRegion.AutoSize = true;
            this.btnAddRegion.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAddRegion.Location = new System.Drawing.Point(128, 26);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(65, 13);
            this.btnAddRegion.TabIndex = 7;
            this.btnAddRegion.Text = "Add region";
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
            // 
            // comboSavedRegions
            // 
            this.comboSavedRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSavedRegions.Enabled = false;
            this.comboSavedRegions.FormattingEnabled = true;
            this.comboSavedRegions.Location = new System.Drawing.Point(9, 42);
            this.comboSavedRegions.Name = "comboSavedRegions";
            this.comboSavedRegions.Size = new System.Drawing.Size(185, 21);
            this.comboSavedRegions.TabIndex = 6;
            this.comboSavedRegions.SelectedIndexChanged += new System.EventHandler(this.comboSavedRegions_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Navigation";
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(79, 83);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "N ";
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.Location = new System.Drawing.Point(49, 113);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(32, 32);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "W ";
            this.btnLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRight.Location = new System.Drawing.Point(109, 113);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(32, 32);
            this.btnRight.TabIndex = 1;
            this.btnRight.Text = "E";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(79, 143);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "S  ";
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lblObjectCount);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lblNavmeshName);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.lblLoadTime);
            this.panel3.Controls.Add(this.lblCellCount);
            this.panel3.Controls.Add(this.lblRegionId);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(803, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 199);
            this.panel3.TabIndex = 3;
            // 
            // lblObjectCount
            // 
            this.lblObjectCount.AutoSize = true;
            this.lblObjectCount.Location = new System.Drawing.Point(81, 150);
            this.lblObjectCount.Name = "lblObjectCount";
            this.lblObjectCount.Size = new System.Drawing.Size(13, 13);
            this.lblObjectCount.TabIndex = 19;
            this.lblObjectCount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Objects:";
            // 
            // lblNavmeshName
            // 
            this.lblNavmeshName.AutoSize = true;
            this.lblNavmeshName.Location = new System.Drawing.Point(81, 75);
            this.lblNavmeshName.Name = "lblNavmeshName";
            this.lblNavmeshName.Size = new System.Drawing.Size(43, 13);
            this.lblNavmeshName.TabIndex = 17;
            this.lblNavmeshName.Text = "<null>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Navmesh:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(4, 195);
            this.panel5.TabIndex = 15;
            // 
            // lblLoadTime
            // 
            this.lblLoadTime.AutoSize = true;
            this.lblLoadTime.Location = new System.Drawing.Point(81, 103);
            this.lblLoadTime.Name = "lblLoadTime";
            this.lblLoadTime.Size = new System.Drawing.Size(13, 13);
            this.lblLoadTime.TabIndex = 14;
            this.lblLoadTime.Text = "0";
            // 
            // lblCellCount
            // 
            this.lblCellCount.AutoSize = true;
            this.lblCellCount.Location = new System.Drawing.Point(81, 127);
            this.lblCellCount.Name = "lblCellCount";
            this.lblCellCount.Size = new System.Drawing.Size(13, 13);
            this.lblCellCount.TabIndex = 13;
            this.lblCellCount.Text = "0";
            // 
            // lblRegionId
            // 
            this.lblRegionId.AutoSize = true;
            this.lblRegionId.Location = new System.Drawing.Point(81, 49);
            this.lblRegionId.Name = "lblRegionId";
            this.lblRegionId.Size = new System.Drawing.Size(13, 13);
            this.lblRegionId.TabIndex = 10;
            this.lblRegionId.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Load time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cells:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "SectorId:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Diagnostics";
            // 
            // btnLoadRoadsharkCollision
            // 
            this.btnLoadRoadsharkCollision.Enabled = false;
            this.btnLoadRoadsharkCollision.Location = new System.Drawing.Point(21, 96);
            this.btnLoadRoadsharkCollision.Name = "btnLoadRoadsharkCollision";
            this.btnLoadRoadsharkCollision.Size = new System.Drawing.Size(132, 23);
            this.btnLoadRoadsharkCollision.TabIndex = 7;
            this.btnLoadRoadsharkCollision.Text = "Open collision file...";
            this.btnLoadRoadsharkCollision.UseVisualStyleBackColor = true;
            this.btnLoadRoadsharkCollision.Click += new System.EventHandler(this.btnLoadRoadsharkCollision_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.radioFile);
            this.panel1.Controls.Add(this.radioLive);
            this.panel1.Controls.Add(this.btnLoadRoadsharkCollision);
            this.panel1.Location = new System.Drawing.Point(803, 440);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 131);
            this.panel1.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(4, 127);
            this.panel6.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Render";
            // 
            // radioFile
            // 
            this.radioFile.AutoSize = true;
            this.radioFile.Enabled = false;
            this.radioFile.Location = new System.Drawing.Point(21, 73);
            this.radioFile.Name = "radioFile";
            this.radioFile.Size = new System.Drawing.Size(75, 17);
            this.radioFile.TabIndex = 9;
            this.radioFile.Text = "Collisions";
            this.radioFile.UseVisualStyleBackColor = true;
            this.radioFile.CheckedChanged += new System.EventHandler(this.radioFile_CheckedChanged);
            // 
            // radioLive
            // 
            this.radioLive.AutoSize = true;
            this.radioLive.Checked = true;
            this.radioLive.Location = new System.Drawing.Point(21, 50);
            this.radioLive.Name = "radioLive";
            this.radioLive.Size = new System.Drawing.Size(94, 17);
            this.radioLive.TabIndex = 8;
            this.radioLive.TabStop = true;
            this.radioLive.Text = "Game objects";
            this.radioLive.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(789, 815);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.map);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(781, 789);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // map
            // 
            this.map.BackColor = System.Drawing.Color.Black;
            this.map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map.Location = new System.Drawing.Point(6, 10);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(768, 768);
            this.map.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvResources);
            this.tabPage2.Controls.Add(this.propGridResource);
            this.tabPage2.Controls.Add(this.panel7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(781, 789);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Resources";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvResources
            // 
            this.lvResources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile,
            this.colSize,
            this.colLoadTime});
            this.lvResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResources.FullRowSelect = true;
            listViewGroup1.Header = "Navmesh";
            listViewGroup1.Name = "lvGroupNavmeshFiles";
            listViewGroup1.Tag = "0";
            listViewGroup2.Header = "Object";
            listViewGroup2.Name = "lvGroupObjects";
            listViewGroup2.Tag = "1";
            listViewGroup3.Header = "Minimap";
            listViewGroup3.Name = "lvGroupMinimap";
            listViewGroup3.Tag = "2";
            listViewGroup4.Header = "Resources";
            listViewGroup4.Name = "lvGroupResources";
            this.lvResources.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.lvResources.HideSelection = false;
            this.lvResources.Location = new System.Drawing.Point(3, 3);
            this.lvResources.MultiSelect = false;
            this.lvResources.Name = "lvResources";
            this.lvResources.Size = new System.Drawing.Size(775, 621);
            this.lvResources.TabIndex = 0;
            this.lvResources.UseCompatibleStateImageBehavior = false;
            this.lvResources.View = System.Windows.Forms.View.Details;
            this.lvResources.SelectedIndexChanged += new System.EventHandler(this.lvResources_SelectedIndexChanged);
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            this.colFile.Width = 366;
            // 
            // colSize
            // 
            this.colSize.Text = "Size (Bytes)";
            this.colSize.Width = 140;
            // 
            // colLoadTime
            // 
            this.colLoadTime.Text = "Time (ms)";
            this.colLoadTime.Width = 126;
            // 
            // propGridResource
            // 
            this.propGridResource.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.propGridResource.HelpVisible = false;
            this.propGridResource.Location = new System.Drawing.Point(3, 624);
            this.propGridResource.Name = "propGridResource";
            this.propGridResource.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propGridResource.Size = new System.Drawing.Size(775, 130);
            this.propGridResource.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Control;
            this.panel7.Controls.Add(this.lblResourceSummary);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(3, 754);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(775, 32);
            this.panel7.TabIndex = 2;
            // 
            // lblResourceSummary
            // 
            this.lblResourceSummary.AutoSize = true;
            this.lblResourceSummary.Location = new System.Drawing.Point(67, 9);
            this.lblResourceSummary.Name = "lblResourceSummary";
            this.lblResourceSummary.Size = new System.Drawing.Size(87, 13);
            this.lblResourceSummary.TabIndex = 11;
            this.lblResourceSummary.Text = "0 files, 0 B, 0 ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Summary";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(779, 848);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "2022 © wimbeam";
            // 
            // linkGithub
            // 
            this.linkGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkGithub.AutoSize = true;
            this.linkGithub.Location = new System.Drawing.Point(881, 848);
            this.linkGithub.Name = "linkGithub";
            this.linkGithub.Size = new System.Drawing.Size(128, 13);
            this.linkGithub.TabIndex = 11;
            this.linkGithub.TabStop = true;
            this.linkGithub.Text = "View project on GitHub";
            this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
            // 
            // picRSBot
            // 
            this.picRSBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picRSBot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRSBot.Image = global::NavBuilder.Properties.Resources.logo2;
            this.picRSBot.Location = new System.Drawing.Point(798, 721);
            this.picRSBot.Name = "picRSBot";
            this.picRSBot.Size = new System.Drawing.Size(211, 97);
            this.picRSBot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picRSBot.TabIndex = 12;
            this.picRSBot.TabStop = false;
            this.picRSBot.Click += new System.EventHandler(this.picRSBot_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1014, 865);
            this.Controls.Add(this.picRSBot);
            this.Controls.Add(this.linkGithub);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusBarMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.menuMain;
            this.Name = "Window";
            this.Text = "RSBot NavBuilder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.statusCurrentProject)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRSBot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MainMenu menuMain;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuNewProject;
        private System.Windows.Forms.MenuItem menuLoadProject;
        private System.Windows.Forms.StatusBar statusBarMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLoadTime;
        private System.Windows.Forms.Label lblCellCount;
        private System.Windows.Forms.Label lblRegionId;
        private UI.MapCanvas map;
        private System.Windows.Forms.MenuItem manuSaveProject;
        private System.Windows.Forms.StatusBarPanel statusCurrentProject;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuRefresh;
        private System.Windows.Forms.MenuItem menuLoadLast;
        private System.Windows.Forms.MenuItem menuExport;
        private System.Windows.Forms.Button btnLoadRoadsharkCollision;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioFile;
        private System.Windows.Forms.RadioButton radioLive;
        private System.Windows.Forms.ComboBox comboSavedRegions;
        private System.Windows.Forms.Label btnAddRegion;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblNavmeshName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblObjectCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvResources;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colLoadTime;
        private System.Windows.Forms.PropertyGrid propGridResource;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblResourceSummary;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkGithub;
        private System.Windows.Forms.PictureBox picRSBot;
    }
}

