using System;
using System.Windows.Forms;
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

    private void llblSupportEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      OpenEmail(llblSupportEmail.Text);
    }

    private void Hyperlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      OpenUrl(((LinkLabel)sender).Text);
    }
  }
}
