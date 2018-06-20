using System;
using System.Drawing;
using System.Windows.Forms;

using BrokenEvent.Shared;
using BrokenEvent.Shared.Forms;
using BrokenEvent.Shared.CommandManager;
using BrokenEvent.Shared.Controls;
using BrokenEvent.Shared.Rest;
using BrokenEvent.VisualStudioOpener;

namespace ObfuscarMappingParser
{
  partial class SettingsForm : BaseForm
  {
    private readonly Mapping mapping;

    public SettingsForm(Mapping mapping, ICommandManager commandManager)
    {
      this.mapping = mapping;
      InitializeComponent();

      string vs = null;
      if (mapping != null)
        vs = Configs.Instance.GetRecentProperty(mapping.Filename, Configs.PROPERTY_EDITOR);
      else
        cbApplyVsToProject.Checked = cbApplyVsToProject.Enabled = false;

      if (vs == null)
        vs = Configs.Instance.Editor;
      IVisualStudioInfo selected = VisualStudioDetector.GetVisualStudioInfo(vs);
      if (selected == null)
        selected = VisualStudioDetector.GetHighestVisualStudio();

      foreach (IVisualStudioInfo info in VisualStudioDetector.GetVisualStudios())
      {
        BrokenListItem item = new BrokenListItem(info.Description);
        try
        {
          Icon icon = Icon.ExtractAssociatedIcon(info.Path);
          imageList.Images.Add(icon);
          item.ImageIndex = imageList.Images.Count - 1;
        }
        catch { }
        item.Tag = info;

        blvEditors.Items.Add(item);

        if (info == selected)
          item.Selected = true;
      }

      cbShowUnicode.Checked = Configs.Instance.ShowUnicode;
      cbSimplifySystemNames.Checked = Configs.Instance.SimplifySystemNames;
      cbSimplifyNullable.Checked = Configs.Instance.SimplifyNullable;
      cbSimplifyRef.Checked = Configs.Instance.SimplifyRef;
      cbGroupByNamespaces.Checked = Configs.Instance.GroupNamespaces;
      cbGroupByModules.Checked = Configs.Instance.GroupModules;
      cbUseColumns.Checked = Configs.Instance.UseColumns;
      cbWatchClipboard.Checked = Configs.Instance.WatchClipboard;

      EnumHelper.FillCombobox(cbDoubleClick, Configs.Instance.DoubleClickAction);
      EnumHelper.FillCombobox(cbUpdateInterval, Configs.Instance.UpdateHelper.Interval);

      commandSelector.CommandManager = commandManager;
      commandSelector.CommandType = typeof(Actions);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      string editor = blvEditors.SelectedItem.Tag.ToString();

      if (cbApplyVsToProject.Checked)
        Configs.Instance.AddRecentProperty(mapping.Filename, Configs.PROPERTY_EDITOR, editor);
      else
        Configs.Instance.Editor = editor;

      Configs.Instance.ShowUnicode = cbShowUnicode.Checked;
      Configs.Instance.SimplifySystemNames = cbSimplifySystemNames.Checked;
      Configs.Instance.SimplifyNullable = cbSimplifyNullable.Checked;
      Configs.Instance.SimplifyRef = cbSimplifyRef.Checked;
      Configs.Instance.GroupNamespaces = cbGroupByNamespaces.Checked;
      Configs.Instance.GroupModules = cbGroupByModules.Checked;
      Configs.Instance.UseColumns = cbUseColumns.Checked;
      Configs.Instance.WatchClipboard = cbWatchClipboard.Checked;
      Configs.Instance.DoubleClickAction = ((EnumHelper.EnumWrapper<Configs.DoubleClickActions>)cbDoubleClick.SelectedItem).Value;
      Configs.Instance.UpdateHelper.Interval = ((EnumHelper.EnumWrapper<UpdateHelper.CheckInterval>)cbUpdateInterval.SelectedItem).Value;

      DialogResult = DialogResult.OK;
    }
  }
}
