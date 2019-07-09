using System;

using BrokenEvent.Shared.Forms;

namespace ObfuscarMappingParser
{
  partial class AboutForm : BaseAboutForm
  {
    public AboutForm()
    {
      InitializeComponent();
      VersionLabel = lblAppVersion;

      lblCopyright.Text = GetCopyrightString();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Close();
    }

    protected override string AssemblyVersionToString(Version version)
    {
      return version.ToString();
    }

    private void pbLogo_Click(object sender, EventArgs e)
    {
      OpenUrl("https://brokenevent.com");
    }
  }
}
