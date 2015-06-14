using System;
using System.Drawing;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  partial class SettingsForm : BaseForm
  {
    private readonly Mapping mapping;

    public SettingsForm(Mapping mapping)
    {
      this.mapping = mapping;
      InitializeComponent();

      lbVS.Items.Add(VSOpener.VisualStudioVersion.Notepad);
      if (VSOpener.VisualStudioVersions != null)
        foreach (VSOpener.VisualStudioVersion version in VSOpener.VisualStudioVersions)
          lbVS.Items.Add(version);

      cbShowUnicode.Checked = Configs.Instance.ShowUnicode;
      cbSimplifySystemNames.Checked = Configs.Instance.SimplifySystemNames;
      cbSimplifyNullable.Checked = Configs.Instance.SimplifyNullable;
      cbGroupByNamespaces.Checked = Configs.Instance.GroupNamespaces;
      cbGroupByModules.Checked = Configs.Instance.GroupModules;
      cbUseColumns.Checked = Configs.Instance.UseColumns;

      string vs = Configs.Instance.GetRecentProperty(mapping.Filename, "editor");
      if (vs == null)
        lbVS.SelectedItem = Configs.Instance.VisualStudioVersion;
      else
      {
        lbVS.SelectedItem = Enum.Parse(typeof(VSOpener.VisualStudioVersion), vs);
        cbApplyVsToProject.Checked = true;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (cbApplyVsToProject.Checked)
        Configs.Instance.AddRecentProperty(mapping.Filename, "editor", lbVS.SelectedItem.ToString());
      else
        Configs.Instance.VisualStudioVersion = (VSOpener.VisualStudioVersion)lbVS.SelectedItem;

      Configs.Instance.ShowUnicode = cbShowUnicode.Checked;
      Configs.Instance.SimplifySystemNames = cbSimplifySystemNames.Checked;
      Configs.Instance.SimplifyNullable = cbSimplifyNullable.Checked;
      Configs.Instance.GroupNamespaces = cbGroupByNamespaces.Checked;
      Configs.Instance.GroupModules = cbGroupByModules.Checked;
      Configs.Instance.UseColumns = cbUseColumns.Checked;

      DialogResult = DialogResult.OK;
    }

    private void SettingsForm_Paint(object sender, PaintEventArgs e)
    {
      const int DIVIDER_Y = 230;

      using (Pen pen = new Pen(LineColor))
        e.Graphics.DrawLine(pen, 0, DIVIDER_Y, ClientSize.Width, DIVIDER_Y);
    }
  }
}
