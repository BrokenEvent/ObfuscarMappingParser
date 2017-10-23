using System;
using System.Windows.Forms;

using BrokenEvent.Shared.Rest;

namespace ObfuscarMappingParser
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
#if !DEBUG
      RestApi.RegisterTopLevelExceptionHandling();
#endif

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      RestApi.Init(new AppIdentity());

      string filename;
      if (!CommandLine.ProcessCommandline(out filename, args))
        return;

      if (filename == null)
      {
        LauncherForm launcher = new LauncherForm();
        if (launcher.ShowDialog() != DialogResult.OK)
          return;
        filename = launcher.SelectedFilename;
      }

      MainForm form = new MainForm(filename);
      AppIdentity.MainForm = form;
      Application.Run(form);

      Configs.Instance.SaveConfigs();
    }
  }
}
