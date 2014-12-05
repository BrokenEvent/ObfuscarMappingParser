using System;
using System.Windows.Forms;

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
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

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

      Application.Run(new MainForm(filename));

      Configs.Instance.SaveConfigs();
    }
  }
}
