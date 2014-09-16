using System;
using System.IO;
using System.Windows.Forms;

namespace ObfuscarMappingParser
{
  partial class CrashLog : Form
  {
    private Mapping mapping;

    public CrashLog(Mapping mapping)
    {
      this.mapping = mapping;
      InitializeComponent();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
      try
      {
        tbValue.Text = mapping.ProcessCrashlogText(tbValue.Text);
      }
      catch (Exception ex)
      {
        throw new ObfuscarParserException("Failed to process crashlog:\n" + tbValue.Text, ex);
      }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        tbValue.Text = File.ReadAllText(openFileDialog.FileName);
    }
  }
}
