namespace ObfuscarMappingParser
{
  partial class SearchDialog
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
      this.lvResults = new System.Windows.Forms.ListView();
      this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnSearch = new System.Windows.Forms.Button();
      this.lblSearchResults = new System.Windows.Forms.Label();
      this.tbSearch = new System.Windows.Forms.TextBox();
      this.lblSearch = new System.Windows.Forms.Label();
      this.btnClose = new System.Windows.Forms.Button();
      this.controlHighlight = new BrokenEvent.Shared.Controls.ControlHighlight(this.components);
      this.SuspendLayout();
      // 
      // lvResults
      // 
      this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
      this.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvResults.HideSelection = false;
      this.lvResults.Location = new System.Drawing.Point(12, 128);
      this.lvResults.Name = "lvResults";
      this.lvResults.ShowItemToolTips = true;
      this.lvResults.Size = new System.Drawing.Size(463, 158);
      this.lvResults.TabIndex = 5;
      this.lvResults.UseCompatibleStateImageBehavior = false;
      this.lvResults.View = System.Windows.Forms.View.Details;
      this.lvResults.DoubleClick += new System.EventHandler(this.lvResults_DoubleClick);
      this.lvResults.Enter += new System.EventHandler(this.Control_Enter);
      this.lvResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvResults_MouseDown);
      this.lvResults.Resize += new System.EventHandler(this.lvResults_Resize);
      // 
      // btnSearch
      // 
      this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSearch.Location = new System.Drawing.Point(400, 75);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new System.Drawing.Size(75, 23);
      this.btnSearch.TabIndex = 4;
      this.btnSearch.Text = "Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
      this.btnSearch.Enter += new System.EventHandler(this.Control_Enter);
      // 
      // lblSearchResults
      // 
      this.lblSearchResults.AutoSize = true;
      this.lblSearchResults.Location = new System.Drawing.Point(12, 112);
      this.lblSearchResults.Name = "lblSearchResults";
      this.lblSearchResults.Size = new System.Drawing.Size(81, 13);
      this.lblSearchResults.TabIndex = 1;
      this.lblSearchResults.Text = "Search results:";
      // 
      // tbSearch
      // 
      this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.tbSearch.Location = new System.Drawing.Point(12, 77);
      this.tbSearch.Name = "tbSearch";
      this.tbSearch.Size = new System.Drawing.Size(382, 22);
      this.tbSearch.TabIndex = 3;
      this.tbSearch.Enter += new System.EventHandler(this.Control_Enter);
      this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
      // 
      // lblSearch
      // 
      this.lblSearch.AutoSize = true;
      this.lblSearch.Location = new System.Drawing.Point(12, 61);
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Size = new System.Drawing.Size(62, 13);
      this.lblSearch.TabIndex = 2;
      this.lblSearch.Text = "Search for:";
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(375, 306);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(100, 23);
      this.btnClose.TabIndex = 1;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Enter += new System.EventHandler(this.Control_Enter);
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
      // SearchDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(487, 337);
      this.Controls.Add(this.lblSearch);
      this.Controls.Add(this.tbSearch);
      this.Controls.Add(this.lblSearchResults);
      this.Controls.Add(this.btnSearch);
      this.Controls.Add(this.lvResults);
      this.Controls.Add(this.btnClose);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "Search";
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 200);
      this.Name = "SearchDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Search";
      this.Click += new System.EventHandler(this.SearchDialog_Click);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvResults;
    private System.Windows.Forms.ColumnHeader columnHeader;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.Label lblSearchResults;
    private System.Windows.Forms.TextBox tbSearch;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.Button btnClose;
    private BrokenEvent.Shared.Controls.ControlHighlight controlHighlight;

  }
}