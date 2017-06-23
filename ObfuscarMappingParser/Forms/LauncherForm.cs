using System;
using System.Text;
using System.Windows.Forms;
using BrokenEvent.Shared;
using BrokenEvent.Shared.Algorithms;

namespace ObfuscarMappingParser
{
  partial class LauncherForm : BaseForm
  {
    private const int FILENAME_LENGTH = 70;

    public LauncherForm()
    {
      InitializeComponent();
      chFilename.Width = lvFiles.ClientSize.Width;

      foreach (string s in Configs.Instance.Recents)
      {
        ListViewItem lv = lvFiles.Items.Add(PathUtils.ShortenPath(s, FILENAME_LENGTH), 0);
        lv.Tag = s;

        StringBuilder sb = new StringBuilder();
        foreach (string pdb in Configs.Instance.GetRecentPdb(s))
          sb.AppendLine(pdb);

        if (sb.Length > 0)
          lv.ToolTipText = s + "\nAttached PDB:\n" + sb;
        else
          lv.ToolTipText = s;
      }

      if (lvFiles.Items.Count > 0)
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

      foreach (ListViewItem item in lvFiles.Items)
        if (string.Compare((string)item.Tag, odFile.FileName, StringComparison.OrdinalIgnoreCase) == 0)
        {
          item.Selected = true;
          return;
        }

      ListViewItem lv = lvFiles.Items.Add(PathUtils.ShortenPath(odFile.FileName, FILENAME_LENGTH), 0);
      lv.Tag = lv.ToolTipText = odFile.FileName;
      lv.Selected = true;
    }

    private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnOk.Enabled = lvFiles.SelectedItems.Count > 0;
    }

    public string SelectedFilename
    {
      get { return lvFiles.SelectedItems.Count == 0 ? null : (string)lvFiles.SelectedItems[0].Tag; }
    }

    private void lvFiles_DoubleClick(object sender, EventArgs e)
    {
      if (lvFiles.SelectedItems.Count > 0)
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void LauncherForm_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
    }

    private void LauncherForm_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;

      string s = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
      ListViewItem lv = lvFiles.Items.Add(PathUtils.ShortenPath(s, FILENAME_LENGTH), 0);
      lv.Tag = lv.ToolTipText = s;
      lv.Selected = true;
    }
  }
}
