using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrokenEvent.NanoXml;
using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.Algorithms;
using BrokenEvent.Shared.Controls;
using BrokenEvent.Shared.Rest;
using BrokenEvent.Shared.WinApi;
using BrokenEvent.TaskDialogs;
using BrokenEvent.TaskDialogs.Dialogs;
using BrokenEvent.VisualStudioOpener;

using ObfuscarMappingParser.Engine;
using ObfuscarMappingParser.Engine.Items;
using ObfuscarMappingParser.Engine.Reader;
using ObfuscarMappingParser.Properties;

namespace ObfuscarMappingParser
{
  partial class MainForm : Form
  {
    private readonly string fileToLoad;
    private string mappingFilename;
    private MappingViewModel mapping;
    private ClipboardWatcher clipboardWatcher;
    private const string APP_TITLE = "Obfuscar Mapping Parser";

    public readonly int ICON_NAMESPACE;
    public readonly int ICON_NO_NAMESPACE;
    public readonly int ICON_CLASS;
    public readonly int ICON_EVENT;
    public readonly int ICON_FIELD;
    public readonly int ICON_METHOD;
    public readonly int ICON_CTOR;
    public readonly int ICON_PROPERTY;
    public readonly int ICON_ASSEMBLY;
    public readonly int ICON_MULTIPLE;
    public readonly int ICON_PDB;
    public readonly int ICON_RESOURCES;
    public readonly int ICON_RESOURCE;

    private void AddIcon(Bitmap bmp, out int index)
    {
      ilIcons.Images.Add(bmp);
      index = ilIcons.Images.Count - 1;
    }

    public MainForm(string filename)
    {
      fileToLoad = filename;

      InitializeComponent();

      // load manually from resources, as VS RESX always broke icon colors
      AddIcon(Resources.IconNamespace, out ICON_NAMESPACE);
      AddIcon(Resources.IconNoNamespace, out ICON_NO_NAMESPACE);
      AddIcon(Resources.IconClass, out ICON_CLASS);
      AddIcon(Resources.IconEvent, out ICON_EVENT);
      AddIcon(Resources.IconField, out ICON_FIELD);
      AddIcon(Resources.IconMethod, out ICON_METHOD);
      AddIcon(Resources.IconConstructor, out ICON_CTOR);
      AddIcon(Resources.IconProperty, out ICON_PROPERTY);
      AddIcon(Resources.IconAssembly, out ICON_ASSEMBLY);
      AddIcon(Resources.IconMultiple, out ICON_MULTIPLE);
      AddIcon(Resources.IconPdb, out ICON_PDB);
      AddIcon(Resources.IconResources, out ICON_RESOURCES);
      AddIcon(Resources.IconResource, out ICON_RESOURCE);

      ptvElements.Highlights.Add(new ListHighlight(Color.DarkRed));
      ptvElements.Highlights.Add(new ListHighlight(Color.DarkBlue));

      odMapping.Filter = FormatFactory.BuildFilterList();

      try
      {
        Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
      }
      catch { }

      miShowModule.Checked = Configs.Instance.ShowModules;
      miGroupNamespace.Checked = Configs.Instance.GroupNamespaces;
      miGroupModules.Checked = Configs.Instance.GroupModules;
      miUseColumns.Checked = Configs.Instance.UseColumns;

      miSortAscending.Tag = Configs.SortingTypes.OriginalNameAscending;
      miSortDescending.Tag = Configs.SortingTypes.OriginalNameDescending;
      miSortNewAscending.Tag = Configs.SortingTypes.NewNameAscending;
      miSortNewDesc.Tag = Configs.SortingTypes.NewNameDescending;
      SortingType = Configs.Instance.SortingType;

      InitCommandManager();

      EnableMappingActions(false);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      clipboardWatcher = new ClipboardWatcher(Handle);
      clipboardWatcher.RegisterClipboardViewer();
      clipboardWatcher.ClipboardChanged += ClipboardWatcher_ClipboardChanged;
    }

    private void ClipboardWatcher_ClipboardChanged(object sender, EventArgs e)
    {
      if (!Clipboard.ContainsText() || !Configs.Instance.WatchClipboard || !CanFocus)
        return;

      string stacktrace = Clipboard.GetText();
      CrashLogForm form = GetOpenedForm<CrashLogForm>();
      bool skipPrefixes = false;
      if (form != null)
      {
        skipPrefixes = form.SkipPrefixes;
        if (stacktrace == form.Value)
          return;
      }

      try
      {
        stacktrace = mapping.Mapping.ProcessCrashlogText(stacktrace, skipPrefixes);
        if (form != null && stacktrace == form.Value)
          return;
      }
      catch
      {
        return;
      }

      if (form == null)
        AddFormToOpened(form = new CrashLogForm(mapping)).Show(this);
      form.Value = stacktrace;
    }

    private void BeginLoading(string operation)
    {
      this.SetTaskbarProgressState(Taskbar.ThumbnailProgressState.Indeterminate);
      spbLoading.Visible = true;
      slblType.Visible = false;
      slblType.Text = "";
      slblModule.Visible = false;
      slblModule.Text = "";
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = false;
      commandManager.BeginDisable();
      slblSelected.Text = operation;
    }

    private void EndLoading(string result)
    {
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = true;
      spbLoading.Visible = false;
      slblType.Visible = slblModule.Visible = true;
      commandManager.EndDisable();
      slblSelected.Text = result;
      this.SetTaskbarProgressState(Taskbar.ThumbnailProgressState.NoProgress);
    }

    private void HandleMappingLoadingException(Exception e, string filename)
    {
      this.SetTaskbarProgressValue(100, 100);
      this.SetTaskbarProgressState(Taskbar.ThumbnailProgressState.Error);

      if (e is NanoXmlParsingException ||
          e is ObfuscarParserException ||
          e is IOException)
        TaskDialogHelper.ShowMessageBox(
            Handle,
            "Mapping Loading Failed",
            "Loading of mapping file is failed.",
            "File:\n" + filename + "\nReason: " + e.Message,
            TaskDialogStandardIcon.Error
          );
      else
        RestApi.Instance.SendCrashReport(
            new CrashReport("Mapping loading thread failed handler", "Error on loading document", CrashType.JustReport) { Exception = e },
            this
          );
    }

    private async Task OpenFile(string filename)
    {
      mappingFilename = filename;
      Text = $"{APP_TITLE} - {PathUtils.GetFilename(filename)}";
      Configs.Instance.AddRecent(filename);

      BeginLoading($"Loading: {filename}");

      while (openedForms.Count > 0)
        openedForms[0].Close();

      try
      {
        mapping = await Task.Run(() => new MappingViewModel(filename));
      }
      catch (Exception e)
      {
        EnableMappingActions(false);
        commandManager.SetEnabled(Actions.ReloadFile, true);
        HandleMappingLoadingException(e, filename);
        EndLoading("Loading failed.");
        return;
      }

      BuildMapping();
      EnableMappingActions(true);
      EndLoading($"Mapping loaded in {mapping.Mapping.LoadTime} ms");

      AttachPDB(Configs.Instance.GetRecentPdb(mapping.Mapping.Filename), false);

      tbSearch.AutoCompleteCustomSource = mapping.GetNewNamesCollection();
    }

    private async void ReloadFile()
    {
      BeginLoading($"Reloading: {mappingFilename}");
      while (openedForms.Count > 0)
        openedForms[0].Close();

      try
      {
        if (mapping != null)
          await Task.Run(() => mapping.Mapping.Reload());
        else
          mapping = await Task.Run(() => new MappingViewModel(mappingFilename));
      }
      catch (Exception e)
      {
        HandleMappingLoadingException(e, mappingFilename);
        EndLoading("Reloading failed.");
        return;
      }

      BuildMapping();
      EnableMappingActions(true);
      EndLoading($"Mapping reloaded in {mapping.Mapping.LoadTime} ms");
      tbSearch.AutoCompleteCustomSource = mapping.GetNewNamesCollection();
    }

    private void BuildMapping()
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      ptvElements.BeginUpdate();
      mapping.PurgeTreeNodes();

      TreeBuilder builder = new TreeBuilder(ptvElements, mapping, this);
      builder.GroupNamespaces = Configs.Instance.GroupNamespaces;
      builder.GroupModules = Configs.Instance.GroupModules;
      builder.ShowModules = Configs.Instance.ShowModules;
      builder.ShowResources = Configs.Instance.ShowResources;
      builder.ShowSkippedMembers = Configs.Instance.ShowSkipped;
      try
      {
        builder.Build();
      }
      catch (Exception e)
      {
        RestApi.Instance.SendCrashReport(
            new CrashReport("Mapping tree builder", "Error on building classes tree", CrashType.JustReport){ Exception = e},
            this
          );
      }

      Sort(Configs.Instance.SortingType);
      focusedItem = null;

      ptvElements.CollapseAll();
      ptvElements.EndUpdate();
      sw.Stop();
      Debug.WriteLine($"Tree building: {sw.ElapsedMilliseconds} ms");
    }

    private void tbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter || mapping == null) // how??
        return;

      SearchResults result = mapping.Mapping.SearchForNewName(tbSearch.Text, false, false);

      if (result == null || !result.HasValue)
        return;

      ptvElements.Focus();

      if (result.IsSingleResult)
      {
        ptvElements.SelectedNode = mapping.FindNode((RenamedBase)result.SingleResult);
        ptvElements.SelectedNode.EnsureVisible();
        return;
      }

      new SearchResultsForm(this, result, "Search Results: " + tbSearch.Text).Show(this);
      tbSearch.Clear();
    }

    #region Sorting

    private void miSorting_Click(object sender, EventArgs e)
    {
      SortingType = (Configs.SortingTypes)((ToolStripMenuItem)sender).Tag;
    }

    private void Sort(Configs.SortingTypes sortingType)
    {
      switch (sortingType)
      {
        case Configs.SortingTypes.OriginalNameAscending:
          ptvElements.Sort(true);
          break;
        case Configs.SortingTypes.OriginalNameDescending:
          ptvElements.Sort(false);
          break;
        case Configs.SortingTypes.NewNameAscending:
          ptvElements.Sort(ElementsComparisonAsc);
          break;
        case Configs.SortingTypes.NewNameDescending:
          ptvElements.Sort(ElementsComparisonDesc);
          break;
      }
    }

    public Configs.SortingTypes SortingType
    {
      get { return Configs.Instance.SortingType; }
      set
      {
        miSortAscending.Checked = value == Configs.SortingTypes.OriginalNameAscending;
        miSortDescending.Checked = value == Configs.SortingTypes.OriginalNameDescending;
        miSortNewAscending.Checked = value == Configs.SortingTypes.NewNameAscending;
        miSortNewDesc.Checked = value == Configs.SortingTypes.NewNameDescending;
        Configs.Instance.SortingType = value;
        Sort(Configs.Instance.SortingType);
      }
    }

    private static int ElementsComparisonAsc(PineappleTreeNode node1, PineappleTreeNode node2)
    {
      if (node1.Subitems.Count == 0 || node2.Subitems.Count == 0)
        return 0;
      return string.Compare(node1.Subitems[0].Text, node2.Subitems[0].Text, StringComparison.InvariantCulture);
    }

    private static int ElementsComparisonDesc(PineappleTreeNode node1, PineappleTreeNode node2)
    {
      if (node1.Subitems.Count == 0 || node2.Subitems.Count == 0)
        return 0;
      return string.Compare(node2.Subitems[0].Text, node1.Subitems[0].Text, StringComparison.InvariantCulture);
    }

    #endregion

    #region View Menu

    private void miGroupNamespace_Click(object sender, EventArgs e)
    {
      Configs.Instance.GroupNamespaces = miGroupNamespace.Checked = !miGroupNamespace.Checked;
      if (mapping != null)
        BuildMapping();
    }

    private void miShowModule_Click(object sender, EventArgs e)
    {
      Configs.Instance.ShowModules = miShowModule.Checked = !miShowModule.Checked;
      if (mapping != null)
        BuildMapping();
    }

    private void miGroupModules_Click(object sender, EventArgs e)
    {
      Configs.Instance.GroupModules = miGroupModules.Checked = !miGroupModules.Checked;
      if (mapping != null)
        BuildMapping();
    }

    private void miUseColumns_Click(object sender, EventArgs e)
    {
      Configs.Instance.UseColumns = miUseColumns.Checked = !miUseColumns.Checked;
      if (mapping != null)
        BuildMapping();
    }

    #endregion

    #region Properties

    public ImageList IconsList
    {
      get { return ilIcons; }
    }

    public MappingViewModel Mapping
    {
      get { return mapping; }
    }

    public bool HavePdb
    {
      get { return mapping.PdbFiles.Count > 0; }
    }

    #endregion

    #region Context menu

    private RenamedBase focusedItem;
    private string focusedFilename;
    private int focusedLine;

    private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
      e.Cancel = focusedItem == null;
    }

    #endregion

    #region Tree Handlers

    private void ptvElements_NodeSelect(object sender, NodeSelectEventArgs e)
    {
      focusedItem = null;
      if (e.Node != null && e.Node.Selected)
        focusedItem = e.Node.Tag as RenamedBase;

      EnableSelectionActions(focusedItem != null);
      slblSelected.Text = focusedItem == null ? "" : focusedItem.TransformSimple;
      slblType.Text = focusedItem == null ? "" : focusedItem.EntityType.ToString();
      slblModule.Text = focusedItem == null ? "" : focusedItem.ModuleOld;
      commandManager.SetEnabled(Actions.OpenInEditor, focusedItem != null && mapping.DetectMarkersForVS(out focusedFilename, out focusedLine, focusedItem));
    }

    private void ptvElements_DoubleClick(object sender, EventArgs e)
    {
      if (focusedItem == null)
        return;

      commandManager.CallCommand((Actions)(int)Configs.Instance.DoubleClickAction);
    }

    #endregion

    #region Opened Forms

    private List<Form> openedForms = new List<Form>();

    private FormType AddFormToOpened<FormType>(FormType form) where FormType : Form
    {
      openedForms.Add(form);
      form.Closed += OpenedForm_Closed;
      return form;
    }

    private void OpenedForm_Closed(object sender, EventArgs eventArgs)
    {
      openedForms.Remove((Form)sender);
    }

    private void mmFile_DropDownOpening(object sender, EventArgs e)
    {
      miRecents.DropDownItems.Clear();

      foreach (string recent in Configs.Instance.Recents)
      {
        ToolStripMenuItem item = new ToolStripMenuItem(PathUtils.ShortenPath(recent, 70));
        item.Click += RecentItem_Click;
        item.Tag = recent;
        miRecents.DropDownItems.Add(item);
        item.Image = Resources.Document;
      }

      miRecents.Enabled = miRecents.DropDownItems.Count > 0;
    }

    private async void RecentItem_Click(object sender, EventArgs eventArgs)
    {
      string filename = (string)((ToolStripMenuItem)sender).Tag;
      if (!File.Exists(filename))
      {
        MessageBox.Show(this, "File\n" + filename + "\ndoesn't exist.", "File not exist", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      await OpenFile(filename);
    }

    private void btnOpen_DropDownOpening(object sender, EventArgs e)
    {
      while (btnOpen.DropDownItems.Count > 2)
        btnOpen.DropDownItems.RemoveAt(0);

      int index = 0;
      foreach (string recent in Configs.Instance.Recents)
      {
        ToolStripMenuItem item = new ToolStripMenuItem(PathUtils.ShortenPath(recent, 70));
        item.Click += RecentItem_Click;
        item.Tag = recent;
        btnOpen.DropDownItems.Insert(index++, item);
        item.Image = Resources.Document;
      }

      toolStripSeparator3.Visible = btnOpen.DropDownItems.Count > 2;
    }

    private FormType GetOpenedForm<FormType>() where FormType: class
    {
      foreach (Form form in openedForms)
      {
        FormType result = form as FormType;
        if (result != null)
          return result;
      }

      return null;
    }

    #endregion

    #region Recent Menus

    #endregion

    #region PDB

    public bool CallAttachPdb(IWin32Window owner)
    {
      if (mapping == null)
        return false;

      if (odPDB.ShowDialog(owner) != DialogResult.OK)
        return false;

      foreach (string fileName in odPDB.FileNames)
        AttachPDB(fileName, owner, true);

      return true;
    }

    private void AttachPDB(IList<string> files, bool addToRecent)
    {
      if (files == null || mapping == null)
        return;

      // single file - different dialog
      if (files.Count == 1)
      {
        if (TaskDialogHelper.ShowTaskDialog(
                Handle,
                "Attach PDB File",
                "Attach PDB file?",
                files[0],
                TaskDialogStandardIcon.Information,
                new string[] { "Attach", "Don't attach" },
                null,
                new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No }
              ) ==
            TaskDialogResult.Yes)
          AttachPDB(files[0], this, addToRecent);

        return;
      }

      bool isYesToAll = false;

      for (int i = 0; i < files.Count; i++)
      {
        string file = files[i];
        if (!File.Exists(file))
          continue;

        if (mapping.IsPdbAttached(file))
          continue;

        TaskDialogResult result;

        if (isYesToAll)
          result = TaskDialogResult.Yes;
        else if (i == files.Count - 1)
          result = TaskDialogHelper.ShowTaskDialog(
              Handle,
              "Attach PDB File",
              "Attach PDB file?",
              files[0],
              TaskDialogStandardIcon.Information,
              new string[] { "Attach", "Don't attach" },
              null,
              new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No }
            );
        else
          result = TaskDialogHelper.ShowTaskDialog(
              Handle,
              "Attach PDB File",
              "Attach PDB file?",
              files[0],
              TaskDialogStandardIcon.Information,
              new string[] { "Attach", "Attach All", "Don't attach", "Cancel" },
              null,
              new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.Ok, TaskDialogResult.No, TaskDialogResult.Cancel,  }
            );

        switch (result)
        {
          case TaskDialogResult.Yes: // "yes"
            AttachPDB(file, this, addToRecent);
            break;

          case TaskDialogResult.Ok: // "yes to all"
            isYesToAll = true;
            goto case TaskDialogResult.Yes;

          case TaskDialogResult.Cancel: // "cancel"
            return;
        }
      }
    }

    public bool AttachPDB(string filename, IWin32Window owner, bool addToRecents)
    {
      if (mapping == null)
        return false;

      if (mapping.IsPdbAttached(filename))
      {
        slblSelected.Text = "PDB file already attached: " + PathUtils.GetFilename(filename);
        return false;
      }

      try
      {
        mapping.PdbFiles.Add(new PdbFile(filename));
      }
      catch (Exception ex)
      {
        MessageBox.Show(owner, "Failed to load PDB file: " + ex.Message, "Failed to load PDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      if (addToRecents)
        Configs.Instance.AddRecentPdb(mapping.Mapping.Filename, filename);
      slblSelected.Text = "PDB file attached: " + PathUtils.GetFilename(filename);
      return true;
    }

    #endregion

    public async void OpenInVisualStudio(string filename, int line)
    {
      if (!File.Exists(filename))
      {
        if (TaskDialogHelper.ShowTaskDialog(
                Handle,
                "File Not Exists",
                "File not found. Would you like to locate file by sourself?",
                "Filename: " + filename,
                TaskDialogStandardIcon.Error,
                new string[] { "Browse", "Cancel" },
                null,
                new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No, }
              ) != TaskDialogResult.Yes)
          return;

        string file = PathUtils.GetFilename(filename);
        odSourceFile.Filter = file + "|" + file;
        odSourceFile.FileName = file;
        if (odSourceFile.ShowDialog(this) != DialogResult.OK)
          return;

        filename = odSourceFile.FileName;
      }
      string vs = Configs.Instance.GetRecentProperty(mapping.Mapping.Filename, Configs.PROPERTY_EDITOR);
      if (vs == null)
        vs = Configs.Instance.Editor;
      IVisualStudioInfo visualStudio = VisualStudioDetector.GetVisualStudioInfo(vs);

      BeginLoading("Starting the Visual Studio...");
      try
      {
        await Task.Run(()=>visualStudio.OpenFile(filename, line));
      }
      catch (Exception ex)
      {
        this.SetTaskbarProgressValue(100, 100);
        this.SetTaskbarProgressState(Taskbar.ThumbnailProgressState.Error);
        TaskDialogHelper.ShowMessageBox(
            Handle,
            "Failed to Open in Visual Studio",
            "Failed to open in Visual Studio. Try to use another version of Visual Studio.",
            filename + ":" + line + "\n" + ex.Message,
            TaskDialogStandardIcon.Error
          );
      }
      EndLoading("");
    }

    protected override void WndProc(ref Message m)
    {
      if (commandManager.ProcessMessage(ref m))
        return;

      if (clipboardWatcher != null && clipboardWatcher.ProcessMessage(ref m))
        return;

      base.WndProc(ref m);
    }

    #region Drag'n'drop

    private bool lockDragNDrop;

    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      IDataObject dataObject = e.Data;

      if (lockDragNDrop || !dataObject.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      string ext = Path.GetExtension(((string[])dataObject.GetData(DataFormats.FileDrop))[0]).ToLower();

      e.Effect = FormatFactory.HasExtension(ext) || ext == ".pdb" ? DragDropEffects.Move : DragDropEffects.None;
    }

    private async void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      IDataObject dataObject = e.Data;

      if (!dataObject.GetDataPresent(DataFormats.FileDrop))
        return;

      List<string> files = new List<string>((string[])e.Data.GetData(DataFormats.FileDrop));
      List<string> pdbFiles = null;

      string fileToOpen = null;
      int i = 0;
      do
      {
        string ext = Path.GetExtension(files[i]).ToLower();

        // search for xml files to open
        if (FormatFactory.HasExtension(ext))
        {
          fileToOpen = files[i];
          files.RemoveAt(i);
          continue;
        }

        // search for pdb files to attach after open
        if (ext == ".pdb")
        {
          if (pdbFiles == null)
            pdbFiles = new List<string>();
          pdbFiles.Add(files[i]);
        }

        i++;
      }
      while (i < files.Count);

      // open file if found. found pdbs will be attached after open (pdbToAttach)
      if (fileToOpen != null)
      {
        if (TaskDialogHelper.ShowMessageBox(
                Handle,
                "Open File",
                "Open file?",
                fileToOpen,
                TaskDialogStandardIcon.Information,
                TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No
              ) !=
            TaskDialogResult.Yes
          )
          return;

        await OpenFile(fileToOpen);
        return;
      }

      if (mapping == null)
        return;

      AttachPDB(pdbFiles, true);
    }

    #endregion

    private void MainForm_Activated(object sender, EventArgs e)
    {
      if (mapping == null)
        return;

      foreach (PdbFile pdbFile in mapping.PdbFiles)
        if (pdbFile.CheckFileModification() &&
            TaskDialogHelper.ShowTaskDialog(
                  Handle,
                  "PDB File Change Detected",
                  "External PDB file change detected. Reload?",
                  pdbFile.Filename,
                  TaskDialogStandardIcon.Information, 
                  new string[]{"Reload", "Don't reload"},
                  null,
                  new TaskDialogResult[]{TaskDialogResult.Yes, TaskDialogResult.No}
                ) == TaskDialogResult.Yes)
          pdbFile.ReloadFile();

      if (mapping.CheckModifications() &&
            TaskDialogHelper.ShowTaskDialog(
                  Handle,
                  "Mapping File Change Detected",
                  "External mapping file change detected. Reload?",
                  mapping.Mapping.Filename,
                  TaskDialogStandardIcon.Information,
                  new string[] { "Reload", "Don't reload" },
                  null,
                  new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No }
                ) == TaskDialogResult.Yes)
        ReloadFile();
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
      commandManager.WindowHandle = Handle;
      Configs.Instance.UpdateHelper.Initialize(UpdateFound);

      if (!string.IsNullOrEmpty(fileToLoad))
        await OpenFile(fileToLoad);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      NanoXmlElement el = new NanoXmlElement("Actions");
      commandManager.SaveToXml(el);
      Configs.Instance.CommandsElement = el;
    }

    private void UpdateFound(UpdateHelper updateHelper)
    {
      TaskDialogResult result = TaskDialogHelper.ShowTaskDialog(
          Handle,
          "Update is Available",
          $"The Obfuscar Mapping Parser update to {updateHelper.UpdateAvailable.Version} is available. Update now?",
          updateHelper.UpdateAvailable.Description,
          TaskDialogStandardIcon.Information,
          new string[] { "Update Now", "Schedule Update", "Ignore Update", "Cancel" },
          new string[] { "Perform update and restart app", "Perform update when app is closed", "Don't update to this version", null },
          new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.Ok, TaskDialogResult.No, TaskDialogResult.Cancel, }
        );

      switch (result)
      {
        // update now
        case TaskDialogResult.Yes:
          updateHelper.ScheduleUpdate(
              UpdateHelper.BuildCommandLine(
                  null,
                  new Dictionary<string, string>
                  {
                    { "doRestart", null },
                    { "restartCmd", mappingFilename }
                  }
                )
            );
          Close();
          break;

        // schedule
        case TaskDialogResult.Ok:
          // run updater silently
          updateHelper.ScheduleUpdate(updateHelper.SilentCommandline);
          break;

        // ignore
        case TaskDialogResult.No:
          updateHelper.IgnoreUpdate();
          break;
      }
    }

    private async void miUpdateVersion_Click(object sender, EventArgs e)
    {
      BeginLoading("Checking for updates");
      UpdateHelper.UpdateCheckResult result = await Configs.Instance.UpdateHelper.CheckForUpdates();

      switch (result)
      {
        case UpdateHelper.UpdateCheckResult.NoUpdates:
          EndLoading("");
          TaskDialogHelper.ShowMessageBox(
              Handle,
              "No Updates",
              $"You are using the most recent version of the {APP_TITLE}.",
              null,
              TaskDialogStandardIcon.Information
            );
          break;

        case UpdateHelper.UpdateCheckResult.UpdateFound:
          EndLoading("");
          UpdateFound(Configs.Instance.UpdateHelper);
          break;

        case UpdateHelper.UpdateCheckResult.Failure:
          EndLoading("Failed to check for updates.");
          TaskDialogHelper.ShowMessageBox(
              Handle,
              "Failure",
              "Unable to check for updates.",
              null,
              TaskDialogStandardIcon.Error
            );
          break;
      }
    }

    private void miReport_Click(object sender, EventArgs e)
    {
      RestApi.Instance.SendProblemReport(this);
    }

    private async void miVersionHistory_Click(object sender, EventArgs e)
    {
      BeginLoading("Receiving version history");
      await UpdateHelper.ShowVersionHistory();
      EndLoading("");
    }

    private void miRequestFeature_Click(object sender, EventArgs e)
    {
      BaseForm.OpenUrl(this, "https://trello.com/c/BCIHDF4b/22-feature-requests");
    }

    private void miDonate_Click(object sender, EventArgs e)
    {
      BaseForm.OpenUrl(this, "https://brokenevent.com/projects/obfuscarparser/donate");
    }

    private void miGitHub_Click(object sender, EventArgs e)
    {
      BaseForm.OpenUrl(this, "https://github.com/BrokenEvent/ObfuscarMappingParser");
    }
  }
}