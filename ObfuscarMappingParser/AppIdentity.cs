using System.Drawing;
using System.Windows.Forms;

using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.Rest;

namespace ObfuscarMappingParser
{
  class AppIdentity : IAppIdentity, BaseForm.IBaseFormThemeProvider
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

    public bool CanRestart
    {
      get { return false; }
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

    #region BaseForm.IBaseFormThemeProvider

    public Color HeaderFillColor
    {
      get { return AccentColor; }
    }

    public Color HeaderTextColor
    {
      get { return AccentTextColor; }
    }

    public Color BackgroundColor
    {
      get { return Color.White; }
    }

    #endregion

  }
}
