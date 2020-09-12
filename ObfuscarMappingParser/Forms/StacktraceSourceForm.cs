using System;
using System.Drawing;
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
    private BaseStacktraceSource selectedSource;

    private ClipboardStacktraceSource clipboardStacktraceSource = new ClipboardStacktraceSource();
    private UrlStacktraceSource urlStacktraceSource = new UrlStacktraceSource();
    private FileStacktraceSource fileStacktraceSource = new FileStacktraceSource();

    public const string RECENT_URLS = "StacktraceURL";

    public StacktraceSourceForm(MappingViewModel mapping)
    {
      InitializeComponent();

      tbURL.SetCueText("Type URL to get stacktrace");

      foreach (string s in Configs.Instance.GetRecentAdditional(mapping.Mapping.Filename, RECENT_URLS))
        tbURL.AutoCompleteCustomSource.Add(s);

      SelectedSource = clipboardStacktraceSource;
    }

    public BaseStacktraceSource SelectedSource
    {
      get { return selectedSource; }
      private set
      {
        selectedSource = value;
        selectedSource.OnActivate();

        tbURL.Enabled = btnUrlGet.Enabled = value == urlStacktraceSource;
        lblFileName.Enabled = btnBrowse.Enabled = value == fileStacktraceSource;

        UpdatePreview();
      }
    }

    private void UpdatePreview()
    {
      if (selectedSource.Error == null)
      {
        lblPreview.ForeColor = Color.Black;
        lblPreview.Text = selectedSource.Content;
        btnOk.Enabled = true;
      }
      else
      {
        lblPreview.ForeColor = Color.DarkRed;
        lblPreview.Text = selectedSource.Error;
        btnOk.Enabled = false;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void StacktraceSource_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = selectedSource.CanDrop(e.Data) ? DragDropEffects.Move : DragDropEffects.None;
    }

    private async void StacktraceSource_DragDrop(object sender, DragEventArgs e)
    {
      await selectedSource.Drop(e.Data);

      if (selectedSource == urlStacktraceSource)
        tbURL.Text = urlStacktraceSource.Url;
      else if (selectedSource == fileStacktraceSource)
        lblFileName.Text = fileStacktraceSource.Filename;
      UpdatePreview();
    }

    private void StacktraceSourceForm_Activated(object sender, EventArgs e)
    {
      selectedSource.OnActivate();
      UpdatePreview();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      openFileDialog.FileName = fileStacktraceSource.Filename;
      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      fileStacktraceSource.Load(lblFileName.Text = openFileDialog.FileName);
      UpdatePreview();
    }

    private async void btnUrlGet_Click(object sender, EventArgs e)
    {
      lblPreview.Text = "Getting URL...";
      lblPreview.ForeColor = Color.Navy;
      await urlStacktraceSource.Get(tbURL.Text);
      UpdatePreview();
    }

    private void rbClipboard_CheckedChanged(object sender, EventArgs e)
    {
      if (rbClipboard.Checked)
        SelectedSource = clipboardStacktraceSource;
    }

    private void rbURL_CheckedChanged(object sender, EventArgs e)
    {
      if (rbURL.Checked)
        SelectedSource = urlStacktraceSource;
    }

    private void rbFile_CheckedChanged(object sender, EventArgs e)
    {
      if (rbFile.Checked)
        SelectedSource = fileStacktraceSource;
    }
  }

  internal abstract class BaseStacktraceSource
  {
    private string content;
    private string error;

    public abstract string Name { get; }

    public abstract bool CanDrop(IDataObject obj);

    public abstract Task Drop(IDataObject obj);

    public virtual void OnActivate()
    {
    }

    public virtual void Commit(string mappingFilename)
    {
      
    }

    public string Content
    {
      get { return content; }
      protected set
      {
        if (value == null)
          content = null;
        else if (string.IsNullOrWhiteSpace(value))
        {
          Error = "Content can't be empty";
          content = null;
        }
        else
        {
          error = null;
          content = value;
        }
      }
    }

    public string Error
    {
      get { return error; }
      protected set
      {
        error = value;
        content = null;
      }
    }
  }

  internal class ClipboardStacktraceSource: BaseStacktraceSource
  {
    public override string Name
    {
      get { return "Clipboard"; }
    }

    public override void OnActivate()
    {
      if (Clipboard.ContainsText())
      {
        Content = Clipboard.GetText();
        return;
      }

      Error = "clipboard doesn't contain text data";
    }

    public override bool CanDrop(IDataObject obj)
    {
      return obj.GetDataPresent(DataFormats.Text);
    }

    public override Task Drop(IDataObject obj)
    {
      Content = (string)obj.GetData(DataFormats.Text);
      return Task.FromResult(0);
    }
  }

  internal class FileStacktraceSource: BaseStacktraceSource
  {
    private string filename;

    public FileStacktraceSource()
    {
      Error = "<no file loaded>";
    }

    public override string Name
    {
      get { return $"File: {Path.GetFileName(filename)}"; }
    }

    public string Filename
    {
      get { return filename; }
    }

    public void Load(string filename)
    {
      this.filename = filename;
      try
      {
        Content = File.ReadAllText(filename);
      }
      catch (Exception e)
      {
        Error = e.Message;
      }
    }

    public override bool CanDrop(IDataObject obj)
    {
      return obj.GetDataPresent(DataFormats.FileDrop);
    }

    public override Task Drop(IDataObject obj)
    {
      Load(((string[])obj.GetData(DataFormats.FileDrop))[0]);
      return Task.FromResult(0);
    }
  }

  internal class UrlStacktraceSource: BaseStacktraceSource
  {
    private string url;

    public UrlStacktraceSource()
    {
      Error = "<no content downloaded>";
    }

    public override string Name
    {
      get { return null; }
    }

    public string Url
    {
      get { return url; }
    }

    public async Task Get(string url)
    {
      this.url = url;
      try
      {
        Content = await new WebClient().DownloadStringTaskAsync(url);
      }
      catch (Exception e)
      {
        Error = e.Message;
      }
    }

    public override void Commit(string mappingFilename)
    {
      Configs.Instance.AddRecentAdditional(mappingFilename, StacktraceSourceForm.RECENT_URLS, url);
    }

    public override bool CanDrop(IDataObject obj)
    {
      return obj.GetDataPresent(DataFormats.Text);
    }

    public override Task Drop(IDataObject obj)
    {
      return Get((string)obj.GetData(DataFormats.StringFormat));
    }
  }
}
