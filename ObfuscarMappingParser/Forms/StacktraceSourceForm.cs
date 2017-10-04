using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using BrokenEvent.Shared;
using BrokenEvent.Shared.WinApi;

namespace ObfuscarMappingParser
{
  internal partial class StacktraceSourceForm : BaseForm
  {
    private readonly Mapping mapping;
    private string result;
    private string resultSource;

    private const string RECENT_URLS = "StacktraceURL";
    private const string RECENT_FILES = "StacktraceFile";

    public StacktraceSourceForm(Mapping mapping)
    {
      this.mapping = mapping;
      InitializeComponent();

      tbFilename.SetCueText("Select file to read stacktrace");
      tbURL.SetCueText("Type URL to get stacktrace");

      controlHighlight.OwnerForm = this;

      foreach (string s in Configs.Instance.GetRecentAdditional(mapping.Filename, RECENT_URLS))
        tbURL.AutoCompleteCustomSource.Add(s);

      foreach (string s in Configs.Instance.GetRecentAdditional(mapping.Filename, RECENT_FILES))
        tbFilename.AutoCompleteCustomSource.Add(s);
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
      {
        controlHighlight.Show(tbURL, "This field cannot be empty.");
        return;
      }

      Thread thread = new Thread(UrlThreadStart);
      pbProgress.Visible = lblStatus.Visible = true;
      thread.Start(tbURL.Text);
      EnableRadioButtons(false);
      tbURL.Enabled = false;
    }

    private void EnableRadioButtons(bool enable)
    {
      rbFile.Enabled = rbURL.Enabled = rbText.Enabled = enable;
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
      Configs.Instance.AddRecentAdditional(mapping.Filename, RECENT_URLS, tbURL.Text);
      Close();
    }

    private void UrlThreadFailedInternal(Exception e)
    {
      controlHighlight.Show(tbURL, "Failed to get data from URL.");
      pbProgress.Visible = lblStatus.Visible = false;
      EnableRadioButtons(true);
      tbURL.Enabled = true;
    }

    private bool LoadText()
    {
      if (string.IsNullOrEmpty(tbText.Text))
      {
        controlHighlight.Show(tbText, "This field cannot be empty.");
        return false;
      }
      
      result = tbText.Text;
      resultSource = "Text";
      return true;
    }

    private bool LoadFile()
    {
      if (string.IsNullOrEmpty(tbFilename.Text))
      {
        controlHighlight.Show(tbFilename, "This field cannot be empty.");
        return false;
      }

      if (!File.Exists(tbFilename.Text))
      {
        controlHighlight.Show(tbFilename, "File not exists.");
        return false;
      }

      try
      {
        result = File.ReadAllText(tbFilename.Text);
        resultSource = tbFilename.Text;
      }
      catch (Exception)
      {
        controlHighlight.Show(tbFilename, "Unable to load file.");
        return false;
      }

      Configs.Instance.AddRecentAdditional(mapping.Filename, RECENT_FILES, tbFilename.Text);
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

    private void StacktraceSource_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text)
                   ? DragDropEffects.Move
                   : DragDropEffects.None;
    }

    private void StacktraceSource_DragDrop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        rbFile.Checked = true;
        tbFilename.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        RadioButton_Click(sender, EventArgs.Empty);
        return;
      }

      if (e.Data.GetDataPresent(DataFormats.StringFormat))
      {
        rbText.Checked = true;
        tbText.Text = (string)e.Data.GetData(DataFormats.Text);
        RadioButton_Click(sender, EventArgs.Empty);
        return;
      }
    }

    private void Control_Enter(object sender, EventArgs e)
    {
      controlHighlight.Hide();
    }

    private void StacktraceSource_Click(object sender, EventArgs e)
    {
      controlHighlight.Hide();
    }
  }
}
