using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  public partial class StacktraceSource : Form
  {
    private string result;
    private string resultSource;

    public StacktraceSource()
    {
      InitializeComponent();
      tbFilename.SetCueText("Select file to read stacktrace");
      tbURL.SetCueText("Type URL to get stacktrace");
    }

    private void RadioButton_Click(object sender, EventArgs e)
    {
      tbURL.Enabled = rbURL.Checked;
      tbFilename.Enabled = btnBrowse.Enabled = rbFile.Checked;
      tbText.Enabled = rbText.Checked;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (rbText.Checked && !LoadText())
        return;
      if (rbFile.Checked && !LoadFile())
        return;

      if (rbURL.Checked)
      {
        LoadURL();
        return;
      }

      DialogResult = DialogResult.OK;
      Close();
    }

    private void LoadURL()
    {
      if (string.IsNullOrEmpty(tbURL.Text))
        return;

      Thread thread = new Thread(UrlThreadStart);
      btnOk.Enabled = btnCancel.Enabled = gbSource.Enabled = false;
      Text = "Getting URL...";
      thread.Start(tbURL.Text);
    }

    private void UrlThreadStart(object p)
    {
      string url = (string)p;
      try
      {
        UrlThreadComplete(new WebClient().DownloadString(url));
      }
      catch (Exception e)
      {
        UrlThreadFailed(e);
      }
    }

    private void UrlThreadComplete(string s)
    {
      Invoke(new Action<string>(UrlThreadCompleteInternal), s);
    }

    private void UrlThreadFailed(Exception e)
    {
      Invoke(new Action<Exception>(UrlThreadFailedInternal), e);
    }

    private void UrlThreadCompleteInternal(string s)
    {
      result = s;
      resultSource = tbURL.Text;
      DialogResult = DialogResult.OK;
      Close();
    }

    private void UrlThreadFailedInternal(Exception e)
    {
      MessageBox.Show(this, "Failed to get data from URL: " + e.Message, "Failed to load URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
      Text = "Stacktrace source";
      btnOk.Enabled = btnCancel.Enabled = gbSource.Enabled = true;
    }

    private bool LoadText()
    {
      if (string.IsNullOrEmpty(tbText.Text))
        return false;

      result = tbText.Text;
      resultSource = "Text";
      return true;
    }

    private bool LoadFile()
    {
      if (string.IsNullOrEmpty(tbFilename.Text))
        return false;

      if (!File.Exists(tbFilename.Text))
      {
        MessageBox.Show(this, "File\n" + tbFilename.Text + "\nNot exists.", "Unable to load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      try
      {
        result = File.ReadAllText(tbFilename.Text);
        resultSource = tbFilename.Text;
      }
      catch (Exception e)
      {
        MessageBox.Show(this, "Unable to load\n" + tbFilename.Text + "\n" + e.Message, "Unable to load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      return true;
    }

    public string Result
    {
      get { return result; }
    }

    public string ResultSource
    {
      get { return resultSource; }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      openFileDialog.FileName = tbFilename.Text;
      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      tbFilename.Text = openFileDialog.FileName;
    }
  }
}
