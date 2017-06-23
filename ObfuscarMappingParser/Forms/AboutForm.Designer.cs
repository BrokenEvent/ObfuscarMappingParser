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
      this.llblGitHub = new System.Windows.Forms.LinkLabel();
      this.llblProjectPage = new System.Windows.Forms.LinkLabel();
      this.llblProjectPageDesc = new System.Windows.Forms.Label();
      this.llblGitHubDesc = new System.Windows.Forms.Label();
      this.llblTrello = new System.Windows.Forms.LinkLabel();
      this.lblTrelloBoardDesc = new System.Windows.Forms.Label();
      this.imgTrello = new System.Windows.Forms.PictureBox();
      this.imgProject = new System.Windows.Forms.PictureBox();
      this.imgGitHub = new System.Windows.Forms.PictureBox();
      this.pbLogo = new System.Windows.Forms.PictureBox();
      this.indUpdate = new BrokenEvent.Shared.Controls.Indicator();
      this.lblUpdateState = new System.Windows.Forms.Label();
      this.llblUpdate = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.imgTrello)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgProject)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgGitHub)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(405, 318);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // lblAppVersion
      // 
      this.lblAppVersion.AutoSize = true;
      this.lblAppVersion.BackColor = System.Drawing.Color.Transparent;
      this.lblAppVersion.ForeColor = System.Drawing.Color.White;
      this.lblAppVersion.Location = new System.Drawing.Point(57, 32);
      this.lblAppVersion.Name = "lblAppVersion";
      this.lblAppVersion.Size = new System.Drawing.Size(38, 13);
      this.lblAppVersion.TabIndex = 2;
      this.lblAppVersion.Text = "label1";
      // 
      // lblCopyright
      // 
      this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblCopyright.AutoSize = true;
      this.lblCopyright.Location = new System.Drawing.Point(12, 315);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(250, 13);
      this.lblCopyright.TabIndex = 3;
      this.lblCopyright.Text = "©2014 - 2017, Broken Event. All rights reserved.";
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new System.Drawing.Point(12, 58);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(339, 13);
      this.lblMessage.TabIndex = 4;
      this.lblMessage.Text = "If you have any questions or proposals, feel free to send them to";
      // 
      // lblSupportEmail
      // 
      this.lblSupportEmail.AutoSize = true;
      this.lblSupportEmail.Location = new System.Drawing.Point(12, 71);
      this.lblSupportEmail.Name = "lblSupportEmail";
      this.lblSupportEmail.Size = new System.Drawing.Size(82, 13);
      this.lblSupportEmail.TabIndex = 5;
      this.lblSupportEmail.Text = "Support email:";
      // 
      // llblSupportEmail
      // 
      this.llblSupportEmail.AutoSize = true;
      this.llblSupportEmail.Cursor = System.Windows.Forms.Cursors.Hand;
      this.llblSupportEmail.Location = new System.Drawing.Point(94, 71);
      this.llblSupportEmail.Name = "llblSupportEmail";
      this.llblSupportEmail.Size = new System.Drawing.Size(145, 13);
      this.llblSupportEmail.TabIndex = 6;
      this.llblSupportEmail.TabStop = true;
      this.llblSupportEmail.Text = "contact@brokenevent.com";
      this.llblSupportEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblSupportEmail_LinkClicked);
      // 
      // lblWarning
      // 
      this.lblWarning.Location = new System.Drawing.Point(12, 97);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.Size = new System.Drawing.Size(370, 42);
      this.lblWarning.TabIndex = 7;
      this.lblWarning.Text = "This project is not directly connected with the original Obfuscar, it is complete" +
    "ly side software.\r\nTo get original Obfuscar visit:";
      // 
      // llblObfuscar
      // 
      this.llblObfuscar.AutoSize = true;
      this.llblObfuscar.Location = new System.Drawing.Point(12, 139);
      this.llblObfuscar.Name = "llblObfuscar";
      this.llblObfuscar.Size = new System.Drawing.Size(163, 13);
      this.llblObfuscar.TabIndex = 8;
      this.llblObfuscar.TabStop = true;
      this.llblObfuscar.Text = "https://obfuscar.lextudio.com/";
      this.llblObfuscar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Hyperlink_LinkClicked);
      // 
      // llblWebSite
      // 
      this.llblWebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.llblWebSite.AutoSize = true;
      this.llblWebSite.Location = new System.Drawing.Point(12, 328);
      this.llblWebSite.Name = "llblWebSite";
      this.llblWebSite.Size = new System.Drawing.Size(129, 13);
      this.llblWebSite.TabIndex = 9;
      this.llblWebSite.TabStop = true;
      this.llblWebSite.Text = "http://brokenevent.com";
      this.llblWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Hyperlink_LinkClicked);
      // 
      // lblMSImageLibrary
      // 
      this.lblMSImageLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblMSImageLibrary.AutoSize = true;
      this.lblMSImageLibrary.Location = new System.Drawing.Point(12, 289);
      this.lblMSImageLibrary.Name = "lblMSImageLibrary";
      this.lblMSImageLibrary.Size = new System.Drawing.Size(373, 13);
      this.lblMSImageLibrary.TabIndex = 10;
      this.lblMSImageLibrary.Text = "Uses Microsoft® Visual Studio® 2012 Image Library. All rights reserved.";
      // 
      // lblPdbCopyright
      // 
      this.lblPdbCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblPdbCopyright.AutoSize = true;
      this.lblPdbCopyright.Location = new System.Drawing.Point(12, 276);
      this.lblPdbCopyright.Name = "lblPdbCopyright";
      this.lblPdbCopyright.Size = new System.Drawing.Size(449, 13);
      this.lblPdbCopyright.TabIndex = 11;
      this.lblPdbCopyright.Text = "Uses Microsoft.Cci.Pdb code. All trademark rights belongs to Microsoft® Corporati" +
    "on.";
      // 
      // llblGitHub
      // 
      this.llblGitHub.AutoSize = true;
      this.llblGitHub.Location = new System.Drawing.Point(37, 175);
      this.llblGitHub.Name = "llblGitHub";
      this.llblGitHub.Size = new System.Drawing.Size(302, 13);
      this.llblGitHub.TabIndex = 12;
      this.llblGitHub.TabStop = true;
      this.llblGitHub.Text = "https://github.com/BrokenEvent/ObfuscarMappingParser";
      this.llblGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Hyperlink_LinkClicked);
      // 
      // llblProjectPage
      // 
      this.llblProjectPage.AutoSize = true;
      this.llblProjectPage.Location = new System.Drawing.Point(37, 211);
      this.llblProjectPage.Name = "llblProjectPage";
      this.llblProjectPage.Size = new System.Drawing.Size(255, 13);
      this.llblProjectPage.TabIndex = 13;
      this.llblProjectPage.TabStop = true;
      this.llblProjectPage.Text = "http://brokenevent.com/projects/obfuscarparser";
      this.llblProjectPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Hyperlink_LinkClicked);
      // 
      // llblProjectPageDesc
      // 
      this.llblProjectPageDesc.AutoSize = true;
      this.llblProjectPageDesc.Location = new System.Drawing.Point(37, 198);
      this.llblProjectPageDesc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
      this.llblProjectPageDesc.Name = "llblProjectPageDesc";
      this.llblProjectPageDesc.Size = new System.Drawing.Size(115, 13);
      this.llblProjectPageDesc.TabIndex = 14;
      this.llblProjectPageDesc.Text = "Official project page:";
      // 
      // llblGitHubDesc
      // 
      this.llblGitHubDesc.AutoSize = true;
      this.llblGitHubDesc.Location = new System.Drawing.Point(37, 162);
      this.llblGitHubDesc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
      this.llblGitHubDesc.Name = "llblGitHubDesc";
      this.llblGitHubDesc.Size = new System.Drawing.Size(102, 13);
      this.llblGitHubDesc.TabIndex = 15;
      this.llblGitHubDesc.Text = "GitHub repository:";
      // 
      // llblTrello
      // 
      this.llblTrello.AutoSize = true;
      this.llblTrello.Location = new System.Drawing.Point(37, 247);
      this.llblTrello.Name = "llblTrello";
      this.llblTrello.Size = new System.Drawing.Size(280, 13);
      this.llblTrello.TabIndex = 16;
      this.llblTrello.TabStop = true;
      this.llblTrello.Text = "https://trello.com/b/qglGMdCS/brokenevent-projects";
      this.llblTrello.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Hyperlink_LinkClicked);
      // 
      // lblTrelloBoardDesc
      // 
      this.lblTrelloBoardDesc.AutoSize = true;
      this.lblTrelloBoardDesc.Location = new System.Drawing.Point(37, 234);
      this.lblTrelloBoardDesc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
      this.lblTrelloBoardDesc.Name = "lblTrelloBoardDesc";
      this.lblTrelloBoardDesc.Size = new System.Drawing.Size(140, 13);
      this.lblTrelloBoardDesc.TabIndex = 17;
      this.lblTrelloBoardDesc.Text = "Trello board for feedback:";
      // 
      // imgTrello
      // 
      this.imgTrello.Image = global::ObfuscarMappingParser.Properties.Resources.trello;
      this.imgTrello.Location = new System.Drawing.Point(15, 234);
      this.imgTrello.Name = "imgTrello";
      this.imgTrello.Size = new System.Drawing.Size(16, 16);
      this.imgTrello.TabIndex = 20;
      this.imgTrello.TabStop = false;
      // 
      // imgProject
      // 
      this.imgProject.Image = global::ObfuscarMappingParser.Properties.Resources.brokenevent;
      this.imgProject.Location = new System.Drawing.Point(15, 198);
      this.imgProject.Name = "imgProject";
      this.imgProject.Size = new System.Drawing.Size(16, 16);
      this.imgProject.TabIndex = 19;
      this.imgProject.TabStop = false;
      // 
      // imgGitHub
      // 
      this.imgGitHub.Image = global::ObfuscarMappingParser.Properties.Resources.github;
      this.imgGitHub.Location = new System.Drawing.Point(15, 162);
      this.imgGitHub.Name = "imgGitHub";
      this.imgGitHub.Size = new System.Drawing.Size(16, 16);
      this.imgGitHub.TabIndex = 18;
      this.imgGitHub.TabStop = false;
      // 
      // pbLogo
      // 
      this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbLogo.Image = global::ObfuscarMappingParser.Properties.Resources.logo_160;
      this.pbLogo.Location = new System.Drawing.Point(388, 58);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new System.Drawing.Size(120, 120);
      this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pbLogo.TabIndex = 21;
      this.pbLogo.TabStop = false;
      // 
      // indUpdate
      // 
      this.indUpdate.BackColor = System.Drawing.Color.Transparent;
      this.indUpdate.ForeColor = System.Drawing.Color.RoyalBlue;
      this.indUpdate.LinkedControl = null;
      this.indUpdate.Location = new System.Drawing.Point(388, 184);
      this.indUpdate.Name = "indUpdate";
      this.indUpdate.Size = new System.Drawing.Size(24, 24);
      this.indUpdate.TabIndex = 22;
      this.indUpdate.Text = "indicator1";
      // 
      // lblUpdateState
      // 
      this.lblUpdateState.AutoSize = true;
      this.lblUpdateState.Location = new System.Drawing.Point(418, 189);
      this.lblUpdateState.Name = "lblUpdateState";
      this.lblUpdateState.Size = new System.Drawing.Size(64, 13);
      this.lblUpdateState.TabIndex = 23;
      this.lblUpdateState.Text = "Checking...";
      this.lblUpdateState.Visible = false;
      // 
      // llblUpdate
      // 
      this.llblUpdate.AutoSize = true;
      this.llblUpdate.Location = new System.Drawing.Point(385, 184);
      this.llblUpdate.Name = "llblUpdate";
      this.llblUpdate.Size = new System.Drawing.Size(45, 13);
      this.llblUpdate.TabIndex = 24;
      this.llblUpdate.TabStop = true;
      this.llblUpdate.Text = "Update";
      this.llblUpdate.Visible = false;
      this.llblUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblUpdate_LinkClicked);
      // 
      // AboutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(517, 349);
      this.ControlBox = false;
      this.Controls.Add(this.llblUpdate);
      this.Controls.Add(this.lblUpdateState);
      this.Controls.Add(this.indUpdate);
      this.Controls.Add(this.pbLogo);
      this.Controls.Add(this.imgTrello);
      this.Controls.Add(this.imgProject);
      this.Controls.Add(this.imgGitHub);
      this.Controls.Add(this.lblTrelloBoardDesc);
      this.Controls.Add(this.llblTrello);
      this.Controls.Add(this.llblGitHubDesc);
      this.Controls.Add(this.llblProjectPageDesc);
      this.Controls.Add(this.llblProjectPage);
      this.Controls.Add(this.llblGitHub);
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
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderText = "Opfuscar Mapping Parser";
      this.Name = "AboutForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About Obfuscar Mapping Parser";
      this.Load += new System.EventHandler(this.AboutForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.imgTrello)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgProject)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgGitHub)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

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
    private System.Windows.Forms.LinkLabel llblGitHub;
    private System.Windows.Forms.LinkLabel llblProjectPage;
    private System.Windows.Forms.Label llblProjectPageDesc;
    private System.Windows.Forms.Label llblGitHubDesc;
    private System.Windows.Forms.LinkLabel llblTrello;
    private System.Windows.Forms.Label lblTrelloBoardDesc;
    private System.Windows.Forms.PictureBox imgGitHub;
    private System.Windows.Forms.PictureBox imgProject;
    private System.Windows.Forms.PictureBox imgTrello;
    private System.Windows.Forms.PictureBox pbLogo;
    private BrokenEvent.Shared.Controls.Indicator indUpdate;
    private System.Windows.Forms.Label lblUpdateState;
    private System.Windows.Forms.LinkLabel llblUpdate;
  }
}