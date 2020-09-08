namespace ObfuscarMappingParser
{
  partial class MainForm
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
      BrokenEvent.Shared.Controls.PineappleTreeColumn pineappleTreeColumn1 = new BrokenEvent.Shared.Controls.PineappleTreeColumn();
      BrokenEvent.Shared.Controls.PineappleTreeColumn pineappleTreeColumn2 = new BrokenEvent.Shared.Controls.PineappleTreeColumn();
      this.odMapping = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.spbLoading = new System.Windows.Forms.ToolStripProgressBar();
      this.slblSelected = new System.Windows.Forms.ToolStripStatusLabel();
      this.slblModule = new System.Windows.Forms.ToolStripStatusLabel();
      this.slblType = new System.Windows.Forms.ToolStripStatusLabel();
      this.ilIcons = new System.Windows.Forms.ImageList(this.components);
      this.tsTools = new System.Windows.Forms.ToolStrip();
      this.btnOpen = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.miOpenBtn = new System.Windows.Forms.ToolStripMenuItem();
      this.btnCrashLogs = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.lblSearch = new System.Windows.Forms.ToolStripLabel();
      this.tbSearch = new System.Windows.Forms.ToolStripTextBox();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.mmFile = new System.Windows.Forms.ToolStripMenuItem();
      this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.miRecents = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.miReload = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.miManagePDBs = new System.Windows.Forms.ToolStripMenuItem();
      this.miAttachPDB = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.miExit = new System.Windows.Forms.ToolStripMenuItem();
      this.miView = new System.Windows.Forms.ToolStripMenuItem();
      this.miSortAscending = new System.Windows.Forms.ToolStripMenuItem();
      this.miSortDescending = new System.Windows.Forms.ToolStripMenuItem();
      this.miSortNewAscending = new System.Windows.Forms.ToolStripMenuItem();
      this.miSortNewDesc = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.miGroupNamespace = new System.Windows.Forms.ToolStripMenuItem();
      this.miShowModule = new System.Windows.Forms.ToolStripMenuItem();
      this.miGroupModules = new System.Windows.Forms.ToolStripMenuItem();
      this.miUseColumns = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.miStatistics = new System.Windows.Forms.ToolStripMenuItem();
      this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mmSearch = new System.Windows.Forms.ToolStripMenuItem();
      this.miCrashlogs = new System.Windows.Forms.ToolStripMenuItem();
      this.miStacktrace = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.miSearch = new System.Windows.Forms.ToolStripMenuItem();
      this.miSearchOriginal = new System.Windows.Forms.ToolStripMenuItem();
      this.mmTools = new System.Windows.Forms.ToolStripMenuItem();
      this.miConvert = new System.Windows.Forms.ToolStripMenuItem();
      this.mmHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.miUpdateVersion = new System.Windows.Forms.ToolStripMenuItem();
      this.miVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.miReport = new System.Windows.Forms.ToolStripMenuItem();
      this.miRequestFeature = new System.Windows.Forms.ToolStripMenuItem();
      this.miGitHub = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.miDonate = new System.Windows.Forms.ToolStripMenuItem();
      this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miOpenVS = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.miCopyOldName = new System.Windows.Forms.ToolStripMenuItem();
      this.miCopyFullOldName = new System.Windows.Forms.ToolStripMenuItem();
      this.miCopyNewName = new System.Windows.Forms.ToolStripMenuItem();
      this.miCopyFullNewName = new System.Windows.Forms.ToolStripMenuItem();
      this.odPDB = new System.Windows.Forms.OpenFileDialog();
      this.odSourceFile = new System.Windows.Forms.OpenFileDialog();
      this.ptvElements = new BrokenEvent.Shared.Controls.PineappleTreeView();
      this.statusStrip.SuspendLayout();
      this.tsTools.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.contextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // odMapping
      // 
      this.odMapping.Filter = "XML files (*.xml)|*.xml";
      // 
      // statusStrip
      // 
      this.statusStrip.AllowMerge = false;
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spbLoading,
            this.slblSelected,
            this.slblModule,
            this.slblType});
      this.statusStrip.Location = new System.Drawing.Point(0, 528);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.ShowItemToolTips = true;
      this.statusStrip.Size = new System.Drawing.Size(727, 22);
      this.statusStrip.TabIndex = 1;
      this.statusStrip.Text = "statusStrip";
      // 
      // spbLoading
      // 
      this.spbLoading.Enabled = false;
      this.spbLoading.Name = "spbLoading";
      this.spbLoading.Size = new System.Drawing.Size(100, 16);
      this.spbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.spbLoading.Visible = false;
      // 
      // slblSelected
      // 
      this.slblSelected.AutoSize = false;
      this.slblSelected.Name = "slblSelected";
      this.slblSelected.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
      this.slblSelected.Size = new System.Drawing.Size(412, 17);
      this.slblSelected.Spring = true;
      this.slblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // slblModule
      // 
      this.slblModule.AutoSize = false;
      this.slblModule.Name = "slblModule";
      this.slblModule.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
      this.slblModule.Size = new System.Drawing.Size(200, 17);
      // 
      // slblType
      // 
      this.slblType.AutoSize = false;
      this.slblType.Name = "slblType";
      this.slblType.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
      this.slblType.Size = new System.Drawing.Size(100, 17);
      // 
      // ilIcons
      // 
      this.ilIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.ilIcons.ImageSize = new System.Drawing.Size(16, 16);
      this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // tsTools
      // 
      this.tsTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnCrashLogs,
            this.toolStripSeparator1,
            this.lblSearch,
            this.tbSearch});
      this.tsTools.Location = new System.Drawing.Point(0, 24);
      this.tsTools.Name = "tsTools";
      this.tsTools.Padding = new System.Windows.Forms.Padding(0);
      this.tsTools.Size = new System.Drawing.Size(727, 25);
      this.tsTools.Stretch = true;
      this.tsTools.TabIndex = 4;
      this.tsTools.Text = "toolStrip1";
      // 
      // btnOpen
      // 
      this.btnOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.miOpenBtn});
      this.btnOpen.Image = global::ObfuscarMappingParser.Properties.Resources.Open;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(65, 22);
      this.btnOpen.Text = "Open";
      this.btnOpen.DropDownOpening += new System.EventHandler(this.btnOpen_DropDownOpening);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
      // 
      // miOpenBtn
      // 
      this.miOpenBtn.Image = global::ObfuscarMappingParser.Properties.Resources.Open;
      this.miOpenBtn.Name = "miOpenBtn";
      this.miOpenBtn.Size = new System.Drawing.Size(131, 22);
      this.miOpenBtn.Text = "Open file...";
      // 
      // btnCrashLogs
      // 
      this.btnCrashLogs.Enabled = false;
      this.btnCrashLogs.Image = global::ObfuscarMappingParser.Properties.Resources.Stacktrace;
      this.btnCrashLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCrashLogs.Name = "btnCrashLogs";
      this.btnCrashLogs.Size = new System.Drawing.Size(81, 22);
      this.btnCrashLogs.Text = "Stacktrace";
      this.btnCrashLogs.ToolTipText = "Deobfuscate stacktrace";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // lblSearch
      // 
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Size = new System.Drawing.Size(79, 22);
      this.lblSearch.Text = "Quick Search:";
      this.lblSearch.ToolTipText = "Type full name of entity and press <Enter> to search";
      // 
      // tbSearch
      // 
      this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbSearch.Name = "tbSearch";
      this.tbSearch.Size = new System.Drawing.Size(200, 25);
      this.tbSearch.ToolTipText = "Type full name of entity end press <Enter> to search";
      this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmFile,
            this.miView,
            this.mmSearch,
            this.mmTools,
            this.mmHelp});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(727, 24);
      this.menuStrip.TabIndex = 3;
      this.menuStrip.Text = "menuStrip1";
      // 
      // mmFile
      // 
      this.mmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miRecents,
            this.toolStripSeparator9,
            this.miReload,
            this.toolStripSeparator6,
            this.miManagePDBs,
            this.miAttachPDB,
            this.toolStripSeparator2,
            this.miExit});
      this.mmFile.Name = "mmFile";
      this.mmFile.Size = new System.Drawing.Size(37, 20);
      this.mmFile.Text = "File";
      this.mmFile.DropDownOpening += new System.EventHandler(this.mmFile_DropDownOpening);
      // 
      // miOpen
      // 
      this.miOpen.Image = global::ObfuscarMappingParser.Properties.Resources.Open;
      this.miOpen.Name = "miOpen";
      this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.miOpen.Size = new System.Drawing.Size(169, 22);
      this.miOpen.Text = "Open...";
      // 
      // miRecents
      // 
      this.miRecents.Name = "miRecents";
      this.miRecents.Size = new System.Drawing.Size(169, 22);
      this.miRecents.Text = "Recent";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(166, 6);
      // 
      // miReload
      // 
      this.miReload.Image = global::ObfuscarMappingParser.Properties.Resources.Reload;
      this.miReload.Name = "miReload";
      this.miReload.Size = new System.Drawing.Size(169, 22);
      this.miReload.Text = "Reload";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(166, 6);
      // 
      // miManagePDBs
      // 
      this.miManagePDBs.Enabled = false;
      this.miManagePDBs.Name = "miManagePDBs";
      this.miManagePDBs.Size = new System.Drawing.Size(169, 22);
      this.miManagePDBs.Text = "Manage .PDB files";
      // 
      // miAttachPDB
      // 
      this.miAttachPDB.Enabled = false;
      this.miAttachPDB.Name = "miAttachPDB";
      this.miAttachPDB.Size = new System.Drawing.Size(169, 22);
      this.miAttachPDB.Text = "Attach .PDB file...";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
      // 
      // miExit
      // 
      this.miExit.Name = "miExit";
      this.miExit.Size = new System.Drawing.Size(169, 22);
      this.miExit.Text = "Exit";
      // 
      // miView
      // 
      this.miView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSortAscending,
            this.miSortDescending,
            this.miSortNewAscending,
            this.miSortNewDesc,
            this.toolStripSeparator4,
            this.miGroupNamespace,
            this.miShowModule,
            this.miGroupModules,
            this.miUseColumns,
            this.toolStripSeparator5,
            this.miStatistics,
            this.miSettings});
      this.miView.Name = "miView";
      this.miView.Size = new System.Drawing.Size(44, 20);
      this.miView.Text = "View";
      // 
      // miSortAscending
      // 
      this.miSortAscending.Image = global::ObfuscarMappingParser.Properties.Resources.SortAscending;
      this.miSortAscending.Name = "miSortAscending";
      this.miSortAscending.Size = new System.Drawing.Size(256, 22);
      this.miSortAscending.Text = "Sort by Original Name Ascending";
      this.miSortAscending.Click += new System.EventHandler(this.miSorting_Click);
      // 
      // miSortDescending
      // 
      this.miSortDescending.Image = global::ObfuscarMappingParser.Properties.Resources.SortDescending;
      this.miSortDescending.Name = "miSortDescending";
      this.miSortDescending.Size = new System.Drawing.Size(256, 22);
      this.miSortDescending.Text = "Sort by Original Name Descending";
      this.miSortDescending.Click += new System.EventHandler(this.miSorting_Click);
      // 
      // miSortNewAscending
      // 
      this.miSortNewAscending.Image = global::ObfuscarMappingParser.Properties.Resources.SortAscending;
      this.miSortNewAscending.Name = "miSortNewAscending";
      this.miSortNewAscending.Size = new System.Drawing.Size(256, 22);
      this.miSortNewAscending.Text = "Sort by New Name Ascending";
      this.miSortNewAscending.Click += new System.EventHandler(this.miSorting_Click);
      // 
      // miSortNewDesc
      // 
      this.miSortNewDesc.Image = global::ObfuscarMappingParser.Properties.Resources.SortDescending;
      this.miSortNewDesc.Name = "miSortNewDesc";
      this.miSortNewDesc.Size = new System.Drawing.Size(256, 22);
      this.miSortNewDesc.Text = "Sort by New Name Descending";
      this.miSortNewDesc.Click += new System.EventHandler(this.miSorting_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(253, 6);
      // 
      // miGroupNamespace
      // 
      this.miGroupNamespace.Image = global::ObfuscarMappingParser.Properties.Resources.SortUsingNamespace;
      this.miGroupNamespace.Name = "miGroupNamespace";
      this.miGroupNamespace.Size = new System.Drawing.Size(256, 22);
      this.miGroupNamespace.Text = "Group by Original Namespace";
      this.miGroupNamespace.Click += new System.EventHandler(this.miGroupNamespace_Click);
      // 
      // miShowModule
      // 
      this.miShowModule.Image = global::ObfuscarMappingParser.Properties.Resources.IconAssembly;
      this.miShowModule.Name = "miShowModule";
      this.miShowModule.Size = new System.Drawing.Size(256, 22);
      this.miShowModule.Text = "Show Module";
      this.miShowModule.Click += new System.EventHandler(this.miShowModule_Click);
      // 
      // miGroupModules
      // 
      this.miGroupModules.Image = global::ObfuscarMappingParser.Properties.Resources.IconAssembly;
      this.miGroupModules.Name = "miGroupModules";
      this.miGroupModules.Size = new System.Drawing.Size(256, 22);
      this.miGroupModules.Text = "Group by Modules";
      this.miGroupModules.Click += new System.EventHandler(this.miGroupModules_Click);
      // 
      // miUseColumns
      // 
      this.miUseColumns.Image = global::ObfuscarMappingParser.Properties.Resources.Tree;
      this.miUseColumns.Name = "miUseColumns";
      this.miUseColumns.Size = new System.Drawing.Size(256, 22);
      this.miUseColumns.Text = "Use Columns in Tree";
      this.miUseColumns.Click += new System.EventHandler(this.miUseColumns_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(253, 6);
      // 
      // miStatistics
      // 
      this.miStatistics.Enabled = false;
      this.miStatistics.Name = "miStatistics";
      this.miStatistics.Size = new System.Drawing.Size(256, 22);
      this.miStatistics.Text = "Statistics";
      // 
      // miSettings
      // 
      this.miSettings.Image = global::ObfuscarMappingParser.Properties.Resources.Settings;
      this.miSettings.Name = "miSettings";
      this.miSettings.Size = new System.Drawing.Size(256, 22);
      this.miSettings.Text = "Settings...";
      // 
      // mmSearch
      // 
      this.mmSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCrashlogs,
            this.miStacktrace,
            this.toolStripSeparator8,
            this.miSearch,
            this.miSearchOriginal});
      this.mmSearch.Name = "mmSearch";
      this.mmSearch.Size = new System.Drawing.Size(54, 20);
      this.mmSearch.Text = "Search";
      // 
      // miCrashlogs
      // 
      this.miCrashlogs.Image = global::ObfuscarMappingParser.Properties.Resources.Stacktrace;
      this.miCrashlogs.Name = "miCrashlogs";
      this.miCrashlogs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.miCrashlogs.Size = new System.Drawing.Size(246, 22);
      this.miCrashlogs.Text = "Deobfuscate Stacktrace...";
      // 
      // miStacktrace
      // 
      this.miStacktrace.Image = global::ObfuscarMappingParser.Properties.Resources.Stacktrace2;
      this.miStacktrace.Name = "miStacktrace";
      this.miStacktrace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.miStacktrace.Size = new System.Drawing.Size(246, 22);
      this.miStacktrace.Text = "Analyze Stacktrace...";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(243, 6);
      // 
      // miSearch
      // 
      this.miSearch.Image = global::ObfuscarMappingParser.Properties.Resources.Search;
      this.miSearch.Name = "miSearch";
      this.miSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
      this.miSearch.Size = new System.Drawing.Size(246, 22);
      this.miSearch.Text = "Search...";
      // 
      // miSearchOriginal
      // 
      this.miSearchOriginal.Image = global::ObfuscarMappingParser.Properties.Resources.Search;
      this.miSearchOriginal.Name = "miSearchOriginal";
      this.miSearchOriginal.Size = new System.Drawing.Size(246, 22);
      this.miSearchOriginal.Text = "Search for Original";
      // 
      // mmTools
      // 
      this.mmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miConvert});
      this.mmTools.Name = "mmTools";
      this.mmTools.Size = new System.Drawing.Size(48, 20);
      this.mmTools.Text = "Tools";
      // 
      // miConvert
      // 
      this.miConvert.Name = "miConvert";
      this.miConvert.Size = new System.Drawing.Size(177, 22);
      this.miConvert.Text = "Convert Raw Data...";
      // 
      // mmHelp
      // 
      this.mmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdateVersion,
            this.miVersionHistory,
            this.toolStripSeparator11,
            this.miReport,
            this.miRequestFeature,
            this.miGitHub,
            this.toolStripSeparator10,
            this.miDonate,
            this.miAbout});
      this.mmHelp.Name = "mmHelp";
      this.mmHelp.Size = new System.Drawing.Size(44, 20);
      this.mmHelp.Text = "Help";
      // 
      // miUpdateVersion
      // 
      this.miUpdateVersion.Name = "miUpdateVersion";
      this.miUpdateVersion.Size = new System.Drawing.Size(210, 22);
      this.miUpdateVersion.Text = "Check for Updates";
      this.miUpdateVersion.Click += new System.EventHandler(this.miUpdateVersion_Click);
      // 
      // miVersionHistory
      // 
      this.miVersionHistory.Name = "miVersionHistory";
      this.miVersionHistory.Size = new System.Drawing.Size(210, 22);
      this.miVersionHistory.Text = "Version History";
      this.miVersionHistory.Click += new System.EventHandler(this.miVersionHistory_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(207, 6);
      // 
      // miReport
      // 
      this.miReport.Name = "miReport";
      this.miReport.Size = new System.Drawing.Size(210, 22);
      this.miReport.Text = "Report a Problem";
      this.miReport.Click += new System.EventHandler(this.miReport_Click);
      // 
      // miRequestFeature
      // 
      this.miRequestFeature.Name = "miRequestFeature";
      this.miRequestFeature.Size = new System.Drawing.Size(210, 22);
      this.miRequestFeature.Text = "Request a Feature";
      this.miRequestFeature.Click += new System.EventHandler(this.miRequestFeature_Click);
      // 
      // miGitHub
      // 
      this.miGitHub.Name = "miGitHub";
      this.miGitHub.Size = new System.Drawing.Size(210, 22);
      this.miGitHub.Text = "GitHub";
      this.miGitHub.Click += new System.EventHandler(this.miGitHub_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(207, 6);
      // 
      // miDonate
      // 
      this.miDonate.Name = "miDonate";
      this.miDonate.Size = new System.Drawing.Size(210, 22);
      this.miDonate.Text = "Support the Development";
      this.miDonate.Click += new System.EventHandler(this.miDonate_Click);
      // 
      // miAbout
      // 
      this.miAbout.Name = "miAbout";
      this.miAbout.Size = new System.Drawing.Size(210, 22);
      this.miAbout.Text = "About";
      // 
      // contextMenuStrip
      // 
      this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenVS,
            this.toolStripSeparator7,
            this.miCopyOldName,
            this.miCopyFullOldName,
            this.miCopyNewName,
            this.miCopyFullNewName});
      this.contextMenuStrip.Name = "contextMenuStrip";
      this.contextMenuStrip.Size = new System.Drawing.Size(187, 120);
      this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
      // 
      // miOpenVS
      // 
      this.miOpenVS.Image = global::ObfuscarMappingParser.Properties.Resources.Editor;
      this.miOpenVS.Name = "miOpenVS";
      this.miOpenVS.Size = new System.Drawing.Size(186, 22);
      this.miOpenVS.Text = "Open in Editor";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(183, 6);
      // 
      // miCopyOldName
      // 
      this.miCopyOldName.Name = "miCopyOldName";
      this.miCopyOldName.Size = new System.Drawing.Size(186, 22);
      this.miCopyOldName.Text = "Copy Old Name";
      // 
      // miCopyFullOldName
      // 
      this.miCopyFullOldName.Name = "miCopyFullOldName";
      this.miCopyFullOldName.Size = new System.Drawing.Size(186, 22);
      this.miCopyFullOldName.Text = "Copy Full Old Name";
      // 
      // miCopyNewName
      // 
      this.miCopyNewName.Name = "miCopyNewName";
      this.miCopyNewName.Size = new System.Drawing.Size(186, 22);
      this.miCopyNewName.Text = "Copy New Name";
      // 
      // miCopyFullNewName
      // 
      this.miCopyFullNewName.Name = "miCopyFullNewName";
      this.miCopyFullNewName.Size = new System.Drawing.Size(186, 22);
      this.miCopyFullNewName.Text = "Copy Full New Name";
      // 
      // odPDB
      // 
      this.odPDB.DefaultExt = "pdb";
      this.odPDB.Filter = "Program database (*.pdb)|*.pdb";
      this.odPDB.Multiselect = true;
      // 
      // odSourceFile
      // 
      this.odSourceFile.FileName = "openFileDialog1";
      // 
      // ptvElements
      // 
      this.ptvElements.AllowIgnoreColumns = true;
      this.ptvElements.AutoScroll = true;
      this.ptvElements.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.ptvElements.CollapseImage = global::ObfuscarMappingParser.Properties.Resources.Collapse;
      this.ptvElements.ColumnHeaderStyle = BrokenEvent.Shared.Controls.ColumnHeaderStyle.None;
      pineappleTreeColumn1.Autosize = true;
      pineappleTreeColumn1.HeaderText = "Classname";
      pineappleTreeColumn1.Multiline = true;
      pineappleTreeColumn1.TextPadding = new System.Windows.Forms.Padding(2);
      pineappleTreeColumn1.Width = 477;
      pineappleTreeColumn2.HeaderText = "Renamed";
      pineappleTreeColumn2.TextPadding = new System.Windows.Forms.Padding(2);
      pineappleTreeColumn2.Width = 250;
      this.ptvElements.Columns.Add(pineappleTreeColumn1);
      this.ptvElements.Columns.Add(pineappleTreeColumn2);
      this.ptvElements.ContextMenuStrip = this.contextMenuStrip;
      this.ptvElements.DisabledColor = System.Drawing.SystemColors.Control;
      this.ptvElements.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ptvElements.DrawBorder = false;
      this.ptvElements.DropHoverIndicatorColor = System.Drawing.Color.CornflowerBlue;
      this.ptvElements.EmptyListText = "";
      this.ptvElements.ExpandImage = global::ObfuscarMappingParser.Properties.Resources.Expand;
      this.ptvElements.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.ptvElements.FullRowSelect = true;
      this.ptvElements.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.ptvElements.IconPadding = new System.Windows.Forms.Padding(2, 2, 0, 0);
      this.ptvElements.ImageList = this.ilIcons;
      this.ptvElements.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
      this.ptvElements.Location = new System.Drawing.Point(0, 49);
      this.ptvElements.Name = "ptvElements";
      this.ptvElements.ShowToolTip = true;
      this.ptvElements.Size = new System.Drawing.Size(727, 479);
      this.ptvElements.TabIndex = 5;
      this.ptvElements.Text = "pineappleTreeView1";
      this.ptvElements.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 2);
      this.ptvElements.NodeSelect += new System.EventHandler<BrokenEvent.Shared.Controls.NodeSelectEventArgs>(this.ptvElements_NodeSelect);
      this.ptvElements.DoubleClick += new System.EventHandler(this.ptvElements_DoubleClick);
      // 
      // MainForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(727, 550);
      this.Controls.Add(this.ptvElements);
      this.Controls.Add(this.tsTools);
      this.Controls.Add(this.menuStrip);
      this.Controls.Add(this.statusStrip);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
      this.Text = "Obfuscar Mapping Parser";
      this.Activated += new System.EventHandler(this.MainForm_Activated);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.tsTools.ResumeLayout(false);
      this.tsTools.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.contextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog odMapping;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel slblSelected;
    private System.Windows.Forms.ImageList ilIcons;
    private System.Windows.Forms.ToolStrip tsTools;
    private System.Windows.Forms.ToolStripButton btnCrashLogs;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel lblSearch;
    private System.Windows.Forms.ToolStripTextBox tbSearch;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem mmFile;
    private System.Windows.Forms.ToolStripMenuItem miOpen;
    private System.Windows.Forms.ToolStripMenuItem miRecents;
    private System.Windows.Forms.ToolStripMenuItem miExit;
    private System.Windows.Forms.ToolStripMenuItem mmHelp;
    private System.Windows.Forms.ToolStripMenuItem miAbout;
    private BrokenEvent.Shared.Controls.PineappleTreeView ptvElements;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripDropDownButton btnOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem miOpenBtn;
    private System.Windows.Forms.ToolStripMenuItem mmSearch;
    private System.Windows.Forms.ToolStripMenuItem miCrashlogs;
    private System.Windows.Forms.ToolStripMenuItem miView;
    private System.Windows.Forms.ToolStripMenuItem miSortAscending;
    private System.Windows.Forms.ToolStripMenuItem miSortDescending;
    private System.Windows.Forms.ToolStripMenuItem miSortNewAscending;
    private System.Windows.Forms.ToolStripMenuItem miSortNewDesc;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem miGroupNamespace;
    private System.Windows.Forms.ToolStripMenuItem miShowModule;
    private System.Windows.Forms.ToolStripProgressBar spbLoading;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem miStatistics;
    private System.Windows.Forms.ToolStripMenuItem miGroupModules;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem miCopyOldName;
    private System.Windows.Forms.ToolStripMenuItem miCopyFullOldName;
    private System.Windows.Forms.ToolStripMenuItem miOpenVS;
    private System.Windows.Forms.ToolStripMenuItem miCopyNewName;
    private System.Windows.Forms.ToolStripMenuItem miCopyFullNewName;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripMenuItem miAttachPDB;
    private System.Windows.Forms.OpenFileDialog odPDB;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem miSettings;
    private System.Windows.Forms.ToolStripMenuItem miSearch;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripMenuItem miStacktrace;
    private System.Windows.Forms.ToolStripMenuItem miUseColumns;
    private System.Windows.Forms.OpenFileDialog odSourceFile;
    private System.Windows.Forms.ToolStripMenuItem miManagePDBs;
    private System.Windows.Forms.ToolStripMenuItem miReload;
    private System.Windows.Forms.ToolStripMenuItem miSearchOriginal;
    private System.Windows.Forms.ToolStripMenuItem mmTools;
    private System.Windows.Forms.ToolStripMenuItem miConvert;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripMenuItem miUpdateVersion;
    private System.Windows.Forms.ToolStripMenuItem miReport;
    private System.Windows.Forms.ToolStripMenuItem miVersionHistory;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripStatusLabel slblType;
    private System.Windows.Forms.ToolStripStatusLabel slblModule;
    private System.Windows.Forms.ToolStripMenuItem miRequestFeature;
    private System.Windows.Forms.ToolStripMenuItem miDonate;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripMenuItem miGitHub;
  }
}

