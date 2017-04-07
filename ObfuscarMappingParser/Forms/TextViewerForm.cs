using System;

using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  public partial class TextViewerForm : BaseForm
  {
    public TextViewerForm(string text)
    {
      InitializeComponent();
      tbText.Text = text;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
