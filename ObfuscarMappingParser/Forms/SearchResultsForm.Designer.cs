namespace ObfuscarMappingParser
{
  partial class SearchResultsForm
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
      this.lvItems = new System.Windows.Forms.ListView();
      this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.SuspendLayout();
      // 
      // lvItems
      // 
      this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem});
      this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvItems.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvItems.HideSelection = false;
      this.lvItems.Location = new System.Drawing.Point(0, 0);
      this.lvItems.MultiSelect = false;
      this.lvItems.Name = "lvItems";
      this.lvItems.ShowItemToolTips = true;
      this.lvItems.Size = new System.Drawing.Size(489, 218);
      this.lvItems.TabIndex = 1;
      this.lvItems.UseCompatibleStateImageBehavior = false;
      this.lvItems.View = System.Windows.Forms.View.Details;
      this.lvItems.DoubleClick += new System.EventHandler(this.lvItems_DoubleClick);
      this.lvItems.Resize += new System.EventHandler(this.lvItems_Resize);
      // 
      // SearchResultsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(489, 218);
      this.Controls.Add(this.lvItems);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "SearchResultsForm";
      this.ShowInTaskbar = false;
      this.Text = "SearchResults";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvItems;
    private System.Windows.Forms.ColumnHeader chItem;

  }
}