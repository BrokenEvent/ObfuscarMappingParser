using System.Diagnostics;
using System.Windows.Forms;
using BrokenEvent.Shared.CrashReporter;

namespace ObfuscarMappingParser
{
  class CrashHandler: CommonCrashHandler
  {
    private static CrashHandler instance;

    public static void InitInstance(MainForm mainForm)
    {
      instance = new CrashHandler(mainForm);
    }

    public static CrashHandler Instance
    {
      get { return instance; }
    }

    private MainForm mainForm;

    public CrashHandler(MainForm mainForm)
    {
      this.mainForm = mainForm;
    }

    public override string AppName
    {
      get { return "Obfuscar Mapping Parser"; }
    }

    public override void RestartApp()
    {
      Process.Start(new ProcessStartInfo(Application.ExecutablePath, mainForm.Mapping != null ? mainForm.Mapping.Filename : null));
    }

    public override string UserAgent
    {
      get { return "ObfuscarParser.Agent"; }
    }

    public void MakePromblemReport(CrashReportInfo info, bool canResume)
    {
      new CrashReportForm(info, canResume, this).ShowDialog(mainForm);
    }
  }
}
