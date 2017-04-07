using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BrokenEvent.NanoXml;
using BrokenEvent.PdbReader;
using BrokenEvent.Shared;
using BrokenEvent.Shared.CrashReporter;
using BrokenEvent.Shared.TreeView;
using BrokenEvent.TaskDialogs;
using BrokenEvent.TaskDialogs.Dialogs;

using ObfuscarMappingParser.Properties;

namespace ObfuscarMappingParser
{
  partial class MainForm : Form
  {
    private Mapping mapping;
    private List<PDBFile> pdbfiles = new List<PDBFile>();
    private List<string> pdbToAttach;
    private List<Form> openedForms = new List<Form>();

    public MainForm(string filename)
    {
      InitializeComponent();

      Application.ThreadException += ApplicationOnThreadException;

      try
      {
        Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
      }
      catch { }

      ptvElements.Columns.Add(new PineappleTreeColumn(100));

      ptvElements.Highlights.Add(new PineappleTreeHighlight(Color.DarkRed));
      ptvElements.Highlights.Add(new PineappleTreeHighlight(Color.DarkBlue));

      miShowModule.Checked = Configs.Instance.ShowModules;
      miGroupNamespace.Checked = Configs.Instance.GroupNamespaces;
      miGroupModules.Checked = Configs.Instance.GroupModules;
      miUseColumns.Checked = Configs.Instance.UseColumns;

      miSortAscending.Tag = Configs.SortingTypes.OriginalNameAscending;
      miSortDescending.Tag = Configs.SortingTypes.OriginalNameDescending;
      miSortNewAscending.Tag = Configs.SortingTypes.NewNameAscending;
      miSortNewDesc.Tag = Configs.SortingTypes.NewNameDescending;
      SortingType = Configs.Instance.SortingType;

      if (!string.IsNullOrEmpty(filename))
        OpenFile(filename);

      VSOpener.VisualStudioOpeningStartEvent = VisualStudioOpeningStart;
      VSOpener.VisualStudioOpeningCompleteEvent = VisualStudioOpeningComplete;

      CrashHandler.InitInstance(this);
    }

    private string lastName;

    private void VisualStudioOpeningComplete()
    {
      EnableCommonControls(true);
      lastName = Text;
      slblSelected.Text = string.Empty;
      Application.DoEvents();
    }

    private void VisualStudioOpeningStart()
    {
      EnableCommonControls(false);
      Text = lastName;
      slblSelected.Text = Text = "Starting Visual Studio...";
      Application.DoEvents();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (odMapping.ShowDialog(this) != DialogResult.OK)
        return;

      OpenFile(odMapping.FileName);      
    }

    private void OpenFile(string filename)
    {
      Text = "Obfuscar Mapping Parser - " + PathUtils.GetFilename(filename);
      Configs.Instance.AddRecent(filename);

      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.Indeterminate);
      spbLoading.Visible = true;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = false;
      slblSelected.Text = "Loading: " + filename;
      new MappingLoaderThread(filename).Start(MappingLoadingCompleted);

      while (openedForms.Count > 0)
        openedForms[0].Close();
    }

    private void ReloadFile()
    {
      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.Indeterminate);
      spbLoading.Visible = true;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = false;
      slblSelected.Text = "Loading: " + mapping.Filename;
      new MappingReloaderThread(mapping).Start(MappingReloadingCompleted);

      while (openedForms.Count > 0)
        openedForms[0].Close();
    }

    private void EnableCommonControls(bool enable)
    {
      menuStrip.Enabled = enable;
      ptvElements.Enabled = enable;
      tsTools.Enabled = enable;
    }

    private void AttachRelatedPdbs(IList<string> pdb, bool addToRecent)
    {
      if (pdb == null)
        return;

      foreach (string s in pdb)
      {
        if (!File.Exists(s))
          continue;

        TaskDialogResult d = TaskDialogHelper.ShowTaskDialog(
            Handle,
            "Attach PDB File",
            "Attach related PDB file?",
            s,
            TaskDialogStandardIcon.Information,
            new string[] { "Attach", "Don't attach" },
            null,
            new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No, }
          );

        switch (d)
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

    private void MappingLoadingCompletedInternal(object mappingObj, string filename)
    {
      mapping = (Mapping)mappingObj;
      pdbfiles.Clear();
      BuildMapping();
      btnCrashLogs.Enabled = tbSearch.Enabled = mmSearch.Enabled = miAttachPDB.Enabled = miStatistics.Enabled = true;
      spbLoading.Visible = false;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = true;
      slblSelected.Text = "Mapping loaded in " + mapping.TimingTotal + " ms";
      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.NoProgress);

      AttachRelatedPdbs(Configs.Instance.GetRecentPdb(mapping.Filename), false);
      AttachRelatedPdbs(pdbToAttach, true);
      pdbToAttach = null;

      tbSearch.AutoCompleteCustomSource = mapping.GetNewNamesCollection();
    }

    private void MappingLoadingFailedInternal(object e, string filename)
    {      
      btnCrashLogs.Enabled = tbSearch.Enabled = mmSearch.Enabled = true;
      pdbToAttach = null;
      spbLoading.Visible = false;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = true;
      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.NoProgress);

      if (e is NanoXmlParsingException ||
          e is ObfuscarParserException ||
          e is IOException)
        TaskDialogHelper.ShowMessageBox(
            Handle,
            "Mapping Loading Failed",
            "Loading of mapping file is failed.",
            "File:\n" + filename + "\nReason: " + ((Exception)e).Message,
            TaskDialogStandardIcon.Error
          );
      else
        CrashHandler.Instance.MakePromblemReport(
            new CrashReportInfo(
                "Mapping loading thread failed handler",
                "Error on loading document",
                (Exception)e
              ),
            true
          );
    }

    private void MappingLoadingCompleted(object result, string filename)
    {
      if (result is Mapping)
        Invoke(new MappingLoaderThread.LoadingThreadCompleted(MappingLoadingCompletedInternal), result, filename);
      else
        Invoke(new MappingLoaderThread.LoadingThreadCompleted(MappingLoadingFailedInternal), result, filename);
    }

    private void MappingReloadingCompleted(object result, string filename)
    {
      if (result is Mapping)
        Invoke(new MappingLoaderThread.LoadingThreadCompleted(MappingReloadingCompletedInternal), result, filename);
      else
        Invoke(new MappingLoaderThread.LoadingThreadCompleted(MappingReloadingFailedInternal), result, filename);
    }

    private void MappingReloadingCompletedInternal(object mappingObj, string filename)
    {
      mapping = (Mapping)mappingObj;
      BuildMapping();
      btnCrashLogs.Enabled = tbSearch.Enabled = mmSearch.Enabled = miAttachPDB.Enabled = miStatistics.Enabled = true;
      spbLoading.Visible = false;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = true;
      slblSelected.Text = "Mapping reloaded in " + mapping.TimingTotal + " ms";
      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.NoProgress);
      tbSearch.AutoCompleteCustomSource = mapping.GetNewNamesCollection();
    }

    private void MappingReloadingFailedInternal(object e, string filename)
    {
      btnCrashLogs.Enabled = tbSearch.Enabled = mmSearch.Enabled = true;
      spbLoading.Visible = false;
      menuStrip.Enabled = ptvElements.Enabled = tsTools.Enabled = true;
      this.SetTaskbarProgressState(Win7FormExtension.ThumbnailProgressState.NoProgress);

      if (e is NanoXmlParsingException || e is ObfuscarParserException)
        TaskDialogHelper.ShowMessageBox(
            Handle,
            "Mapping Loading Failed",
            "Loading of mapping file is failed.",
            "File:\n" + filename + "\nReason: " + ((Exception)e).Message,
            TaskDialogStandardIcon.Error
          );
      else
        CrashHandler.Instance.MakePromblemReport(
            new CrashReportInfo(
                "Mapping loading thread failed handler",
                "Error on loading document",
                (Exception)e
              ),
            true
          );
    }

    private void BuildMapping()
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      ptvElements.BeginUpdate();
      mapping.PurgeTreeNodes();

      TreeBuilder builder = new TreeBuilder(ptvElements, mapping);
      builder.GroupNamespaces = Configs.Instance.GroupNamespaces;
      builder.GroupModules = Configs.Instance.GroupModules;
      builder.ShowModules = Configs.Instance.ShowModules;
      try
      {
        builder.Build();
      }
      catch (Exception e)
      {
        CrashHandler.Instance.MakePromblemReport(
             new CrashReportInfo(
                 "Mapping tree builder",
                 "Error on building classes tree",
                 e
               ),
             true
           );
      }

      Sort(Configs.Instance.SortingType);

      ptvElements.CollapseAll();
      ptvElements.EndUpdate();
      sw.Stop();
      Debug.WriteLine("Tree building: " + sw.ElapsedMilliseconds + " ms");
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

    private void btnCrashLogs_Click(object sender, EventArgs e)
    {
      AddFormToOpened(new CrashLogForm(mapping)).Show(this);
    }

    private FormType AddFormToOpened<FormType>(FormType form) where FormType: Form
    {
      openedForms.Add(form);
      form.Closed += OpenedForm_Closed;
      return form;
    }

    private void OpenedForm_Closed(object sender, EventArgs eventArgs)
    {
      openedForms.Remove((Form)sender);
    }

    private void ptvElements_NodeSelect(object sender, NodeSelectEventArgs e)
    {
      if (e.Node == null || !e.Node.Selected || e.Node.Tag == null)
      {
        slblSelected.Text = "";
        return;
      }

      slblSelected.Text = ((RenamedBase)e.Node.Tag).TransformSimple;
    }

    private void miExit_Click(object sender, EventArgs e)
    {
      Close();
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

      miManagePDBs.Enabled = miAttachPDB.Enabled = miReload.Enabled = mapping != null;
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

    private void miAbout_Click(object sender, EventArgs e)
    {
      AboutForm about = new AboutForm();
      about.ShowDialog(this);
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
      return string.Compare(node1.Subitems[0], node2.Subitems[0], StringComparison.InvariantCulture);
    }

    private static int ElementsComparisonDesc(PineappleTreeNode node1, PineappleTreeNode node2)
    {
      if (node1.Subitems.Count == 0 || node2.Subitems.Count == 0)
        return 0;
      return string.Compare(node2.Subitems[0], node1.Subitems[0], StringComparison.InvariantCulture);
    }

    #endregion

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

    private void miStatistics_Click(object sender, EventArgs e)
    {
      new StatisticsForm(mapping).ShowDialog(this);
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

    public ImageList IconsList
    {
      get { return ilIcons; }
    }

    public Mapping Mapping
    {
      get { return mapping; }
    }

    #region Context menu

    private RenamedBase focusedItem;
    private string focusedFilename;
    private int focusedLine;

    private void miCopyOldName_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(focusedItem.NameOldSimple);
    }

    private void miCopyFullOldName_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(focusedItem.NameOldFull);
    }

    private void miCopyNewName_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(focusedItem.NameNewSimple);
    }

    private void miCopyFullNewName_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(focusedItem.NameNewFull);
    }

    private void miOpenVS_Click(object sender, EventArgs e)
    {
      OpenInVisualStudio(focusedFilename, focusedLine);
    }

    private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
      if (ptvElements.SelectedNode == null || ptvElements.SelectedNode.Tag == null)
      {
        e.Cancel = true;
        return;
      }

      focusedItem = (RenamedBase)ptvElements.SelectedNode.Tag;
      miOpenVS.Enabled = DetectMarkersForVS(out focusedFilename, out focusedLine, focusedItem);
    }

    #endregion

    public bool HavePdb
    {
      get { return pdbfiles.Count > 0; }
    }

    public bool SearchInPdb(out string filename, out int lineNumber, RenamedBase item)
    {      
      filename = null;
      lineNumber = -1;

      string s = item.NameOldPlain;
      int i = s.LastIndexOf('.');
      if (i == -1)
        return false;

      PDBFunction f = null;
      string className = s.Substring(0, i);
      string itemName = s.Substring(i + 1);
      foreach (PDBFile file in pdbfiles)
      {
        f = file.Search(className, itemName);
        if (f != null)
          break;
      }

      if (f == null)
        return false;

      filename = f.Filename;
      lineNumber = (int)f.Line;
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

    public void OpenInVisualStudio(string filename, int line)
    {
      if (!File.Exists(filename))
      {
        if (MessageBox.Show(this, "File\n" + filename + "\ndoesn't exist.\nTry to set file path directly?", "File not exists.", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) != DialogResult.Yes)
          return;

        string file = PathUtils.GetFilename(filename);
        odSourceFile.Filter = file + "|" + file;
        odSourceFile.FileName = file;
        if (odSourceFile.ShowDialog(this) != DialogResult.OK)
          return;

        filename = odSourceFile.FileName;
      }

      VSOpener.VisualStudioVersion version = Configs.Instance.VisualStudioVersion;
      string vs = Configs.Instance.GetRecentProperty(mapping.Filename, "editor");
      if (vs != null)
        version = (VSOpener.VisualStudioVersion)Enum.Parse(typeof(VSOpener.VisualStudioVersion), vs);

      try
      {
        VSOpener.OpenInVisualStudio(filename, line, version);
      }
      catch (Exception ex)
      {
        if (version != VSOpener.VisualStudioVersion.Notepad)
          TaskDialogHelper.ShowMessageBox(
              Handle,
              "Failed to Open in Visual Studio",
              "Failed to open in Visual Studio. Try to use another version of Visual Studio.",
              filename + ":" + line + "\n" + ex.Message,
              TaskDialogStandardIcon.Error
            );
        else
          MessageBox.Show( this, "Unable to open\n" + filename + ":" + line, "Failed to open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void miAttachPDB_Click(object sender, EventArgs e)
    {
      CallAttachPdb(this);
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
      foreach (PDBFile file in pdbfiles)
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
        pdbfiles.Add(new PDBFile(filename));
      }
      catch (Exception ex)
      {
        MessageBox.Show(owner, "Failed to load PDB file: " + ex.Message, "Failed to load PDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      slblSelected.Text = "PDB file attached: " + PathUtils.GetFilename(filename);
      return true;
    }

    private void miSettings_Click(object sender, EventArgs e)
    {
      new SettingsForm(mapping).ShowDialog(this);
      if (mapping != null)
        BuildMapping();
    }

    private void ptvElements_DoubleClick(object sender, EventArgs e)
    {
      if (ptvElements.SelectedNode == null || ptvElements.SelectedNode.Tag == null)
        return;

      string filename;
      int lineNumber;
      if (!DetectMarkersForVS(out filename, out lineNumber, (RenamedBase)ptvElements.SelectedNode.Tag))
        return;

      OpenInVisualStudio(filename, lineNumber);
    }

    private void miSearch_Click(object sender, EventArgs e)
    {
      new SearchDialog(this, false).ShowDialog(this);
    }

    private void miStacktrace_Click(object sender, EventArgs e)
    {
      StacktraceSourceForm source = new StacktraceSourceForm(mapping);
      if (source.ShowDialog(this) != DialogResult.OK)
        return;

      try
      {
        StacktraceAnalyerForm analyer = new StacktraceAnalyerForm(this, source.Result, source.ResultSource);
        analyer.ShowDialog(this);
      }
      catch (ObfuscarParserException ex)
      {
        MessageBox.Show(this, ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
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
              new TaskDialogResult[] { TaskDialogResult.Yes, TaskDialogResult.No, TaskDialogResult.Cancel,  }
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

    private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs args)
    {
      CrashHandler.Instance.MakePromblemReport(
          new CrashReportInfo(
              "Top-level interceptor",
              "Exception on top-level",
              args.Exception
            ),
          false
        );
    }

    private void miManagePDBs_Click(object sender, EventArgs e)
    {
      lockDragNDrop = true;
      new PDBManagerForm(pdbfiles, this).ShowDialog(this);
      lockDragNDrop = false;
    }

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

    private void miReload_Click(object sender, EventArgs e)
    {
      ReloadFile();
    }

    private void MainForm_Activated(object sender, EventArgs e)
    {
      if (mapping == null)
        return;

      foreach (PDBFile pdbFile in pdbfiles)
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

    private void miSearchOriginal_Click(object sender, EventArgs e)
    {
      new SearchDialog(this, true).ShowDialog(this);
    }

    private void miConvert_Click(object sender, EventArgs e)
    {
      new ConvertSettingsForm().ShowDialog(this);
    }
  }
}