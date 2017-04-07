namespace ObfuscarMappingParser
{
  partial class ObjectViewerForm
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
      this.btnClose = new System.Windows.Forms.Button();
      this.propertyGrid = new System.Windows.Forms.PropertyGrid();
      this.lblToString = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(332, 346);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // propertyGrid
      // 
      this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.propertyGrid.HelpVisible = false;
      this.propertyGrid.Location = new System.Drawing.Point(12, 59);
      this.propertyGrid.Name = "propertyGrid";
      this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
      this.propertyGrid.Size = new System.Drawing.Size(395, 270);
      this.propertyGrid.TabIndex = 1;
      this.propertyGrid.ToolbarVisible = false;
      // 
      // lblToString
      // 
      this.lblToString.AutoSize = true;
      this.lblToString.BackColor = System.Drawing.Color.Transparent;
      this.lblToString.ForeColor = System.Drawing.Color.White;
      this.lblToString.Location = new System.Drawing.Point(57, 32);
      this.lblToString.Name = "lblToString";
      this.lblToString.Size = new System.Drawing.Size(38, 13);
      this.lblToString.TabIndex = 3;
      this.lblToString.Text = "label1";
      // 
      // ObjectViewerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(419, 377);
      this.Controls.Add(this.lblToString);
      this.Controls.Add(this.propertyGrid);
      this.Controls.Add(this.btnClose);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderText = "Object Viewer";
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ObjectViewerForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Object Viewer";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.PropertyGrid propertyGrid;
    private System.Windows.Forms.Label lblToString;
  }
}