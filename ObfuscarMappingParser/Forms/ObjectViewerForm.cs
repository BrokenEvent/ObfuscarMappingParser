using BrokenEvent.Shared.Forms;

namespace ObfuscarMappingParser
{
  public partial class ObjectViewerForm : BaseForm
  {
    public ObjectViewerForm(object value)
    {
      InitializeComponent();
      lblToString.Text = value.ToString();
    }

    private void btnClose_Click(object sender, System.EventArgs e)
    {
      Close();
    }
  }
}
