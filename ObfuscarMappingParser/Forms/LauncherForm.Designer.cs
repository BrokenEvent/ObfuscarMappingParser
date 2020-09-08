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
      BrokenEvent.Shared.Controls.BrokenListColumn brokenListColumn1 = new BrokenEvent.Shared.Controls.BrokenListColumn();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.odFile = new System.Windows.Forms.OpenFileDialog();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.ilIcon = new System.Windows.Forms.ImageList(this.components);
      this.lblFiles = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.blvFiles = new BrokenEvent.Shared.Controls.BrokenListView();
      this.lblDnDHint = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Enabled = false;
      this.btnOk.Location = new System.Drawing.Point(566, 531);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(672, 531);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
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
      // btnBrowse
      // 
      this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnBrowse.Location = new System.Drawing.Point(12, 531);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(100, 23);
      this.btnBrowse.TabIndex = 6;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // ilIcon
      // 
      this.ilIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.ilIcon.ImageSize = new System.Drawing.Size(16, 16);
      this.ilIcon.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // lblFiles
      // 
      this.lblFiles.AutoSizeLabel = true;
      this.lblFiles.BackColor = System.Drawing.Color.Transparent;
      this.lblFiles.CharacterWrap = false;
      this.lblFiles.Font = new System.Drawing.Font("Segoe UI", 11F);
      this.lblFiles.Location = new System.Drawing.Point(12, 59);
      this.lblFiles.Name = "lblFiles";
      this.lblFiles.Size = new System.Drawing.Size(192, 21);
      this.lblFiles.TabIndex = 7;
      this.lblFiles.Text = "Select mapping file to open:";
      // 
      // blvFiles
      // 
      this.blvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.blvFiles.AutoScroll = true;
      this.blvFiles.AutoScrollMinSize = new System.Drawing.Size(758, 0);
      this.blvFiles.BackColor = System.Drawing.SystemColors.Window;
      this.blvFiles.ColumnHeaderStyle = BrokenEvent.Shared.Controls.ColumnHeaderStyle.None;
      brokenListColumn1.Autosize = true;
      brokenListColumn1.CharacterWrap = true;
      brokenListColumn1.HeaderAlignment = ((BrokenEvent.Shared.Controls.ItemTextAlignment)((BrokenEvent.Shared.Controls.ItemTextAlignment.HorizontalLeft | BrokenEvent.Shared.Controls.ItemTextAlignment.VerticalTop)));
      brokenListColumn1.HeaderText = null;
      brokenListColumn1.Multiline = true;
      brokenListColumn1.TextAlignment = ((BrokenEvent.Shared.Controls.ItemTextAlignment)((BrokenEvent.Shared.Controls.ItemTextAlignment.HorizontalLeft | BrokenEvent.Shared.Controls.ItemTextAlignment.VerticalTop)));
      brokenListColumn1.TextPadding = new System.Windows.Forms.Padding(2, 4, 2, 2);
      brokenListColumn1.Width = 758;
      this.blvFiles.Columns.Add(brokenListColumn1);
      this.blvFiles.DisabledColor = System.Drawing.Color.Empty;
      this.blvFiles.DrawBorder = false;
      this.blvFiles.DropHoverIndicatorColor = System.Drawing.Color.CornflowerBlue;
      this.blvFiles.EmptyListText = "No recent items found.";
      this.blvFiles.GroupsFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.blvFiles.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.blvFiles.ImageList = this.ilIcon;
      this.blvFiles.Location = new System.Drawing.Point(12, 86);
      this.blvFiles.Name = "blvFiles";
      this.blvFiles.SelectMode = BrokenEvent.Shared.Controls.BrokenListView.SelectionMode.AlwaysSelected;
      this.blvFiles.Size = new System.Drawing.Size(760, 424);
      this.blvFiles.TabIndex = 8;
      this.blvFiles.Text = "brokenListView1";
      this.blvFiles.TextPadding = new System.Windows.Forms.Padding(2);
      this.blvFiles.ItemSelected += new System.EventHandler<BrokenEvent.Shared.Controls.ItemSelectEventArgs>(this.blvFiles_ItemSelected);
      this.blvFiles.DoubleClick += new System.EventHandler(this.blvFiles_DoubleClick);
      // 
      // lblDnDHint
      // 
      this.lblDnDHint.AutoSize = true;
      this.lblDnDHint.ForeColor = System.Drawing.Color.Gray;
      this.lblDnDHint.Location = new System.Drawing.Point(118, 536);
      this.lblDnDHint.Name = "lblDnDHint";
      this.lblDnDHint.Size = new System.Drawing.Size(249, 13);
      this.lblDnDHint.TabIndex = 9;
      this.lblDnDHint.Text = "You can drop mapping files from Explorer here.";
      // 
      // LauncherForm
      // 
      this.AcceptButton = this.btnOk;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.ControlBox = false;
      this.Controls.Add(this.lblDnDHint);
      this.Controls.Add(this.blvFiles);
      this.Controls.Add(this.lblFiles);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "Open File";
      this.Name = "LauncherForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Obfuscar Mapping Parser Launcher";
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.LauncherForm_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.LauncherForm_DragOver);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.OpenFileDialog odFile;
    private System.Windows.Forms.Button btnBrowse;
    private BrokenEvent.Shared.Controls.ThemedLabel lblFiles;
    private System.Windows.Forms.ImageList ilIcon;
    private BrokenEvent.Shared.Controls.BrokenListView blvFiles;
    private System.Windows.Forms.Label lblDnDHint;
  }
}