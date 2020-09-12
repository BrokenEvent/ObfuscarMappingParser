namespace ObfuscarMappingParser
{
  partial class StacktraceAnalyerForm
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
      BrokenEvent.Shared.Controls.ListHighlight listHighlight1 = new BrokenEvent.Shared.Controls.ListHighlight();
      BrokenEvent.Shared.Controls.ListHighlight listHighlight2 = new BrokenEvent.Shared.Controls.ListHighlight();
      BrokenEvent.Shared.Controls.ListHighlight listHighlight3 = new BrokenEvent.Shared.Controls.ListHighlight();
      BrokenEvent.Shared.Controls.PineappleTreeColumn pineappleTreeColumn1 = new BrokenEvent.Shared.Controls.PineappleTreeColumn();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.tbtnOpenInTree = new System.Windows.Forms.ToolStripButton();
      this.tbtnOpenInVS = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.tbtnShort = new System.Windows.Forms.ToolStripButton();
      this.tbtnSimple = new System.Windows.Forms.ToolStripButton();
      this.tbtnFull = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.tbtnShowOriginal = new System.Windows.Forms.ToolStripButton();
      this.tbtnSkipPrefix = new System.Windows.Forms.ToolStripButton();
      this.pineappleTreeView = new BrokenEvent.Shared.Controls.PineappleTreeView();
      this.toolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip
      // 
      this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnOpenInTree,
            this.tbtnOpenInVS,
            this.toolStripSeparator1,
            this.tbtnShort,
            this.tbtnSimple,
            this.tbtnFull,
            this.toolStripSeparator2,
            this.tbtnShowOriginal,
            this.tbtnSkipPrefix});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Size = new System.Drawing.Size(784, 25);
      this.toolStrip.TabIndex = 0;
      this.toolStrip.Text = "toolStrip1";
      // 
      // tbtnOpenInTree
      // 
      this.tbtnOpenInTree.Enabled = false;
      this.tbtnOpenInTree.Image = global::ObfuscarMappingParser.Properties.Resources.Tree;
      this.tbtnOpenInTree.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnOpenInTree.Name = "tbtnOpenInTree";
      this.tbtnOpenInTree.Size = new System.Drawing.Size(95, 22);
      this.tbtnOpenInTree.Text = "Open in Tree";
      this.tbtnOpenInTree.Click += new System.EventHandler(this.tbtnOpenInTree_Click);
      // 
      // tbtnOpenInVS
      // 
      this.tbtnOpenInVS.Enabled = false;
      this.tbtnOpenInVS.Image = global::ObfuscarMappingParser.Properties.Resources.Editor;
      this.tbtnOpenInVS.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnOpenInVS.Name = "tbtnOpenInVS";
      this.tbtnOpenInVS.Size = new System.Drawing.Size(103, 22);
      this.tbtnOpenInVS.Text = "Open in Editor";
      this.tbtnOpenInVS.Click += new System.EventHandler(this.tbtnOpenInVS_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // tbtnShort
      // 
      this.tbtnShort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnShort.Image = global::ObfuscarMappingParser.Properties.Resources.ShortView;
      this.tbtnShort.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnShort.Name = "tbtnShort";
      this.tbtnShort.Size = new System.Drawing.Size(23, 22);
      this.tbtnShort.Text = "Short view";
      this.tbtnShort.ToolTipText = "Show only item names and param names";
      this.tbtnShort.Click += new System.EventHandler(this.tbtnShort_Click);
      // 
      // tbtnSimple
      // 
      this.tbtnSimple.Checked = true;
      this.tbtnSimple.CheckState = System.Windows.Forms.CheckState.Checked;
      this.tbtnSimple.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnSimple.Image = global::ObfuscarMappingParser.Properties.Resources.SimpleView;
      this.tbtnSimple.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnSimple.Name = "tbtnSimple";
      this.tbtnSimple.Size = new System.Drawing.Size(23, 22);
      this.tbtnSimple.Text = "Simple view";
      this.tbtnSimple.ToolTipText = "Show full item names (with namespace) and short param names";
      this.tbtnSimple.Click += new System.EventHandler(this.tbtnSimple_Click);
      // 
      // tbtnFull
      // 
      this.tbtnFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnFull.Image = global::ObfuscarMappingParser.Properties.Resources.FullView;
      this.tbtnFull.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnFull.Name = "tbtnFull";
      this.tbtnFull.Size = new System.Drawing.Size(23, 22);
      this.tbtnFull.Text = "Full view";
      this.tbtnFull.ToolTipText = "Show namespaces in item names and params";
      this.tbtnFull.Click += new System.EventHandler(this.tbtnFull_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // tbtnShowOriginal
      // 
      this.tbtnShowOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.tbtnShowOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnShowOriginal.Name = "tbtnShowOriginal";
      this.tbtnShowOriginal.Size = new System.Drawing.Size(83, 22);
      this.tbtnShowOriginal.Text = "Show original";
      this.tbtnShowOriginal.ToolTipText = "Show original lines";
      this.tbtnShowOriginal.Click += new System.EventHandler(this.tbtnShowOriginal_Click);
      // 
      // tbtnSkipPrefix
      // 
      this.tbtnSkipPrefix.Checked = true;
      this.tbtnSkipPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
      this.tbtnSkipPrefix.Image = global::ObfuscarMappingParser.Properties.Resources.SkipPrefix;
      this.tbtnSkipPrefix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnSkipPrefix.Name = "tbtnSkipPrefix";
      this.tbtnSkipPrefix.Size = new System.Drawing.Size(92, 22);
      this.tbtnSkipPrefix.Text = "Skip prefixes";
      this.tbtnSkipPrefix.ToolTipText = "Skip prefixes in lines (at ...)";
      this.tbtnSkipPrefix.Click += new System.EventHandler(this.tbtnSkipPrefix_Click);
      // 
      // pineappleTreeView
      // 
      this.pineappleTreeView.AutoScroll = true;
      this.pineappleTreeView.BackColor = System.Drawing.SystemColors.ControlLightLight;
      listHighlight1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      listHighlight2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
      listHighlight3.Color = System.Drawing.Color.LightYellow;
      this.pineappleTreeView.Backlights.Add(listHighlight1);
      this.pineappleTreeView.Backlights.Add(listHighlight2);
      this.pineappleTreeView.Backlights.Add(listHighlight3);
      this.pineappleTreeView.CollapseImage = global::ObfuscarMappingParser.Properties.Resources.Collapse;
      this.pineappleTreeView.ColumnHeaderStyle = BrokenEvent.Shared.Controls.ColumnHeaderStyle.None;
      pineappleTreeColumn1.Autosize = true;
      pineappleTreeColumn1.HeaderText = null;
      pineappleTreeColumn1.Multiline = true;
      pineappleTreeColumn1.TextPadding = new System.Windows.Forms.Padding(2, 4, 2, 4);
      pineappleTreeColumn1.Width = 612;
      this.pineappleTreeView.Columns.Add(pineappleTreeColumn1);
      this.pineappleTreeView.DisabledColor = System.Drawing.SystemColors.Control;
      this.pineappleTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pineappleTreeView.DrawBorder = false;
      this.pineappleTreeView.DropHoverIndicatorColor = System.Drawing.Color.CornflowerBlue;
      this.pineappleTreeView.EmptyListText = "";
      this.pineappleTreeView.ExpandImage = global::ObfuscarMappingParser.Properties.Resources.Expand;
      this.pineappleTreeView.FullRowSelect = true;
      this.pineappleTreeView.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.pineappleTreeView.IconPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
      this.pineappleTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
      this.pineappleTreeView.Location = new System.Drawing.Point(0, 25);
      this.pineappleTreeView.Name = "pineappleTreeView";
      this.pineappleTreeView.ShowToolTip = true;
      this.pineappleTreeView.Size = new System.Drawing.Size(784, 536);
      this.pineappleTreeView.TabIndex = 1;
      this.pineappleTreeView.Text = "pineappleTreeView1";
      this.pineappleTreeView.NodeSelect += new System.EventHandler<BrokenEvent.Shared.Controls.NodeSelectEventArgs>(this.pineappleTreeView_NodeSelect);
      // 
      // StacktraceAnalyerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.pineappleTreeView);
      this.Controls.Add(this.toolStrip);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StacktraceAnalyerForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "StacktraceAnalyerForm";
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip;
    private BrokenEvent.Shared.Controls.PineappleTreeView pineappleTreeView;
    private System.Windows.Forms.ToolStripButton tbtnShort;
    private System.Windows.Forms.ToolStripButton tbtnSimple;
    private System.Windows.Forms.ToolStripButton tbtnFull;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton tbtnOpenInTree;
    private System.Windows.Forms.ToolStripButton tbtnOpenInVS;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton tbtnShowOriginal;
    private System.Windows.Forms.ToolStripButton tbtnSkipPrefix;

  }
}