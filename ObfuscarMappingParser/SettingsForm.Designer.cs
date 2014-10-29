namespace ObfuscarMappingParser
{
  partial class SettingsForm
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
      this.tabControl = new System.Windows.Forms.TabControl();
      this.tpVisualStudio = new System.Windows.Forms.TabPage();
      this.lbVS = new System.Windows.Forms.ListBox();
      this.lblVS = new System.Windows.Forms.Label();
      this.tpNaming = new System.Windows.Forms.TabPage();
      this.cbSimplifySystemNames = new System.Windows.Forms.CheckBox();
      this.cbShowUnicode = new System.Windows.Forms.CheckBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.tabControl.SuspendLayout();
      this.tpVisualStudio.SuspendLayout();
      this.tpNaming.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl
      // 
      this.tabControl.Controls.Add(this.tpVisualStudio);
      this.tabControl.Controls.Add(this.tpNaming);
      this.tabControl.Location = new System.Drawing.Point(12, 12);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(291, 209);
      this.tabControl.TabIndex = 0;
      // 
      // tpVisualStudio
      // 
      this.tpVisualStudio.Controls.Add(this.lbVS);
      this.tpVisualStudio.Controls.Add(this.lblVS);
      this.tpVisualStudio.Location = new System.Drawing.Point(4, 22);
      this.tpVisualStudio.Name = "tpVisualStudio";
      this.tpVisualStudio.Padding = new System.Windows.Forms.Padding(3);
      this.tpVisualStudio.Size = new System.Drawing.Size(283, 183);
      this.tpVisualStudio.TabIndex = 0;
      this.tpVisualStudio.Text = "External editor";
      this.tpVisualStudio.UseVisualStyleBackColor = true;
      // 
      // lbVS
      // 
      this.lbVS.FormattingEnabled = true;
      this.lbVS.Location = new System.Drawing.Point(9, 19);
      this.lbVS.Name = "lbVS";
      this.lbVS.Size = new System.Drawing.Size(268, 160);
      this.lbVS.TabIndex = 1;
      // 
      // lblVS
      // 
      this.lblVS.AutoSize = true;
      this.lblVS.Location = new System.Drawing.Point(6, 3);
      this.lblVS.Name = "lblVS";
      this.lblVS.Size = new System.Drawing.Size(176, 13);
      this.lblVS.TabIndex = 0;
      this.lblVS.Text = "Select external editor to open files:";
      // 
      // tpNaming
      // 
      this.tpNaming.Controls.Add(this.cbSimplifySystemNames);
      this.tpNaming.Controls.Add(this.cbShowUnicode);
      this.tpNaming.Location = new System.Drawing.Point(4, 22);
      this.tpNaming.Name = "tpNaming";
      this.tpNaming.Padding = new System.Windows.Forms.Padding(3);
      this.tpNaming.Size = new System.Drawing.Size(283, 183);
      this.tpNaming.TabIndex = 1;
      this.tpNaming.Text = "Naming";
      this.tpNaming.UseVisualStyleBackColor = true;
      // 
      // cbSimplifySystemNames
      // 
      this.cbSimplifySystemNames.AutoSize = true;
      this.cbSimplifySystemNames.Location = new System.Drawing.Point(20, 42);
      this.cbSimplifySystemNames.Name = "cbSimplifySystemNames";
      this.cbSimplifySystemNames.Size = new System.Drawing.Size(239, 17);
      this.cbSimplifySystemNames.TabIndex = 1;
      this.cbSimplifySystemNames.Text = "Simplify system names (System.Int32 → int)";
      this.cbSimplifySystemNames.UseVisualStyleBackColor = true;
      // 
      // cbShowUnicode
      // 
      this.cbShowUnicode.AutoSize = true;
      this.cbShowUnicode.Location = new System.Drawing.Point(20, 19);
      this.cbShowUnicode.Name = "cbShowUnicode";
      this.cbShowUnicode.Size = new System.Drawing.Size(158, 17);
      this.cbShowUnicode.TabIndex = 0;
      this.cbShowUnicode.Text = "Show Unicode symbols as is";
      this.cbShowUnicode.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(143, 227);
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
      this.btnCancel.Location = new System.Drawing.Point(224, 227);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // SettingsForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(311, 259);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.tabControl);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "SettingsForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Obfuscar mapping parser settings";
      this.tabControl.ResumeLayout(false);
      this.tpVisualStudio.ResumeLayout(false);
      this.tpVisualStudio.PerformLayout();
      this.tpNaming.ResumeLayout(false);
      this.tpNaming.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tpVisualStudio;
    private System.Windows.Forms.ListBox lbVS;
    private System.Windows.Forms.Label lblVS;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TabPage tpNaming;
    private System.Windows.Forms.CheckBox cbSimplifySystemNames;
    private System.Windows.Forms.CheckBox cbShowUnicode;
  }
}