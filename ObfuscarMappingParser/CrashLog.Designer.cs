namespace ObfuscarMappingParser
{
  partial class CrashLog
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
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnProcess = new System.Windows.Forms.ToolStripButton();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.tbValue = new System.Windows.Forms.TextBox();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProcess,
            this.btnOpen});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(587, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnProcess
      // 
      this.btnProcess.Image = global::ObfuscarMappingParser.Properties.Resources.Start;
      this.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnProcess.Name = "btnProcess";
      this.btnProcess.Size = new System.Drawing.Size(67, 22);
      this.btnProcess.Text = "Process";
      this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
      // 
      // btnOpen
      // 
      this.btnOpen.Image = global::ObfuscarMappingParser.Properties.Resources.Open;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(101, 22);
      this.btnOpen.Text = "Load from file";
      this.btnOpen.ToolTipText = "Load crashlog from file";
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // tbValue
      // 
      this.tbValue.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbValue.Font = new System.Drawing.Font("Consolas", 9F);
      this.tbValue.Location = new System.Drawing.Point(0, 25);
      this.tbValue.Multiline = true;
      this.tbValue.Name = "tbValue";
      this.tbValue.Size = new System.Drawing.Size(587, 272);
      this.tbValue.TabIndex = 1;
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "All files (*.*)|*.*";
      // 
      // CrashLog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(587, 297);
      this.Controls.Add(this.tbValue);
      this.Controls.Add(this.toolStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "CrashLog";
      this.ShowInTaskbar = false;
      this.Text = "Stacktrace processor";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.TextBox tbValue;
    private System.Windows.Forms.ToolStripButton btnProcess;
    private System.Windows.Forms.ToolStripButton btnOpen;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
  }
}