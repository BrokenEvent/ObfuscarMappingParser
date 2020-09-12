using BrokenEvent.Shared;

namespace ObfuscarMappingParser
{
  partial class StacktraceSourceForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnBrowse = new System.Windows.Forms.Button();
      this.tbURL = new System.Windows.Forms.TextBox();
      this.rbClipboard = new System.Windows.Forms.RadioButton();
      this.rbFile = new System.Windows.Forms.RadioButton();
      this.rbURL = new System.Windows.Forms.RadioButton();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnUrlGet = new System.Windows.Forms.Button();
      this.lblPreview = new System.Windows.Forms.Label();
      this.lblFileName = new System.Windows.Forms.Label();
      this.tlblSource = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.tlblPreview = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.SuspendLayout();
      // 
      // btnBrowse
      // 
      this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowse.Location = new System.Drawing.Point(471, 159);
      this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 5;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // tbURL
      // 
      this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbURL.Enabled = false;
      this.tbURL.Location = new System.Drawing.Point(161, 131);
      this.tbURL.Margin = new System.Windows.Forms.Padding(4);
      this.tbURL.Name = "tbURL";
      this.tbURL.Size = new System.Drawing.Size(303, 22);
      this.tbURL.TabIndex = 1;
      // 
      // rbClipboard
      // 
      this.rbClipboard.Checked = true;
      this.rbClipboard.Location = new System.Drawing.Point(13, 99);
      this.rbClipboard.Margin = new System.Windows.Forms.Padding(8, 12, 4, 4);
      this.rbClipboard.Name = "rbClipboard";
      this.rbClipboard.Size = new System.Drawing.Size(140, 17);
      this.rbClipboard.TabIndex = 6;
      this.rbClipboard.TabStop = true;
      this.rbClipboard.Text = "Paste from clipboard";
      this.rbClipboard.UseVisualStyleBackColor = true;
      this.rbClipboard.CheckedChanged += new System.EventHandler(this.rbClipboard_CheckedChanged);
      // 
      // rbFile
      // 
      this.rbFile.Location = new System.Drawing.Point(13, 165);
      this.rbFile.Margin = new System.Windows.Forms.Padding(8, 12, 4, 4);
      this.rbFile.Name = "rbFile";
      this.rbFile.Size = new System.Drawing.Size(140, 17);
      this.rbFile.TabIndex = 3;
      this.rbFile.Text = "Load from file:";
      this.rbFile.UseVisualStyleBackColor = true;
      this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
      // 
      // rbURL
      // 
      this.rbURL.Location = new System.Drawing.Point(13, 132);
      this.rbURL.Margin = new System.Windows.Forms.Padding(8, 12, 4, 4);
      this.rbURL.Name = "rbURL";
      this.rbURL.Size = new System.Drawing.Size(140, 17);
      this.rbURL.TabIndex = 0;
      this.rbURL.Text = "Download from URL:";
      this.rbURL.UseVisualStyleBackColor = true;
      this.rbURL.CheckedChanged += new System.EventHandler(this.rbURL_CheckedChanged);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(342, 410);
      this.btnOk.Margin = new System.Windows.Forms.Padding(4);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 7;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(448, 410);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // btnUrlGet
      // 
      this.btnUrlGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUrlGet.Location = new System.Drawing.Point(471, 130);
      this.btnUrlGet.Margin = new System.Windows.Forms.Padding(4);
      this.btnUrlGet.Name = "btnUrlGet";
      this.btnUrlGet.Size = new System.Drawing.Size(75, 23);
      this.btnUrlGet.TabIndex = 10;
      this.btnUrlGet.Text = "Get";
      this.btnUrlGet.UseVisualStyleBackColor = true;
      this.btnUrlGet.Click += new System.EventHandler(this.btnUrlGet_Click);
      // 
      // lblPreview
      // 
      this.lblPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblPreview.AutoEllipsis = true;
      this.lblPreview.Location = new System.Drawing.Point(12, 215);
      this.lblPreview.Name = "lblPreview";
      this.lblPreview.Size = new System.Drawing.Size(535, 181);
      this.lblPreview.TabIndex = 11;
      this.lblPreview.Text = "label1";
      this.lblPreview.UseMnemonic = false;
      // 
      // lblFileName
      // 
      this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblFileName.AutoEllipsis = true;
      this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
      this.lblFileName.Location = new System.Drawing.Point(160, 167);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(304, 13);
      this.lblFileName.TabIndex = 12;
      this.lblFileName.Text = "<no file selected>";
      this.lblFileName.UseMnemonic = false;
      // 
      // tlblSource
      // 
      this.tlblSource.AutoSizeLabel = true;
      this.tlblSource.BackColor = System.Drawing.Color.Transparent;
      this.tlblSource.CharacterWrap = false;
      this.tlblSource.Location = new System.Drawing.Point(13, 62);
      this.tlblSource.Margin = new System.Windows.Forms.Padding(4);
      this.tlblSource.Name = "tlblSource";
      this.tlblSource.Size = new System.Drawing.Size(48, 21);
      this.tlblSource.Style = BrokenEvent.Shared.Controls.ThemedLabel.ThemeStyle.ControlPanelSubtitle;
      this.tlblSource.TabIndex = 13;
      this.tlblSource.Text = "Source";
      // 
      // tlblPreview
      // 
      this.tlblPreview.AutoSizeLabel = true;
      this.tlblPreview.BackColor = System.Drawing.Color.Transparent;
      this.tlblPreview.CharacterWrap = false;
      this.tlblPreview.Location = new System.Drawing.Point(13, 190);
      this.tlblPreview.Margin = new System.Windows.Forms.Padding(4);
      this.tlblPreview.Name = "tlblPreview";
      this.tlblPreview.Size = new System.Drawing.Size(58, 21);
      this.tlblPreview.Style = BrokenEvent.Shared.Controls.ThemedLabel.ThemeStyle.ControlPanelSubtitle;
      this.tlblPreview.TabIndex = 14;
      this.tlblPreview.Text = "Preview:";
      // 
      // StacktraceSourceForm
      // 
      this.AcceptButton = this.btnOk;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(560, 441);
      this.ControlBox = false;
      this.Controls.Add(this.tlblPreview);
      this.Controls.Add(this.tlblSource);
      this.Controls.Add(this.lblFileName);
      this.Controls.Add(this.lblPreview);
      this.Controls.Add(this.btnUrlGet);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.rbClipboard);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.tbURL);
      this.Controls.Add(this.rbFile);
      this.Controls.Add(this.rbURL);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "Select Stacktrace Source";
      this.Name = "StacktraceSourceForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stacktrace Source";
      this.Activated += new System.EventHandler(this.StacktraceSourceForm_Activated);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragOver);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox tbURL;
    private System.Windows.Forms.RadioButton rbClipboard;
    private System.Windows.Forms.RadioButton rbFile;
    private System.Windows.Forms.RadioButton rbURL;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.Button btnUrlGet;
    private System.Windows.Forms.Label lblPreview;
    private System.Windows.Forms.Label lblFileName;
    private BrokenEvent.Shared.Controls.ThemedLabel tlblSource;
    private BrokenEvent.Shared.Controls.ThemedLabel tlblPreview;
  }
}