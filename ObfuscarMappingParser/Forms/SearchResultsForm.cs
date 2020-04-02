using System;
using System.Windows.Forms;

using BrokenEvent.Shared.Controls;

namespace ObfuscarMappingParser
{
  partial class SearchResultsForm : Form
  {
    private readonly MainForm mainForm;

    public SearchResultsForm(MainForm mainForm, SearchResults items, string name)
    {
      this.mainForm = mainForm;
      InitializeComponent();
      lvItems.SmallImageList = mainForm.IconsList;

      Text = name;
      foreach (INamedEntity item in items.Results)
      {
        string itemName = item.NameFull;

        ListViewItem lv = new ListViewItem(itemName);
        lv.ImageIndex = TreeBuilder.GetIconForEntity(item.EntityType, mainForm);
        lv.Tag = item;
        lvItems.Items.Add(lv);
        lv.ToolTipText = $"{mainForm.Mapping.FindNode((RenamedBase)item).ToolTipText} Double-click to select in classes tree.";
      }

      chItem.Width = lvItems.ClientSize.Width;
    }

    private void lvItems_DoubleClick(object sender, EventArgs e)
    {
      if (lvItems.SelectedItems.Count == 0)
        return;

      PineappleTreeNode node =  mainForm.Mapping.FindNode((RenamedBase)lvItems.SelectedItems[0].Tag);
      node.TreeView.SelectedNode = node;
      node.EnsureVisible();
    }

    private void lvItems_Resize(object sender, EventArgs e)
    {
      chItem.Width = lvItems.ClientSize.Width;
    }
  }
}
