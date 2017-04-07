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
      this.btnBrowse = new System.Windows.Forms.Button();
      this.tbText = new System.Windows.Forms.TextBox();
      this.tbFilename = new System.Windows.Forms.TextBox();
      this.tbURL = new System.Windows.Forms.TextBox();
      this.rbText = new System.Windows.Forms.RadioButton();
      this.rbFile = new System.Windows.Forms.RadioButton();
      this.rbURL = new System.Windows.Forms.RadioButton();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.pbProgress = new System.Windows.Forms.ProgressBar();
      this.lblStatus = new System.Windows.Forms.Label();
      this.controlHighlight = new BrokenEvent.Shared.ControlHighlight(this.components);
      this.SuspendLayout();
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(383, 132);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 6;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      this.btnBrowse.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // tbText
      // 
      this.tbText.Enabled = false;
      this.tbText.Font = new System.Drawing.Font("Consolas", 8.25F);
      this.tbText.Location = new System.Drawing.Point(12, 183);
      this.tbText.Multiline = true;
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(446, 172);
      this.tbText.TabIndex = 5;
      this.tbText.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // tbFilename
      // 
      this.tbFilename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbFilename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbFilename.Location = new System.Drawing.Point(12, 133);
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
      // rbText
      // 
      this.rbText.Location = new System.Drawing.Point(12, 161);
      this.rbText.Name = "rbText";
      this.rbText.Size = new System.Drawing.Size(439, 17);
      this.rbText.TabIndex = 2;
      this.rbText.Text = "Text:";
      this.rbText.UseVisualStyleBackColor = true;
      this.rbText.Click += new System.EventHandler(this.RadioButton_Click);
      this.rbText.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // rbFile
      // 
      this.rbFile.Checked = true;
      this.rbFile.Location = new System.Drawing.Point(12, 110);
      this.rbFile.Name = "rbFile";
      this.rbFile.Size = new System.Drawing.Size(439, 17);
      this.rbFile.TabIndex = 1;
      this.rbFile.TabStop = true;
      this.rbFile.Text = "File:";
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
      this.rbURL.Text = "URL:";
      this.rbURL.UseVisualStyleBackColor = true;
      this.rbURL.Click += new System.EventHandler(this.RadioButton_Click);
      this.rbURL.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(302, 375);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
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
      this.btnCancel.Location = new System.Drawing.Point(383, 375);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // pbProgress
      // 
      this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pbProgress.Location = new System.Drawing.Point(12, 375);
      this.pbProgress.Name = "pbProgress";
      this.pbProgress.Size = new System.Drawing.Size(284, 10);
      this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.pbProgress.TabIndex = 7;
      this.pbProgress.Visible = false;
      // 
      // lblStatus
      // 
      this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblStatus.AutoSize = true;
      this.lblStatus.Location = new System.Drawing.Point(9, 388);
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
      this.controlHighlight.Font = new System.Drawing.Font("Segoe UI", 16F);
      this.controlHighlight.HighlightColor = System.Drawing.Color.DarkRed;
      this.controlHighlight.LineWidth = 2.5F;
      this.controlHighlight.Padding = new System.Windows.Forms.Padding(2);
      this.controlHighlight.TextColor = System.Drawing.Color.White;
      this.controlHighlight.TextPadding = new System.Windows.Forms.Padding(4);
      // 
      // StacktraceSourceForm
      // 
      this.AcceptButton = this.btnOk;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(470, 406);
      this.ControlBox = false;
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pbProgress);
      this.Controls.Add(this.tbText);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.rbText);
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
      this.Name = "StacktraceSourceForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stacktrace Source";
      this.Click += new System.EventHandler(this.StacktraceSource_Click);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.StacktraceSource_DragOver);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox tbText;
    private System.Windows.Forms.TextBox tbFilename;
    private System.Windows.Forms.TextBox tbURL;
    private System.Windows.Forms.RadioButton rbText;
    private System.Windows.Forms.RadioButton rbFile;
    private System.Windows.Forms.RadioButton rbURL;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.ProgressBar pbProgress;
    private System.Windows.Forms.Label lblStatus;
    private ControlHighlight controlHighlight;
  }
}