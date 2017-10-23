using System;
using System.Windows.Forms;
using BrokenEvent.Shared;
using BrokenEvent.Shared.Controls;
using BrokenEvent.Shared.WinApi;

namespace ObfuscarMappingParser
{
  partial class SearchDialog : BaseForm
  {
    private readonly MainForm mainForm;
    private readonly bool searchOriginal;

    private class ItemDescriptor
    {
      private RenamedBase item;
      private string filename;
      private int line;

      public ItemDescriptor(RenamedBase item)
      {
        this.item = item;
      }

      public RenamedBase Item
      {
        get { return item; }
      }

      public int Line
      {
        get { return line; }
        set { line = value; }
      }

      public string Filename
      {
        get { return filename; }
        set { filename = value; }
      }
    }

    public SearchDialog(MainForm mainForm, bool searchOriginal)
    {
      this.mainForm = mainForm;
      this.searchOriginal = searchOriginal;
      InitializeComponent();
      lvResults_Resize(null, EventArgs.Empty);
      lvResults.SmallImageList = mainForm.IconsList;

      tbSearch.SetCueText("Type name of the obfuscated element");

      if (searchOriginal)
      {
        Text = "Search for Original Name";
        HeaderText = "Search for Original Name";
      }

      tbSearch.AutoCompleteCustomSource = searchOriginal ?
        mainForm.Mapping.GetOldNamesCollection() :
        mainForm.Mapping.GetNewNamesCollection();

      controlHighlight.OwnerForm = this;
    }

    private void lvResults_Resize(object sender, EventArgs e)
    {
      columnHeader.Width = lvResults.ClientSize.Width;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbSearch.Text))
      {
        controlHighlight.Show(tbSearch, "This field cannot be empty");
        return;
      }

      Search();
    }

    private void Search()
    {
      lvResults.Items.Clear();

      SearchResults results = searchOriginal ? mainForm.Mapping.SearchOriginal(tbSearch.Text) : mainForm.Mapping.Search(tbSearch.Text, false, false);
      if (results == null || !results.HasValue)
        return;

      if (results.IsSingleResult)
        BuildResult((RenamedBase)results.SingleResult);
      else
        foreach (RenamedBase result in results.Results)
          BuildResult(result);

      tbSearch.Clear();

      lvResults.Focus();
    }

    private void BuildResult(RenamedBase item)
    {
      ListViewItem l = new ListViewItem(item.NameOldSimple);
      l.ImageIndex = TreeBuilder.GetIconForEntity(item.EntityType, mainForm);
      lvResults.Items.Add(l);
      ItemDescriptor d = new ItemDescriptor(item);
      l.Tag = d;
      string tip;
      if (item is RenamedClass)
        tip = TreeBuilder.BuildHintForClass((RenamedClass)item);
      else
        tip = TreeBuilder.BuildHintForItem((RenamedItem)item);

      if (!mainForm.HavePdb)
        tip += "Unable to map to source code, no PDB files attached.";
      else
      {
        if (item.EntityType != EntityType.Method)
          tip += "Mapping to source code works only for methods.";
        else
        {
          string filename;
          int line;
          if (mainForm.SearchInPdb(out filename, out line, item))
          {
            tip += "Source position:\n" + filename + ":" + line + " (Ctrl+Click to open)";
            d.Filename = filename;
            d.Line = line;
          }
          else
            tip += "Unable to map to source code.";
        }
      }

      tip += "\nDouble-click to select in classes tree.";

      l.ToolTipText = tip;
    }

    private void lvResults_DoubleClick(object sender, EventArgs e)
    {
      if (lvResults.SelectedItems.Count == 0)
        return;

      PineappleTreeNode node = ((ItemDescriptor)lvResults.SelectedItems[0].Tag).Item.TreeNode;
      node.TreeView.SelectedNode = node;
      node.EnsureVisible();
      Close();
    }

    private void lvResults_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || ModifierKeys != Keys.Control)
        return;

      if (lvResults.SelectedItems.Count == 0)
        return;

      ItemDescriptor d = (ItemDescriptor)lvResults.SelectedItems[0].Tag;
      if (d.Filename == null)
        return;

      mainForm.OpenInVisualStudio(d.Filename, d.Line);
    }

    private void tbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
        return;

      Search();
    }

    private void Control_Enter(object sender, EventArgs e)
    {
      controlHighlight.Hide();
    }

    private void SearchDialog_Click(object sender, EventArgs e)
    {
      controlHighlight.Hide();
    }
  }
}
