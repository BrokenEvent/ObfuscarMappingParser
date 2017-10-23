namespace ObfuscarMappingParser
{
  partial class PDBManagerForm
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
      this.lvList = new System.Windows.Forms.ListView();
      this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnClose = new System.Windows.Forms.Button();
      this.lblLoadedPDBs = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.btnAttach = new System.Windows.Forms.Button();
      this.btnDetach = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lvList
      // 
      this.lvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chGuid});
      this.lvList.FullRowSelect = true;
      this.lvList.HideSelection = false;
      this.lvList.Location = new System.Drawing.Point(12, 85);
      this.lvList.Name = "lvList";
      this.lvList.ShowItemToolTips = true;
      this.lvList.Size = new System.Drawing.Size(477, 185);
      this.lvList.TabIndex = 0;
      this.lvList.UseCompatibleStateImageBehavior = false;
      this.lvList.View = System.Windows.Forms.View.Details;
      this.lvList.SelectedIndexChanged += new System.EventHandler(this.lvList_SelectedIndexChanged);
      this.lvList.Resize += new System.EventHandler(this.lvList_Resize);
      // 
      // chFilename
      // 
      this.chFilename.Text = "Filename";
      // 
      // chGuid
      // 
      this.chGuid.Text = "GUID";
      this.chGuid.Width = 230;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(389, 291);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(100, 23);
      this.btnClose.TabIndex = 1;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // lblLoadedPDBs
      // 
      this.lblLoadedPDBs.AutoSizeLabel = true;
      this.lblLoadedPDBs.BackColor = System.Drawing.Color.Transparent;
      this.lblLoadedPDBs.CharacterWrap = false;
      this.lblLoadedPDBs.Location = new System.Drawing.Point(12, 58);
      this.lblLoadedPDBs.Name = "lblLoadedPDBs";
      this.lblLoadedPDBs.Size = new System.Drawing.Size(104, 21);
      this.lblLoadedPDBs.TabIndex = 2;
      this.lblLoadedPDBs.Text = "Attached PDBs:";
      // 
      // btnAttach
      // 
      this.btnAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnAttach.Location = new System.Drawing.Point(15, 291);
      this.btnAttach.Name = "btnAttach";
      this.btnAttach.Size = new System.Drawing.Size(100, 23);
      this.btnAttach.TabIndex = 3;
      this.btnAttach.Text = "Attach";
      this.btnAttach.UseVisualStyleBackColor = true;
      this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
      // 
      // btnDetach
      // 
      this.btnDetach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnDetach.Enabled = false;
      this.btnDetach.Location = new System.Drawing.Point(121, 291);
      this.btnDetach.Name = "btnDetach";
      this.btnDetach.Size = new System.Drawing.Size(100, 23);
      this.btnDetach.TabIndex = 4;
      this.btnDetach.Text = "Detach";
      this.btnDetach.UseVisualStyleBackColor = true;
      this.btnDetach.Click += new System.EventHandler(this.btnDetach_Click);
      // 
      // PDBManagerForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(504, 322);
      this.Controls.Add(this.btnDetach);
      this.Controls.Add(this.btnAttach);
      this.Controls.Add(this.lblLoadedPDBs);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.lvList);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "PDB Manager";
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PDBManagerForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "PDB Manager";
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PDBManagerForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PDBManagerForm_DragEnter);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvList;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.ColumnHeader chFilename;
    private BrokenEvent.Shared.Controls.ThemedLabel lblLoadedPDBs;
    private System.Windows.Forms.Button btnAttach;
    private System.Windows.Forms.Button btnDetach;
    private System.Windows.Forms.ColumnHeader chGuid;
  }
}