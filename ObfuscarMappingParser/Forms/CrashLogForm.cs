using System;
using System.IO;
using System.Windows.Forms;

using BrokenEvent.TaskDialogs;
using BrokenEvent.TaskDialogs.Dialogs;

namespace ObfuscarMappingParser
{
  partial class CrashLogForm : Form
  {
    private Mapping mapping;

    public CrashLogForm(Mapping mapping)
    {
      this.mapping = mapping;
      InitializeComponent();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
      try
      {
        tbValue.Text = mapping.ProcessCrashlogText(tbValue.Text, btnSkipPrefix.Checked);
      }
      catch (Exception ex)
      {
        TaskDialogHelper.ShowMessageBox(
            Handle,
            "Failed",
            "Unable to process crashlog.",
            "Reason: " + ex.Message,
            TaskDialogStandardIcon.Error
          );
      }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        try
        {
          tbValue.Text = File.ReadAllText(openFileDialog.FileName);
        }
        catch { }
    }

    private void CrashLogForm_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text)
             ? DragDropEffects.Move
             : DragDropEffects.None;
    }

    private void CrashLogForm_DragDrop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        try
        {
          tbValue.Text = File.ReadAllText(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
        }
        catch {}

      if (e.Data.GetDataPresent(DataFormats.StringFormat))
        tbValue.Text = (string)e.Data.GetData(DataFormats.Text);
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(tbValue.Text))
        Clipboard.SetText(tbValue.Text);
    }

    private void tbValue_TextChanged(object sender, EventArgs e)
    {
      btnCopy.Enabled = !string.IsNullOrWhiteSpace(tbValue.Text);
    }

    public string Value
    {
      get { return tbValue.Text; }
      set { tbValue.Text = value; }
    }

    public bool SkipPrefixes
    {
      get { return btnSkipPrefix.Checked; }
    }
  }
}
