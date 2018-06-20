using System;
using System.Threading.Tasks;

using BrokenEvent.Shared.Forms;

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

    private void llblUpdate_Click(object sender, EventArgs e)
    {
      if (Configs.Instance.UpdateHelper.UpdateAvailable != null)
        OpenUrl(Configs.Instance.UpdateHelper.UpdateAvailable.InstallerUrl.ToString());
      else
        DoUpdateCheck();
    }

    protected override string AssemblyVersionToString(Version version)
    {
      return version.ToString();
    }

    private async void DoUpdateCheck()
    {
      indUpdate.IndicatorEnabled = true;
      llblUpdate.Visible = false;
      lblUpdateState.Visible = true;

      bool updateCheckSuccess = await Task.Run(() => Configs.Instance.UpdateHelper.CheckForUpdates(true));

      indUpdate.IndicatorEnabled = false;

      if (updateCheckSuccess)
      {
        if (Configs.Instance.UpdateHelper.UpdateAvailable != null)
        {
          llblUpdate.Visible = true;
          llblUpdate.Text = $"Update to {Configs.Instance.UpdateHelper.UpdateAvailable.Version}";
          lblUpdateState.Visible = false;
        }
        else
          lblUpdateState.Text = "You are using an actual version.";
      }
      else
      {
        llblUpdate.Visible = true;
        llblUpdate.Text = "Update check failed. Check again?";
        lblUpdateState.Visible = false;
      }
    }

    private void AboutForm_Load(object sender, EventArgs e)
    {
      DoUpdateCheck();
    }
  }
}
