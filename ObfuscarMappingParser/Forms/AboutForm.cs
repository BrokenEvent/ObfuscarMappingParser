using System;
using System.Threading.Tasks;

using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  partial class AboutForm : BaseAboutForm
  {
    public AboutForm()
    {
      InitializeComponent();
      VersionLabel = lblAppVersion;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Close();
    }

    protected override string AssemblyVersionToString(Version version)
    {
      return version.ToString();
    }

    private void llblUpdate_Click(object sender, EventArgs e)
    {
      OpenUrl(Configs.Instance.UpdateHelper.UpdateAvailable.InstallUrl.ToString());
    }

    private async void AboutForm_Load(object sender, EventArgs e)
    {
      indUpdate.IndicatorEnabled = true;
      lblUpdateState.Visible = true;

      await Task.Run(() => Configs.Instance.UpdateHelper.CheckForUpdates(true));

      indUpdate.IndicatorEnabled = false;
      lblUpdateState.Visible = false;

      if (Configs.Instance.UpdateHelper.UpdateAvailable != null)
        llblUpdate.Visible = true;
    }
  }
}
