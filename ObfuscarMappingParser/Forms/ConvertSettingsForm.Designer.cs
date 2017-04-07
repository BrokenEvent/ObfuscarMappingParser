namespace ObfuscarMappingParser
{
  partial class ConvertSettingsForm
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
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.gbSource = new System.Windows.Forms.GroupBox();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.lblPath = new System.Windows.Forms.Label();
      this.rbSourceText = new System.Windows.Forms.RadioButton();
      this.rbSourceFile = new System.Windows.Forms.RadioButton();
      this.gbTarget = new System.Windows.Forms.GroupBox();
      this.rbTargetUTF16 = new System.Windows.Forms.RadioButton();
      this.rbTargetFile = new System.Windows.Forms.RadioButton();
      this.rbTargetObject = new System.Windows.Forms.RadioButton();
      this.rbTargetUTF8 = new System.Windows.Forms.RadioButton();
      this.gbConversion = new System.Windows.Forms.GroupBox();
      this.rbHEX = new System.Windows.Forms.RadioButton();
      this.rbBase64 = new System.Windows.Forms.RadioButton();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.gbSource.SuspendLayout();
      this.gbTarget.SuspendLayout();
      this.gbConversion.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(380, 300);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 9;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(461, 300);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // gbSource
      // 
      this.gbSource.Controls.Add(this.btnBrowse);
      this.gbSource.Controls.Add(this.lblPath);
      this.gbSource.Controls.Add(this.rbSourceText);
      this.gbSource.Controls.Add(this.rbSourceFile);
      this.gbSource.Location = new System.Drawing.Point(12, 62);
      this.gbSource.Name = "gbSource";
      this.gbSource.Size = new System.Drawing.Size(524, 98);
      this.gbSource.TabIndex = 0;
      this.gbSource.TabStop = false;
      this.gbSource.Text = "Source";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(443, 31);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 1;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // lblPath
      // 
      this.lblPath.AutoSize = true;
      this.lblPath.Location = new System.Drawing.Point(16, 41);
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size(85, 13);
      this.lblPath.TabIndex = 2;
      this.lblPath.Text = "no file selected";
      // 
      // rbSourceText
      // 
      this.rbSourceText.AutoSize = true;
      this.rbSourceText.Location = new System.Drawing.Point(19, 70);
      this.rbSourceText.Margin = new System.Windows.Forms.Padding(16, 16, 3, 3);
      this.rbSourceText.Name = "rbSourceText";
      this.rbSourceText.Size = new System.Drawing.Size(132, 17);
      this.rbSourceText.TabIndex = 2;
      this.rbSourceText.Text = "Text (from Clipboard)";
      this.rbSourceText.UseVisualStyleBackColor = true;
      // 
      // rbSourceFile
      // 
      this.rbSourceFile.AutoSize = true;
      this.rbSourceFile.Checked = true;
      this.rbSourceFile.Location = new System.Drawing.Point(19, 21);
      this.rbSourceFile.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
      this.rbSourceFile.Name = "rbSourceFile";
      this.rbSourceFile.Size = new System.Drawing.Size(46, 17);
      this.rbSourceFile.TabIndex = 0;
      this.rbSourceFile.TabStop = true;
      this.rbSourceFile.Text = "File:";
      this.rbSourceFile.UseVisualStyleBackColor = true;
      this.rbSourceFile.CheckedChanged += new System.EventHandler(this.rbSourceFile_CheckedChanged);
      // 
      // gbTarget
      // 
      this.gbTarget.Controls.Add(this.rbTargetUTF16);
      this.gbTarget.Controls.Add(this.rbTargetFile);
      this.gbTarget.Controls.Add(this.rbTargetObject);
      this.gbTarget.Controls.Add(this.rbTargetUTF8);
      this.gbTarget.Location = new System.Drawing.Point(289, 166);
      this.gbTarget.Name = "gbTarget";
      this.gbTarget.Size = new System.Drawing.Size(247, 118);
      this.gbTarget.TabIndex = 2;
      this.gbTarget.TabStop = false;
      this.gbTarget.Text = "Target";
      // 
      // rbTargetUTF16
      // 
      this.rbTargetUTF16.AutoSize = true;
      this.rbTargetUTF16.Location = new System.Drawing.Point(19, 44);
      this.rbTargetUTF16.Name = "rbTargetUTF16";
      this.rbTargetUTF16.Size = new System.Drawing.Size(99, 17);
      this.rbTargetUTF16.TabIndex = 6;
      this.rbTargetUTF16.TabStop = true;
      this.rbTargetUTF16.Text = "Text (UTF16 LE)";
      this.rbTargetUTF16.UseVisualStyleBackColor = true;
      // 
      // rbTargetFile
      // 
      this.rbTargetFile.AutoSize = true;
      this.rbTargetFile.Location = new System.Drawing.Point(19, 90);
      this.rbTargetFile.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
      this.rbTargetFile.Name = "rbTargetFile";
      this.rbTargetFile.Size = new System.Drawing.Size(76, 17);
      this.rbTargetFile.TabIndex = 8;
      this.rbTargetFile.TabStop = true;
      this.rbTargetFile.Text = "Binary file";
      this.rbTargetFile.UseVisualStyleBackColor = true;
      // 
      // rbTargetObject
      // 
      this.rbTargetObject.AutoSize = true;
      this.rbTargetObject.Location = new System.Drawing.Point(19, 67);
      this.rbTargetObject.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
      this.rbTargetObject.Name = "rbTargetObject";
      this.rbTargetObject.Size = new System.Drawing.Size(207, 17);
      this.rbTargetObject.TabIndex = 7;
      this.rbTargetObject.Text = ".NET Object (using BinaryFormatter)";
      this.rbTargetObject.UseVisualStyleBackColor = true;
      // 
      // rbTargetUTF8
      // 
      this.rbTargetUTF8.AutoSize = true;
      this.rbTargetUTF8.Checked = true;
      this.rbTargetUTF8.Location = new System.Drawing.Point(19, 21);
      this.rbTargetUTF8.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
      this.rbTargetUTF8.Name = "rbTargetUTF8";
      this.rbTargetUTF8.Size = new System.Drawing.Size(79, 17);
      this.rbTargetUTF8.TabIndex = 5;
      this.rbTargetUTF8.TabStop = true;
      this.rbTargetUTF8.Text = "Text (UTF8)";
      this.rbTargetUTF8.UseVisualStyleBackColor = true;
      // 
      // gbConversion
      // 
      this.gbConversion.Controls.Add(this.rbHEX);
      this.gbConversion.Controls.Add(this.rbBase64);
      this.gbConversion.Location = new System.Drawing.Point(12, 166);
      this.gbConversion.Name = "gbConversion";
      this.gbConversion.Size = new System.Drawing.Size(261, 118);
      this.gbConversion.TabIndex = 1;
      this.gbConversion.TabStop = false;
      this.gbConversion.Text = "Conversion";
      // 
      // rbHEX
      // 
      this.rbHEX.AutoSize = true;
      this.rbHEX.Location = new System.Drawing.Point(19, 70);
      this.rbHEX.Margin = new System.Windows.Forms.Padding(16, 16, 3, 3);
      this.rbHEX.Name = "rbHEX";
      this.rbHEX.Size = new System.Drawing.Size(45, 17);
      this.rbHEX.TabIndex = 4;
      this.rbHEX.TabStop = true;
      this.rbHEX.Text = "HEX";
      this.rbHEX.UseVisualStyleBackColor = true;
      // 
      // rbBase64
      // 
      this.rbBase64.AutoSize = true;
      this.rbBase64.Checked = true;
      this.rbBase64.Location = new System.Drawing.Point(19, 34);
      this.rbBase64.Margin = new System.Windows.Forms.Padding(16, 16, 3, 3);
      this.rbBase64.Name = "rbBase64";
      this.rbBase64.Size = new System.Drawing.Size(61, 17);
      this.rbBase64.TabIndex = 3;
      this.rbBase64.TabStop = true;
      this.rbBase64.Text = "Base64";
      this.rbBase64.UseVisualStyleBackColor = true;
      // 
      // openFileDialog
      // 
      this.openFileDialog.DefaultExt = "txt";
      this.openFileDialog.FileName = "Select File";
      this.openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // ConvertSettingsForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(548, 332);
      this.Controls.Add(this.gbConversion);
      this.Controls.Add(this.gbTarget);
      this.Controls.Add(this.gbSource);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "Convert";
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConvertSettingsForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Convert Raw Data";
      this.gbSource.ResumeLayout(false);
      this.gbSource.PerformLayout();
      this.gbTarget.ResumeLayout(false);
      this.gbTarget.PerformLayout();
      this.gbConversion.ResumeLayout(false);
      this.gbConversion.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox gbSource;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label lblPath;
    private System.Windows.Forms.RadioButton rbSourceText;
    private System.Windows.Forms.RadioButton rbSourceFile;
    private System.Windows.Forms.GroupBox gbTarget;
    private System.Windows.Forms.GroupBox gbConversion;
    private System.Windows.Forms.RadioButton rbHEX;
    private System.Windows.Forms.RadioButton rbBase64;
    private System.Windows.Forms.RadioButton rbTargetUTF8;
    private System.Windows.Forms.RadioButton rbTargetUTF16;
    private System.Windows.Forms.RadioButton rbTargetFile;
    private System.Windows.Forms.RadioButton rbTargetObject;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
  }
}