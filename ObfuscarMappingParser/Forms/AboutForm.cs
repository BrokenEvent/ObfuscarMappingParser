using System;

using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.Rest;

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

      UpdateHelper.UpdateCheckResult result = await Configs.Instance.UpdateHelper.CheckForUpdates();

      switch (result)
      {
        case UpdateHelper.UpdateCheckResult.UpdateFound:
          llblUpdate.Visible = true;
          llblUpdate.Text = $"Update to {Configs.Instance.UpdateHelper.UpdateAvailable.Version}";
          lblUpdateState.Visible = false;
          break;

        case UpdateHelper.UpdateCheckResult.NoUpdates:
          lblUpdateState.Text = "You are using the most recent version.";
          break;

        default:
          llblUpdate.Visible = true;
          llblUpdate.Text = "Update check failed. Check again?";
          lblUpdateState.Visible = false;
          break;
      }

      indUpdate.IndicatorEnabled = false;
    }

    private void AboutForm_Load(object sender, EventArgs e)
    {
      lblCopyright.Text = GetCopyrightString();
      DoUpdateCheck();
    }

    private void pbLogo_Click(object sender, EventArgs e)
    {
      OpenUrl("https://brokenevent.com");
    }
  }
}
