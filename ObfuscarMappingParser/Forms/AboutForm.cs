using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  partial class AboutForm : BaseForm
  {
    public AboutForm()
    {
      InitializeComponent();
      VersionLabel = lblAppVersion;
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    private void llblSupportEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start(string.Format("mailto:{0}", llblSupportEmail.Text));
      }
      catch (Win32Exception ex)
      {
        MessageBox.Show(this, "Failed to start e-mail client: " + ex.Message, "Failed to start e-mail client.");
      }
    }

    private void Hyperlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start(((LinkLabel)sender).Text);
      }
      catch (Win32Exception ex)
      {
        MessageBox.Show(this, "Failed to start web-browser: " + ex.Message, "Failed to start web-browser.");
      }
    }
  }
}
