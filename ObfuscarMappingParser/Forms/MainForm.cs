using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrokenEvent.NanoXml;
using BrokenEvent.PDBReader;
using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.Algorithms;
using BrokenEvent.Shared.Controls;
using BrokenEvent.Shared.Rest;
using BrokenEvent.Shared.WinApi;
using BrokenEvent.TaskDialogs;
using BrokenEvent.TaskDialogs.Dialogs;
using BrokenEvent.VisualStudioOpener;

using ObfuscarMappingParser.Properties;

namespace ObfuscarMappingParser
{
  partial class MainForm : Form
  {
    private Mapping mapping;
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

    private void AddIcon(Bitmap bmp, out int index)
    {
      ilIcons.Images.Add(bmp);
      index = ilIcons.Images.Count - 1;
    }

    public MainForm(string filename)
    {
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

      ptvElements.Highlights.Add(new ListHighlight(Color.DarkRed));
      ptvElements.Highlights.Add(new ListHighlight(Color.DarkBlue));

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

      if (!string.IsNullOrEmpty(filename))
        OpenFile(filename);
      else
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
        stacktrace = mapping.ProcessCrashlogText(stacktrace, skipPrefixes);
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
            new CrashReport("Mapping loading thread failed handler", "Error on loading document") { Exception = e },
            RestApi.CrashType.JustReport,
            this
          );
    }

    private async void OpenFile(string filename)
    {
      Text = $"{APP_TITLE} - {PathUtils.GetFilename(filename)}";
      Configs.Instance.AddRecent(filename);

      BeginLoading($"Loading: {filename}");

      while (openedForms.Count > 0)
        openedForms[0].Close();

      try
      {
        mapping = await Task.Run(() => new Mapping(filename));
      }
      catch (Exception e)
      {
        HandleMappingLoadingException(e, filename);
        EndLoading("Loading failed.");
        return;
      }

      pdbfiles.Clear();
      BuildMapping();
      EnableMappingActions(true);
      EndLoading($"Mapping loaded in {mapping.LoadTime} ms");

      AttachRelatedPdbs(Configs.Instance.GetRecentPdb(mapping.Filename), false);
      AttachRelatedPdbs(pdbToAttach, true);
      pdbToAttach = null;

      tbSearch.AutoCompleteCustomSource = mapping.GetNewNamesCollection();
    }

    private async void ReloadFile()
    {
      BeginLoading($"Reloading: {mapping.Filename}");
      while (openedForms.Count > 0)
        openedForms[0].Close();

      try
      {
        await Task.Run(()=> mapping.Reload());
      }
      catch (Exception e)
      {
        HandleMappingLoadingException(e, mapping.Filename);
        EndLoading("Reloading failed.");
        return;
      }

      BuildMapping();
      EnableMappingActions(true);
      EndLoading($"Mapping reloaded in {mapping.LoadTime} ms");
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
      try
      {
        builder.Build();
      }
      catch (Exception e)
      {
        RestApi.Instance.SendCrashReport(
            new CrashReport("Mapping tree builder", "Error on building classes tree"){ Exception = e},
            RestApi.CrashType.JustReport,
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

      SearchResults result = mapping.Search(tbSearch.Text, false, false);

      if (result == null || !result.HasValue)
        return;

      ptvElements.Focus();

      if (result.IsSingleResult)
      {
        ptvElements.SelectedNode = ((RenamedBase)result.SingleResult).TreeNode;
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

    public Mapping Mapping
    {
      get { return mapping; }
    }

    public bool HavePdb
    {
      get { return pdbfiles.Count > 0; }
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
      commandManager.SetEnabled(Actions.OpenInEditor, focusedItem != null && DetectMarkersForVS(out focusedFilename, out focusedLine, focusedItem));
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

    private void RecentItem_Click(object sender, EventArgs eventArgs)
    {
      string filename = (string)((ToolStripMenuItem)sender).Tag;
      if (!File.Exists(filename))
      {
        MessageBox.Show(this, "File\n" + filename + "\ndoesn't exist.", "File not exist", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      OpenFile(filename);
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

    private List<PdbFile> pdbfiles = new List<PdbFile>();
    private List<string> pdbToAttach;

    private void AttachRelatedPdbs(IList<string> pdb, bool addToRecent)
    {
      if (pdb == null)
        return;

      TaskDialogResult allResult = TaskDialogResult.None;

      foreach (string s in pdb)
      {
        if (!File.Exists(s))
          continue;

        TaskDialogResult result = allResult;

        if (result == TaskDialogResult.None)
          using (TaskDialog dialog = TaskDialogHelper.ConstructTaskDialog(
              Handle,
              "Attach PDB File",
              "Attach related PDB file?",
              s,
              TaskDialogStandardIcon.Information
            ))
          {
            dialog.AddCommandLink(TaskDialogResult.Yes, "Attach");
            dialog.AddCommandLink(TaskDialogResult.Yes, "Don't attach");
            dialog.FooterCheckBoxText = "Do this for other PDBs of this mapping.";
            dialog.FooterCheckBoxChecked = false;

            result = dialog.Show();
            if (dialog.FooterCheckBoxChecked.Value)
              allResult = result;
          }

        switch (result)
        {
          case TaskDialogResult.Yes:
            if (AttachPDB(s, this) && addToRecent)
              Configs.Instance.AddRecentPdb(mapping.Filename, s);
            break;

          case TaskDialogResult.No:
            break;

          default:
            return;
        }
      }
    }

    public bool CallAttachPdb(IWin32Window owner)
    {
      if (odPDB.ShowDialog(owner) != DialogResult.OK)
        return false;

      foreach (string fileName in odPDB.FileNames)
        if (AttachPDB(fileName, owner))
          Configs.Instance.AddRecentPdb(mapping.Filename, fileName);

      return true;
    }

    private bool SearchForLoadedPdb(string filename)
    {
      foreach (PdbFile file in pdbfiles)
        if (string.Compare(file.Filename, filename, StringComparison.OrdinalIgnoreCase) == 0)
          return true;

      return false;
    }

    public bool AttachPDB(string filename, IWin32Window owner)
    {
      if (SearchForLoadedPdb(filename))
      {
        slblSelected.Text = "PDB file already attached: " + PathUtils.GetFilename(filename);
        return false;
      }

      try
      {
        pdbfiles.Add(new PdbFile(filename));
      }
      catch (Exception ex)
      {
        MessageBox.Show(owner, "Failed to load PDB file: " + ex.Message, "Failed to load PDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      slblSelected.Text = "PDB file attached: " + PathUtils.GetFilename(filename);
      return true;
    }

    public bool SearchInPdb(out string filename, out int lineNumber, RenamedBase item)
    {
      filename = null;
      lineNumber = -1;

      string s = item.NameOldPlain;
      int i = s.LastIndexOf('.');
      if (i == -1)
        return false;

      string className = s.Substring(0, i);
      string itemName = s.Substring(i + 1);
      CodeLocation location = null;

      foreach (PdbFile file in pdbfiles)
      {
        location = file.Resolver.FindLocation(className, itemName);
        if (location != null)
          break;
      }

      if (location == null)
        return false;

      filename = location.FileName;
      lineNumber = (int)location.Line;
      return true;
    }

    private bool DetectMarkersForVS(out string filename, out int lineNumber, RenamedBase item)
    {
      filename = null;
      lineNumber = -1;

      if (pdbfiles.Count == 0 || item.EntityType != EntityType.Method)
        return false;

      return SearchInPdb(out filename, out lineNumber, item);
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
      string vs = Configs.Instance.GetRecentProperty(mapping.Filename, Configs.PROPERTY_EDITOR);
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
      if (!lockDragNDrop && !e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      e.Effect = DragDropEffects.Move;
    }

    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;

      List<string> files = new List<string>((string[])e.Data.GetData(DataFormats.FileDrop));

      string fileToOpen = null;
      int i = 0;
      do
      {
        // search for xml files to open
        if (string.Compare(Path.GetExtension(files[i]).ToLower(), ".xml", StringComparison.Ordinal) == 0)
        {
          fileToOpen = files[i];
          files.RemoveAt(i);
          continue;
        }

        // search for pdb files to attach after open
        if (string.Compare(Path.GetExtension(files[i]).ToLower(), ".pdb", StringComparison.Ordinal) == 0)
        {
          if (pdbToAttach == null)
            pdbToAttach = new List<string>();
          pdbToAttach.Add(files[i]);
        }

        i++;
      }
      while (i < files.Count);

      // open file if found. found pdbs will be attached after open (pdbToAttach)
      if (fileToOpen != null)
      {
        if (MessageBox.Show(this, "Open file\n" + fileToOpen + "\n?", "Open file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;

        OpenFile(fileToOpen);
        return;
      }

      if (mapping == null)
        return;

      // attach pdb files
      foreach (string file in files)
      {
        if (string.Compare(Path.GetExtension(file).ToLower(), ".pdb", StringComparison.Ordinal) == 0)
        {
          TaskDialogResult d = TaskDialogHelper.ShowTaskDialog(
              Handle,
              "Attach PDB File",
              "Attach related PDB file?",
              file,
              TaskDialogStandardIcon.Information,
              new string[] { "Attach", "Don't attach", "Cancel operation" },
              null,
              new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No, TaskDialogResult.Cancel, }
            );
          switch (d)
          {
            case TaskDialogResult.Yes:
              AttachPDB(file, this);
              break;
            case TaskDialogResult.No:
              break;
            default:
              return;
          }
        }
      }
    }

    #endregion

    private void MainForm_Activated(object sender, EventArgs e)
    {
      if (mapping == null)
        return;

      foreach (PdbFile pdbFile in pdbfiles)
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
                  mapping.Filename,
                  TaskDialogStandardIcon.Information,
                  new string[] { "Reload", "Don't reload" },
                  null,
                  new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No }
                ) == TaskDialogResult.Yes)
        ReloadFile();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      commandManager.WindowHandle = Handle;
      CheckForUpdates(true);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      NanoXmlElement el = new NanoXmlElement("Actions");
      commandManager.SaveToXml(el);
      Configs.Instance.CommandsElement = el;
    }

    private async void CheckForUpdates(bool silent)
    {
      miUpdateVersion.Enabled = false;
      VersionResponse oldVersion = Configs.Instance.UpdateHelper.UpdateAvailable;
      await Task.Run(() => Configs.Instance.UpdateHelper.CheckForUpdates());

      VersionResponse version = Configs.Instance.UpdateHelper.UpdateAvailable;
      miUpdateVersion.Enabled = true;

      if (version == null || version == oldVersion)
      {
        miUpdateVersion.Text = "Check for Updates";
        if (!silent)
          MessageBox.Show(this, $"You are using the most recent version of the {APP_TITLE}.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      miUpdateVersion.Text = $"Update to {version.Version}...";
      if (TaskDialogHelper.ShowTaskDialog(
              Handle,
              "Update is Available",
              "The Obfuscar Mapping Parser update is available. Update now?",
              version.Description,
              TaskDialogStandardIcon.Information, 
              new string[]{"Update now", "Don't update"},
              null,
              new TaskDialogResult[]{TaskDialogResult.Yes, TaskDialogResult.No }
            ) == TaskDialogResult.Yes)
        DoUpdateVersion();
    }

    private void DoUpdateVersion()
    {
      BaseForm.OpenUrl(this, Configs.Instance.UpdateHelper.UpdateAvailable.InstallerUrl.ToString());
    }

    private void miUpdateVersion_Click(object sender, EventArgs e)
    {
      if (Configs.Instance.UpdateHelper.UpdateAvailable != null)
        DoUpdateVersion();
      else
        CheckForUpdates(false);
    }

    private void miReport_Click(object sender, EventArgs e)
    {
      RestApi.Instance.SendProblemReport(this);
    }

    private async void miVersionHistory_Click(object sender, EventArgs e)
    {
      BeginLoading("Receiving version history");
      RestResponse<HistoryResponse> response = await Task.Run((Func<RestResponse<HistoryResponse>>)RestApi.Instance.GetVersionHistory);

      if (response.Result == null)
      {
        EndLoading("Failed to receive history.");
        return;
      }

      EndLoading("");
      StringBuilder sb = new StringBuilder();

      for (int i = response.Result.History.Count - 1; i >= 0; i--)
      {
        HistoryResponse.VersionInfo info = response.Result.History[i];
        sb.Append("Version: ").Append(info.Version).Append("; ");
        sb.Append("Branch: ").Append(info.Branch).Append("; ");
        sb.Append("Date: ").Append(info.ReleaseDate.ToLongDateString());
        sb.AppendLine(". Changes:");

        string[] lines = info.Description.Split('\n');
        foreach (string s in lines)
          sb.AppendLine(s);
        sb.AppendLine();
      }

      string filename = Path.GetTempFileName() + ".txt";
      File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
      ProcessStartInfo startInfo = new ProcessStartInfo(filename);
      startInfo.Verb = "open";
      startInfo.UseShellExecute = true;
      Process process = Process.Start(startInfo);
      process.WaitForInputIdle();
      File.Delete(filename);
    }

  }
}