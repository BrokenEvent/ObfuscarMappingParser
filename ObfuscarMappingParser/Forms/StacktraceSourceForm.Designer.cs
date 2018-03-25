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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StacktraceSourceForm));
      this.btnBrowse = new System.Windows.Forms.Button();
      this.tbFilename = new System.Windows.Forms.TextBox();
      this.tbURL = new System.Windows.Forms.TextBox();
      this.rbClipboard = new System.Windows.Forms.RadioButton();
      this.rbFile = new System.Windows.Forms.RadioButton();
      this.rbURL = new System.Windows.Forms.RadioButton();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.lblStatus = new System.Windows.Forms.Label();
      this.controlHighlight = new BrokenEvent.Shared.Controls.ControlHighlight(this.components);
      this.lblClipboardPreview = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(383, 144);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 6;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      this.btnBrowse.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // tbFilename
      // 
      this.tbFilename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbFilename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbFilename.Location = new System.Drawing.Point(12, 145);
      this.tbFilename.Name = "tbFilename";
      this.tbFilename.Size = new System.Drawing.Size(365, 22);
      this.tbFilename.TabIndex = 4;
      this.tbFilename.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // tbURL
      // 
      this.tbURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbURL.Enabled = false;
      this.tbURL.Location = new System.Drawing.Point(12, 81);
      this.tbURL.Name = "tbURL";
      this.tbURL.Size = new System.Drawing.Size(446, 22);
      this.tbURL.TabIndex = 3;
      this.tbURL.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // rbClipboard
      // 
      this.rbClipboard.Location = new System.Drawing.Point(12, 186);
      this.rbClipboard.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
      this.rbClipboard.Name = "rbClipboard";
      this.rbClipboard.Size = new System.Drawing.Size(439, 17);
      this.rbClipboard.TabIndex = 2;
      this.rbClipboard.Text = "Get from clipboard";
      this.rbClipboard.UseVisualStyleBackColor = true;
      this.rbClipboard.Click += new System.EventHandler(this.RadioButton_Click);
      this.rbClipboard.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // rbFile
      // 
      this.rbFile.Checked = true;
      this.rbFile.Location = new System.Drawing.Point(12, 122);
      this.rbFile.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
      this.rbFile.Name = "rbFile";
      this.rbFile.Size = new System.Drawing.Size(439, 17);
      this.rbFile.TabIndex = 1;
      this.rbFile.TabStop = true;
      this.rbFile.Text = "Load from file:";
      this.rbFile.UseVisualStyleBackColor = true;
      this.rbFile.Click += new System.EventHandler(this.RadioButton_Click);
      this.rbFile.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // rbURL
      // 
      this.rbURL.Location = new System.Drawing.Point(12, 58);
      this.rbURL.Name = "rbURL";
      this.rbURL.Size = new System.Drawing.Size(446, 17);
      this.rbURL.TabIndex = 0;
      this.rbURL.Text = "Download from URL:";
      this.rbURL.UseVisualStyleBackColor = true;
      this.rbURL.Click += new System.EventHandler(this.RadioButton_Click);
      this.rbURL.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(252, 236);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      this.btnOk.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(358, 236);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Location = new System.Drawing.Point(147, 60);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(78, 13);
      this.lblStatus.TabIndex = 8;
      this.lblStatus.Text = "Getting URL...";
      this.lblStatus.Visible = false;
      // 
      // controlHighlight
      // 
      this.controlHighlight.BackgroundAlpha = ((byte)(200));
      this.controlHighlight.BackgroundColor = System.Drawing.Color.Red;
      this.controlHighlight.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.controlHighlight.HighlightColor = System.Drawing.Color.DarkRed;
      this.controlHighlight.LineWidth = 2.5F;
      this.controlHighlight.Padding = new System.Windows.Forms.Padding(2);
      this.controlHighlight.TextColor = System.Drawing.Color.White;
      this.controlHighlight.TextPadding = new System.Windows.Forms.Padding(4);
      // 
      // lblClipboardPreview
      // 
      this.lblClipboardPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblClipboardPreview.Enabled = false;
      this.lblClipboardPreview.Location = new System.Drawing.Point(12, 206);
      this.lblClipboardPreview.Name = "lblClipboardPreview";
      this.lblClipboardPreview.Size = new System.Drawing.Size(446, 13);
      this.lblClipboardPreview.TabIndex = 9;
      this.lblClipboardPreview.Text = "label1";
      // 
      // StacktraceSourceForm
      // 
      this.AcceptButton = this.btnOk;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(470, 267);
      this.ControlBox = false;
      this.Controls.Add(this.lblClipboardPreview);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.rbClipboard);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.tbFilename);
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
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "StacktraceSourceForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stacktrace Source";
      this.Activated += new System.EventHandler(this.StacktraceSourceForm_Activated);
      this.Click += new System.EventHandler(this.StacktraceSource_Click);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragOver);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox tbFilename;
    private System.Windows.Forms.TextBox tbURL;
    private System.Windows.Forms.RadioButton rbClipboard;
    private System.Windows.Forms.RadioButton rbFile;
    private System.Windows.Forms.RadioButton rbURL;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.Label lblStatus;
    private BrokenEvent.Shared.Controls.ControlHighlight controlHighlight;
    private System.Windows.Forms.Label lblClipboardPreview;
  }
}