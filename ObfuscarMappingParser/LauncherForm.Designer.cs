namespace ObfuscarMappingParser
{
  partial class LauncherForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.odFile = new System.Windows.Forms.OpenFileDialog();
      this.lblAppVersion = new System.Windows.Forms.Label();
      this.lblAppName = new System.Windows.Forms.Label();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.lvFiles = new System.Windows.Forms.ListView();
      this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.ilIcon = new System.Windows.Forms.ImageList(this.components);
      this.lblFiles = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Enabled = false;
      this.btnOk.Location = new System.Drawing.Point(287, 310);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(368, 310);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // odFile
      // 
      this.odFile.DefaultExt = "xml";
      this.odFile.FileName = "Mapping.xml";
      this.odFile.Filter = "Mapping XML files (*.xml)|*.xml";
      // 
      // lblAppVersion
      // 
      this.lblAppVersion.AutoSize = true;
      this.lblAppVersion.BackColor = System.Drawing.Color.Transparent;
      this.lblAppVersion.Location = new System.Drawing.Point(57, 29);
      this.lblAppVersion.Name = "lblAppVersion";
      this.lblAppVersion.Size = new System.Drawing.Size(35, 13);
      this.lblAppVersion.TabIndex = 4;
      this.lblAppVersion.Text = "label1";
      // 
      // lblAppName
      // 
      this.lblAppName.AutoSize = true;
      this.lblAppName.BackColor = System.Drawing.Color.Transparent;
      this.lblAppName.Font = new System.Drawing.Font("Tahoma", 16F);
      this.lblAppName.Location = new System.Drawing.Point(55, 3);
      this.lblAppName.Name = "lblAppName";
      this.lblAppName.Size = new System.Drawing.Size(258, 27);
      this.lblAppName.TabIndex = 3;
      this.lblAppName.Text = "Obfuscar mapping parser";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(12, 267);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 6;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // lvFiles
      // 
      this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename});
      this.lvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvFiles.HideSelection = false;
      this.lvFiles.Location = new System.Drawing.Point(12, 75);
      this.lvFiles.MultiSelect = false;
      this.lvFiles.Name = "lvFiles";
      this.lvFiles.Size = new System.Drawing.Size(431, 186);
      this.lvFiles.SmallImageList = this.ilIcon;
      this.lvFiles.TabIndex = 5;
      this.lvFiles.UseCompatibleStateImageBehavior = false;
      this.lvFiles.View = System.Windows.Forms.View.Details;
      this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
      this.lvFiles.DoubleClick += new System.EventHandler(this.lvFiles_DoubleClick);
      this.lvFiles.Resize += new System.EventHandler(this.lvFiles_Resize);
      // 
      // ilIcon
      // 
      this.ilIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcon.ImageStream")));
      this.ilIcon.TransparentColor = System.Drawing.Color.Transparent;
      this.ilIcon.Images.SetKeyName(0, "Document.png");
      // 
      // lblFiles
      // 
      this.lblFiles.AutoSize = true;
      this.lblFiles.Location = new System.Drawing.Point(12, 59);
      this.lblFiles.Name = "lblFiles";
      this.lblFiles.Size = new System.Drawing.Size(136, 13);
      this.lblFiles.TabIndex = 7;
      this.lblFiles.Text = "Select mapping file to open";
      // 
      // LauncherForm
      // 
      this.AcceptButton = this.btnOk;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(455, 340);
      this.ControlBox = false;
      this.Controls.Add(this.lblFiles);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.lvFiles);
      this.Controls.Add(this.lblAppVersion);
      this.Controls.Add(this.lblAppName);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "LauncherForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Obfuscar Mapping Parser launcher";
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.LauncherForm_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.LauncherForm_DragOver);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.OpenFileDialog odFile;
    private System.Windows.Forms.Label lblAppVersion;
    private System.Windows.Forms.Label lblAppName;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.ListView lvFiles;
    private System.Windows.Forms.ColumnHeader chFilename;
    private System.Windows.Forms.Label lblFiles;
    private System.Windows.Forms.ImageList ilIcon;
  }
}