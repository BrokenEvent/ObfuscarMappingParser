using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrokenEvent.Shared.Forms;
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
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
      if (rbClipboard.Checked)
      {
        if (!Clipboard.ContainsText())
          return;

        result = Clipboard.GetText();
        resultSource = "Clipboard";
      }

      if (rbFile.Checked && !LoadFile())
        return;

      if (rbURL.Checked && !await LoadURL())
        return;

      DialogResult = DialogResult.OK;
      Close();
    }

    private async Task<bool> LoadURL()
    {
      if (string.IsNullOrEmpty(tbURL.Text))
      {
        controlHighlight.Show(tbURL, "This field cannot be empty.");
        return false;
      }

      Enabled = false;
      lblStatus.Visible = true;
      try
      {
        result = await new WebClient().DownloadStringTaskAsync(tbURL.Text);
        resultSource = "URL";
        Configs.Instance.AddRecentAdditional(mapping.Filename, RECENT_URLS, tbURL.Text);
        return true;
      }
      catch (Exception e)
      {
        controlHighlight.Show(tbURL, e.Message);
        return false;
      }
      finally
      {
        Enabled = true;
      }
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
        Uri uri;
        if (!Uri.TryCreate((string)e.Data.GetData(DataFormats.Text), UriKind.Absolute, out uri))
          return;

        rbURL.Checked = true;
        tbURL.Text = uri.ToString();
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

    private void StacktraceSourceForm_Activated(object sender, EventArgs e)
    {
      rbClipboard.Enabled = Clipboard.ContainsText();
      if (rbClipboard.Enabled)
        lblClipboardPreview.Text = Clipboard.GetText();
      else
        lblClipboardPreview.Text = "<no text in clipboard>";
    }
  }
}
