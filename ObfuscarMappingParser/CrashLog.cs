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
        MessageBox.Show(this, "Unable to process crashlog. Reason: " + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        tbValue.Text = File.ReadAllText(openFileDialog.FileName);
    }
  }
}
