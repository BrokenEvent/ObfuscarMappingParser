using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

using BrokenEvent.Shared;
using BrokenEvent.Shared.Algorithms;

namespace ObfuscarMappingParser
{
  public partial class ConvertSettingsForm : BaseForm
  {
    public ConvertSettingsForm()
    {
      InitializeComponent();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      lblPath.Tag = lblPath.Text = openFileDialog.FileName;
    }

    private void rbSourceFile_CheckedChanged(object sender, EventArgs e)
    {
      btnBrowse.Enabled = lblPath.Enabled = rbSourceFile.Checked;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      string stringData = GetStringData();
      if (stringData == null)
        return;

      byte[] bytes = GetBinaryData(stringData);
      if (bytes == null)
        return;

      Form form = null;
      if (!Convert(bytes, ref form))
        return;

      form.ShowDialog(this);
      DialogResult = DialogResult.OK;
      Close();
    }

    private void ShowError(string value)
    {
      MessageBox.Show(this, value, "Unable to Process", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    private string GetStringData()
    {
      if (rbSourceFile.Checked)
      {
        if (lblPath.Tag == null)
        {
          ShowError("No input file selected");
          return null;
        }

        return File.ReadAllText((string)lblPath.Tag);
      }

      if (rbSourceText.Checked)
      {
        if (!Clipboard.ContainsText())
        {
          ShowError("Clipboard doesn't contains text");
          return null;
        }

        return Clipboard.GetText();
      }

      ShowError("No source selected");
      return null;
    }

    private byte[] GetBinaryData(string value)
    {
      if (rbBase64.Checked)
        try
        {
          return System.Convert.FromBase64String(value);
        }
        catch (Exception e)
        {
          ShowError("Invalid Base64 value:\n" + e.Message);
          return null;
        }

      if (rbHEX.Checked)
        try
        {
          return DigitUtils.StringToByteArray(value);
        }
        catch (Exception e)
        {
          ShowError("Invalid Base64 value:\n" + e.Message);
          return null;
        }

      ShowError("No conversion selected");
      return null;
    }

    private bool Convert(byte[] data, ref Form form)
    {
      if (rbTargetUTF8.Checked)
      {
        string result;
        try
        {
          result = Encoding.UTF8.GetString(data);
        }
        catch (Exception e)
        {
          ShowError("Failed to convert as UTF-8 string:\n" + e.Message);
          return false;
        }

        form = new TextViewerForm(result);
        return true;
      }

      if (rbTargetUTF16.Checked)
      {
        string result;
        try
        {
          result = Encoding.Unicode.GetString(data);
        }
        catch (Exception e)
        {
          ShowError("Failed to convert as UTF-16LE string:\n" + e.Message);
          return false;
        }

        form = new TextViewerForm(result);
        return true;
      }

      if (rbTargetFile.Checked)
      {
        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
          return false;

        try
        {
          File.WriteAllBytes(saveFileDialog.FileName, data);
        }
        catch (Exception e)
        {
          ShowError("Failed to write file:\n" + e.Message);
          return false;
        }

        return true;
      }

      if (rbTargetObject.Checked)
      {
        object result;
        try
        {
          BinaryFormatter formatter = new BinaryFormatter();
          using (MemoryStream stream = new MemoryStream(data))
            result = formatter.Deserialize(stream);
        }
        catch (Exception e)
        {
          ShowError("Failed to deserailze:\n" + e.Message);
          return false;
        }

        form = new ObjectViewerForm(result);
        return true;
      }

      ShowError("No target selected");
      return false;
    }
  }
}
