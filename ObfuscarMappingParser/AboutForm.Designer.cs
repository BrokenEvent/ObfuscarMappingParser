namespace ObfuscarMappingParser
{
  partial class AboutForm
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
      this.lblAppName = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.lblAppVersion = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblMessage = new System.Windows.Forms.Label();
      this.lblSupportEmail = new System.Windows.Forms.Label();
      this.llblSupportEmail = new System.Windows.Forms.LinkLabel();
      this.lblWarning = new System.Windows.Forms.Label();
      this.llblObfuscar = new System.Windows.Forms.LinkLabel();
      this.llblWebSite = new System.Windows.Forms.LinkLabel();
      this.lblMSImageLibrary = new System.Windows.Forms.Label();
      this.lblPdbCopyright = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblAppName
      // 
      this.lblAppName.AutoSize = true;
      this.lblAppName.BackColor = System.Drawing.Color.Transparent;
      this.lblAppName.Font = new System.Drawing.Font("Tahoma", 16F);
      this.lblAppName.Location = new System.Drawing.Point(55, 3);
      this.lblAppName.Name = "lblAppName";
      this.lblAppName.Size = new System.Drawing.Size(258, 27);
      this.lblAppName.TabIndex = 0;
      this.lblAppName.Text = "Obfuscar mapping parser";
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnOk.Location = new System.Drawing.Point(359, 215);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // lblAppVersion
      // 
      this.lblAppVersion.AutoSize = true;
      this.lblAppVersion.BackColor = System.Drawing.Color.Transparent;
      this.lblAppVersion.Location = new System.Drawing.Point(57, 29);
      this.lblAppVersion.Name = "lblAppVersion";
      this.lblAppVersion.Size = new System.Drawing.Size(35, 13);
      this.lblAppVersion.TabIndex = 2;
      this.lblAppVersion.Text = "label1";
      // 
      // lblCopyright
      // 
      this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblCopyright.AutoSize = true;
      this.lblCopyright.Location = new System.Drawing.Point(12, 212);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(210, 13);
      this.lblCopyright.TabIndex = 3;
      this.lblCopyright.Text = "©2014, Broken Event. All rights reserved.";
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new System.Drawing.Point(12, 58);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(322, 13);
      this.lblMessage.TabIndex = 4;
      this.lblMessage.Text = "If you have any questions or proposals, feel free to send them to";
      // 
      // lblSupportEmail
      // 
      this.lblSupportEmail.AutoSize = true;
      this.lblSupportEmail.Location = new System.Drawing.Point(12, 71);
      this.lblSupportEmail.Name = "lblSupportEmail";
      this.lblSupportEmail.Size = new System.Drawing.Size(76, 13);
      this.lblSupportEmail.TabIndex = 5;
      this.lblSupportEmail.Text = "Support email:";
      // 
      // llblSupportEmail
      // 
      this.llblSupportEmail.AutoSize = true;
      this.llblSupportEmail.Cursor = System.Windows.Forms.Cursors.Hand;
      this.llblSupportEmail.Location = new System.Drawing.Point(94, 71);
      this.llblSupportEmail.Name = "llblSupportEmail";
      this.llblSupportEmail.Size = new System.Drawing.Size(137, 13);
      this.llblSupportEmail.TabIndex = 6;
      this.llblSupportEmail.TabStop = true;
      this.llblSupportEmail.Text = "contact@brokenevent.com";
      this.llblSupportEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblSupportEmail_LinkClicked);
      // 
      // lblWarning
      // 
      this.lblWarning.Location = new System.Drawing.Point(12, 97);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.Size = new System.Drawing.Size(422, 42);
      this.lblWarning.TabIndex = 7;
      this.lblWarning.Text = "This project is not directly connected with the original Obfuscar, it is complete" +
          "ly side software.\r\nTo get original Obfuscar visit:";
      // 
      // llblObfuscar
      // 
      this.llblObfuscar.AutoSize = true;
      this.llblObfuscar.Location = new System.Drawing.Point(12, 139);
      this.llblObfuscar.Name = "llblObfuscar";
      this.llblObfuscar.Size = new System.Drawing.Size(160, 13);
      this.llblObfuscar.TabIndex = 8;
      this.llblObfuscar.TabStop = true;
      this.llblObfuscar.Text = "https://obfuscar.codeplex.com/";
      this.llblObfuscar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblObfuscar_LinkClicked);
      // 
      // llblWebSite
      // 
      this.llblWebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.llblWebSite.AutoSize = true;
      this.llblWebSite.Location = new System.Drawing.Point(12, 225);
      this.llblWebSite.Name = "llblWebSite";
      this.llblWebSite.Size = new System.Drawing.Size(123, 13);
      this.llblWebSite.TabIndex = 9;
      this.llblWebSite.TabStop = true;
      this.llblWebSite.Text = "http://brokenevent.com";
      this.llblWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblWebSite_LinkClicked);
      // 
      // lblMSImageLibrary
      // 
      this.lblMSImageLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblMSImageLibrary.AutoSize = true;
      this.lblMSImageLibrary.Location = new System.Drawing.Point(12, 186);
      this.lblMSImageLibrary.Name = "lblMSImageLibrary";
      this.lblMSImageLibrary.Size = new System.Drawing.Size(354, 13);
      this.lblMSImageLibrary.TabIndex = 10;
      this.lblMSImageLibrary.Text = "Uses Microsoft® Visual Studio® 2012 Image Library. All rights reserved.";
      // 
      // lblPdbCopyright
      // 
      this.lblPdbCopyright.AutoSize = true;
      this.lblPdbCopyright.Location = new System.Drawing.Point(12, 173);
      this.lblPdbCopyright.Name = "lblPdbCopyright";
      this.lblPdbCopyright.Size = new System.Drawing.Size(417, 13);
      this.lblPdbCopyright.TabIndex = 11;
      this.lblPdbCopyright.Text = "Uses Microsoft.Cci.Pdb code. All trademark rights belongs to Microsoft® Corporati" +
          "on.";
      // 
      // AboutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(446, 246);
      this.ControlBox = false;
      this.Controls.Add(this.lblPdbCopyright);
      this.Controls.Add(this.lblMSImageLibrary);
      this.Controls.Add(this.llblWebSite);
      this.Controls.Add(this.llblObfuscar);
      this.Controls.Add(this.lblWarning);
      this.Controls.Add(this.llblSupportEmail);
      this.Controls.Add(this.lblSupportEmail);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.lblAppVersion);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.lblAppName);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "AboutForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About BrokenEvent.PADCreator";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblAppName;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label lblAppVersion;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.Label lblSupportEmail;
    private System.Windows.Forms.LinkLabel llblSupportEmail;
    private System.Windows.Forms.Label lblWarning;
    private System.Windows.Forms.LinkLabel llblObfuscar;
    private System.Windows.Forms.LinkLabel llblWebSite;
    private System.Windows.Forms.Label lblMSImageLibrary;
    private System.Windows.Forms.Label lblPdbCopyright;
  }
}