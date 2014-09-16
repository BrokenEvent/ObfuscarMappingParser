using System;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  partial class LauncherForm : BaseForm
  {
    public LauncherForm()
    {
      InitializeComponent();
      chFilename.Width = lvFiles.ClientSize.Width;
      VersionLabel = lblAppVersion;

      foreach (string s in Configs.Instance.Recents)
        lvFiles.Items.Add(s, 0);

      lvFiles.Items[0].Selected = true;
    }

    private void lvFiles_Resize(object sender, EventArgs e)
    {
      chFilename.Width = lvFiles.ClientSize.Width;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      if (odFile.ShowDialog(this) != DialogResult.OK)
        return;

      lvFiles.Items.Add(odFile.FileName, 0).Selected = true;
    }

    private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnOk.Enabled = lvFiles.SelectedItems.Count > 0;
    }

    public string SelectedFilename
    {
      get { return lvFiles.SelectedItems.Count == 0 ? null : lvFiles.SelectedItems[0].Text; }
    }

    private void lvFiles_DoubleClick(object sender, EventArgs e)
    {
      if (lvFiles.SelectedItems.Count > 0)
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }
  }
}
