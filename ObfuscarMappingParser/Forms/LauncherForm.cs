using System;
using System.Text;
using System.Windows.Forms;
using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.Controls;

using ObfuscarMappingParser.Engine.Reader;
using ObfuscarMappingParser.Properties;

namespace ObfuscarMappingParser
{
  partial class LauncherForm : BaseForm
  {
    private void AddItem(string name)
    {
      BrokenListItem item = new BrokenListItem(name);
      item.Tag = name;
      item.ImageIndex = 0;
      item.ToolTipText = name;

      blvFiles.Items.Insert(0, item);
      blvFiles.SelectedItem = item;
    }

    public LauncherForm()
    {
      InitializeComponent();
      ilIcon.Images.Add(Resources.Document);

      blvFiles.BeginUpdate();
      foreach (string s in Configs.Instance.Recents)
      {
        BrokenListItem item = new BrokenListItem(s);
        item.Tag = s;
        item.ImageIndex = 0;

        StringBuilder sb = new StringBuilder();
        foreach (string pdb in Configs.Instance.GetRecentPdb(s))
          sb.AppendLine(pdb);

        if (sb.Length > 0)
          item.ToolTipText = s + "\nAttached PDB:\n" + sb;
        else
          item.ToolTipText = s;

        blvFiles.Items.Add(item);
      }

      blvFiles.EndUpdate();

      if (blvFiles.Items.Count > 0)
        blvFiles.SelectedIndex = 0;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      if (odFile.ShowDialog(this) != DialogResult.OK)
        return;

      foreach (BrokenListItem item in blvFiles.Items)
        if (string.Compare((string)item.Tag, odFile.FileName, StringComparison.OrdinalIgnoreCase) == 0)
        {
          item.Selected = true;
          return;
        }

      AddItem(odFile.FileName);
      blvFiles.SelectedIndex = 0;
    }

    private void blvFiles_ItemSelected(object sender, ItemSelectEventArgs e)
    {
      btnOk.Enabled = blvFiles.SelectedIndex != -1;
    }

    public string SelectedFilename
    {
      get { return blvFiles.SelectedIndex == -1 ? null : (string)blvFiles.SelectedItem.Tag; }
    }

    private void blvFiles_DoubleClick(object sender, EventArgs e)
    {
      if (blvFiles.SelectedIndex != -1)
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void LauncherForm_DragOver(object sender, DragEventArgs e)
    {
      IDataObject dataObject = e.Data;

      if (!dataObject.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      string[] files = (string[])dataObject.GetData(DataFormats.FileDrop);

      e.Effect = FormatFactory.IsOpenable(files[0]) ? DragDropEffects.Move : DragDropEffects.None;
    }

    private void LauncherForm_DragDrop(object sender, DragEventArgs e)
    {
      IDataObject dataObject = e.Data;

      if (!dataObject.GetDataPresent(DataFormats.FileDrop))
        return;

      string filename = ((string[])dataObject.GetData(DataFormats.FileDrop))[0];

      if (!FormatFactory.IsOpenable(filename))
        return;

      AddItem(filename);
    }
  }
}
