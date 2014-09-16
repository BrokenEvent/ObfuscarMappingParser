using System;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  public partial class SettingsForm : Form
  {
    public SettingsForm()
    {
      InitializeComponent();

      lbVS.Items.Add(VSOpener.VisualStudioVersion.Notepad);
      if (VSOpener.VisualStudioVersions != null)
        foreach (VSOpener.VisualStudioVersion version in VSOpener.VisualStudioVersions)
          lbVS.Items.Add(version);

      lbVS.SelectedItem = Configs.Instance.VisualStudioVersion;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Configs.Instance.VisualStudioVersion = (VSOpener.VisualStudioVersion)lbVS.SelectedItem;

      DialogResult = DialogResult.OK;
    }
  }
}
