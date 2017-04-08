using System.Windows.Forms;

using BrokenEvent.Shared.CommandManager;

namespace ObfuscarMappingParser
{
  partial class MainForm
  {
    private CommandManager<Actions> commandManager = new CommandManager<Actions>();
    private static readonly Actions[] MAPPING_RELATED_ACTIONS = new Actions[]
    {
      Actions.DeobfuscateStacktrace,
      Actions.AnalyzeStacktrace, 
      Actions.AttachPdb, 
      Actions.ManagePdb, 
      Actions.Search,
      Actions.SearchForOriginal,
      Actions.Statistics,
      Actions.ReloadFile,
    };

    private static readonly Actions[] SELECTION_RELATED_ACTIONS = new Actions[]
    {
      Actions.CopyFullNewName, 
      Actions.CopyFullOldName, 
      Actions.CopyNewName, 
      Actions.CopyOldName,
    };

    private void InitCommandManager()
    {
      commandManager.RegisterHotkey(Actions.OpenFile, Action_Open, ModifierKey.Ctrl, Keys.O)
        .AddItem(miOpen)
        .AddItem(miOpenBtn);
      commandManager.RegisterHotkey(Actions.ReloadFile, Action_Reload, ModifierKey.None, Keys.None)
        .AddItem(miReload);
      commandManager.RegisterHotkey(Actions.AttachPdb, Action_AttachPDB, ModifierKey.None, Keys.None)
        .AddItem(miAttachPDB);
      commandManager.RegisterHotkey(Actions.ManagePdb, Action_ManagePDB, ModifierKey.None, Keys.None)
        .AddItem(miManagePDBs);
      commandManager.RegisterHotkey(Actions.Exit, Action_Exit, ModifierKey.None, Keys.None)
        .AddItem(miExit);
      commandManager.RegisterHotkey(Actions.Statistics, Action_Statistics, ModifierKey.None, Keys.None)
        .AddItem(miStatistics);
      commandManager.RegisterHotkey(Actions.Settings, Action_Settings, ModifierKey.None, Keys.None)
        .AddItem(miSettings);
      commandManager.RegisterHotkey(Actions.DeobfuscateStacktrace, Action_Deobfuscate, ModifierKey.Ctrl, Keys.S)
        .AddItem(miCrashlogs)
        .AddItem(btnCrashLogs);
      commandManager.RegisterHotkey(Actions.AnalyzeStacktrace, Action_Analyze, ModifierKey.Ctrl, Keys.A)
        .AddItem(miStacktrace);
      commandManager.RegisterHotkey(Actions.Search, Action_Search, ModifierKey.Ctrl, Keys.F)
        .AddItem(miSearch);
      commandManager.RegisterHotkey(Actions.SearchForOriginal, Action_SearchOriginal, ModifierKey.Ctrl | ModifierKey.Shift, Keys.F)
        .AddItem(miSearchOriginal);
      commandManager.RegisterHotkey(Actions.Convert, Action_Convert, ModifierKey.None, Keys.None)
        .AddItem(miConvert);
      commandManager.RegisterHotkey(Actions.About, Action_About, ModifierKey.None, Keys.None)
        .AddItem(miAbout);
      commandManager.RegisterHotkey(Actions.CopyOldName, Action_CopyOldName, ModifierKey.None, Keys.None)
        .AddItem(miCopyOldName);
      commandManager.RegisterHotkey(Actions.CopyFullOldName, Action_CopyFullOldName, ModifierKey.None, Keys.None)
        .AddItem(miCopyFullOldName);
      commandManager.RegisterHotkey(Actions.CopyNewName, Action_CopyNewName, ModifierKey.None, Keys.None)
        .AddItem(miCopyNewName);
      commandManager.RegisterHotkey(Actions.CopyFullNewName, Action_CopyFullNewName, ModifierKey.None, Keys.None)
        .AddItem(miCopyFullNewName);
      commandManager.RegisterHotkey(Actions.OpenInEditor, Action_OpenInEditor, ModifierKey.None, Keys.None)
        .AddItem(miOpenVS);

      if (Configs.Instance.CommandsElement != null)
        commandManager.LoadFromXml(Configs.Instance.CommandsElement);
    }

    private void EnableMappingActions(bool haveMapping)
    {
      foreach (Actions actions in MAPPING_RELATED_ACTIONS)
        commandManager.SetEnabled(actions, haveMapping);
    }

    private void EnableSelectionActions(bool haveSelection)
    {
      foreach (Actions actions in SELECTION_RELATED_ACTIONS)
        commandManager.SetEnabled(actions, haveSelection);
    }

    private void Action_OpenInEditor(Actions command)
    {
      OpenInVisualStudio(focusedFilename, focusedLine);
    }

    private void Action_CopyFullNewName(Actions command)
    {
      Clipboard.SetText(focusedItem.NameNewFull);
    }

    private void Action_CopyNewName(Actions command)
    {
      Clipboard.SetText(focusedItem.NameNewSimple);
    }

    private void Action_CopyFullOldName(Actions command)
    {
      Clipboard.SetText(focusedItem.NameOldFull);
    }

    private void Action_CopyOldName(Actions command)
    {
      Clipboard.SetText(focusedItem.NameOldSimple);
    }

    private void Action_About(Actions command)
    {
      using (AboutForm about = new AboutForm())
        about.ShowDialog(this);
    }

    private void Action_Convert(Actions command)
    {
      using (ConvertSettingsForm form = new ConvertSettingsForm())
        form.ShowDialog(this);
    }

    private void Action_SearchOriginal(Actions command)
    {
      using (SearchDialog form = new SearchDialog(this, true))
        form.ShowDialog(this);
    }

    private void Action_Search(Actions command)
    {
      using (SearchDialog form = new SearchDialog(this, false))
        form.ShowDialog(this);
    }

    private void Action_Analyze(Actions command)
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

    private void Action_Deobfuscate(Actions command)
    {
      AddFormToOpened(new CrashLogForm(mapping)).Show(this);
    }

    private void Action_Settings(Actions command)
    {
      using (SettingsForm form = new SettingsForm(mapping))
        form.ShowDialog(this);

      if (mapping != null)
        BuildMapping();
    }

    private void Action_Statistics(Actions command)
    {
      using (StatisticsForm form = new StatisticsForm(mapping))
        form.ShowDialog(this);
    }

    private void Action_Exit(Actions command)
    {
      Close();
    }

    private void Action_ManagePDB(Actions command)
    {
      lockDragNDrop = true;
      using (PDBManagerForm form = new PDBManagerForm(pdbfiles, this))
        form.ShowDialog(this);
      lockDragNDrop = false;
    }

    private void Action_AttachPDB(Actions command)
    {
      CallAttachPdb(this);
    }

    private void Action_Reload(Actions command)
    {
      ReloadFile();
    }

    private void Action_Open(Actions command)
    {
      if (odMapping.ShowDialog(this) != DialogResult.OK)
        return;

      OpenFile(odMapping.FileName);
    }
  }
}
