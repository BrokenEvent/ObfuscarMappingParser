using System.Drawing;
using System.Windows.Forms;

using BrokenEvent.Shared.Rest;

namespace ObfuscarMappingParser
{
  class AppIdentity : IAppIdentity
  {
    public static IWin32Window MainForm { get; set; }

    public string AppId
    {
      get { return "obfuscarparser"; }
    }

    public string AppName
    {
      get { return "Obfuscar Mapping Parser"; }
    }

    public void RestartApp() { }

    public void Save()
    {
      Configs.Instance.SaveConfigs();
    }

    public Color AccentColor
    {
      get { return Color.RoyalBlue; }
    }

    public Color AccentTextColor
    {
      get { return Color.White; }
    }

    public IWin32Window MainWindow
    {
      get { return MainForm; }
    }

  }
}
