namespace ObfuscarMappingParser
{
  partial class StacktraceSource
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
      this.gbSource = new System.Windows.Forms.GroupBox();
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
      this.gbSource.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbSource
      // 
      this.gbSource.Controls.Add(this.btnBrowse);
      this.gbSource.Controls.Add(this.tbText);
      this.gbSource.Controls.Add(this.tbFilename);
      this.gbSource.Controls.Add(this.tbURL);
      this.gbSource.Controls.Add(this.rbText);
      this.gbSource.Controls.Add(this.rbFile);
      this.gbSource.Controls.Add(this.rbURL);
      this.gbSource.Location = new System.Drawing.Point(12, 12);
      this.gbSource.Name = "gbSource";
      this.gbSource.Size = new System.Drawing.Size(379, 295);
      this.gbSource.TabIndex = 0;
      this.gbSource.TabStop = false;
      this.gbSource.Text = "Select stacktrace source";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(294, 103);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 21);
      this.btnBrowse.TabIndex = 6;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // tbText
      // 
      this.tbText.Enabled = false;
      this.tbText.Location = new System.Drawing.Point(6, 165);
      this.tbText.Multiline = true;
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(363, 124);
      this.tbText.TabIndex = 5;
      // 
      // tbFilename
      // 
      this.tbFilename.Location = new System.Drawing.Point(6, 103);
      this.tbFilename.Name = "tbFilename";
      this.tbFilename.Size = new System.Drawing.Size(282, 21);
      this.tbFilename.TabIndex = 4;
      // 
      // tbURL
      // 
      this.tbURL.Enabled = false;
      this.tbURL.Location = new System.Drawing.Point(6, 43);
      this.tbURL.Name = "tbURL";
      this.tbURL.Size = new System.Drawing.Size(363, 21);
      this.tbURL.TabIndex = 3;
      // 
      // rbText
      // 
      this.rbText.Location = new System.Drawing.Point(6, 142);
      this.rbText.Name = "rbText";
      this.rbText.Size = new System.Drawing.Size(363, 17);
      this.rbText.TabIndex = 2;
      this.rbText.Text = "Text";
      this.rbText.UseVisualStyleBackColor = true;
      this.rbText.Click += new System.EventHandler(this.RadioButton_Click);
      // 
      // rbFile
      // 
      this.rbFile.Checked = true;
      this.rbFile.Location = new System.Drawing.Point(6, 80);
      this.rbFile.Name = "rbFile";
      this.rbFile.Size = new System.Drawing.Size(363, 17);
      this.rbFile.TabIndex = 1;
      this.rbFile.TabStop = true;
      this.rbFile.Text = "File";
      this.rbFile.UseVisualStyleBackColor = true;
      this.rbFile.Click += new System.EventHandler(this.RadioButton_Click);
      // 
      // rbURL
      // 
      this.rbURL.Location = new System.Drawing.Point(6, 20);
      this.rbURL.Name = "rbURL";
      this.rbURL.Size = new System.Drawing.Size(363, 17);
      this.rbURL.TabIndex = 0;
      this.rbURL.Text = "URL";
      this.rbURL.UseVisualStyleBackColor = true;
      this.rbURL.Click += new System.EventHandler(this.RadioButton_Click);
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(397, 20);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(397, 49);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // StacktraceSource
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(478, 310);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.gbSource);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "StacktraceSource";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stacktrace source";
      this.gbSource.ResumeLayout(false);
      this.gbSource.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbSource;
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
  }
}