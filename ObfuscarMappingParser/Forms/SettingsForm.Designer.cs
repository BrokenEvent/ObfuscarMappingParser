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
      this.components = new System.ComponentModel.Container();
      this.lblVS = new System.Windows.Forms.Label();
      this.cbSimplifySystemNames = new System.Windows.Forms.CheckBox();
      this.cbShowUnicode = new System.Windows.Forms.CheckBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cbApplyVsToProject = new System.Windows.Forms.CheckBox();
      this.cbSimplifyNullable = new System.Windows.Forms.CheckBox();
      this.cbGroupByModules = new System.Windows.Forms.CheckBox();
      this.cbGroupByNamespaces = new System.Windows.Forms.CheckBox();
      this.cbUseColumns = new System.Windows.Forms.CheckBox();
      this.lvEditors = new System.Windows.Forms.ListView();
      this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.lblDoubleClick = new System.Windows.Forms.Label();
      this.cbDoubleClick = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // lblVS
      // 
      this.lblVS.AutoSize = true;
      this.lblVS.Location = new System.Drawing.Point(12, 60);
      this.lblVS.Name = "lblVS";
      this.lblVS.Size = new System.Drawing.Size(186, 13);
      this.lblVS.TabIndex = 0;
      this.lblVS.Text = "Select external editor to open files:";
      // 
      // cbSimplifySystemNames
      // 
      this.cbSimplifySystemNames.AutoSize = true;
      this.cbSimplifySystemNames.Location = new System.Drawing.Point(15, 262);
      this.cbSimplifySystemNames.Name = "cbSimplifySystemNames";
      this.cbSimplifySystemNames.Size = new System.Drawing.Size(241, 17);
      this.cbSimplifySystemNames.TabIndex = 1;
      this.cbSimplifySystemNames.Text = "Simplify system names (System.Int32 → int)";
      this.cbSimplifySystemNames.UseVisualStyleBackColor = true;
      // 
      // cbShowUnicode
      // 
      this.cbShowUnicode.AutoSize = true;
      this.cbShowUnicode.Location = new System.Drawing.Point(15, 239);
      this.cbShowUnicode.Name = "cbShowUnicode";
      this.cbShowUnicode.Size = new System.Drawing.Size(170, 17);
      this.cbShowUnicode.TabIndex = 0;
      this.cbShowUnicode.Text = "Show Unicode symbols as is";
      this.cbShowUnicode.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(186, 421);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(292, 421);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cbApplyVsToProject
      // 
      this.cbApplyVsToProject.AutoSize = true;
      this.cbApplyVsToProject.Location = new System.Drawing.Point(15, 203);
      this.cbApplyVsToProject.Name = "cbApplyVsToProject";
      this.cbApplyVsToProject.Size = new System.Drawing.Size(370, 17);
      this.cbApplyVsToProject.TabIndex = 3;
      this.cbApplyVsToProject.Text = "Apply to current project (changes default setting when unchecked)";
      this.cbApplyVsToProject.UseVisualStyleBackColor = true;
      // 
      // cbSimplifyNullable
      // 
      this.cbSimplifyNullable.AutoSize = true;
      this.cbSimplifyNullable.Location = new System.Drawing.Point(15, 285);
      this.cbSimplifyNullable.Name = "cbSimplifyNullable";
      this.cbSimplifyNullable.Size = new System.Drawing.Size(213, 17);
      this.cbSimplifyNullable.TabIndex = 4;
      this.cbSimplifyNullable.Text = "Simplify nullable (Nullable<A> → A?)";
      this.cbSimplifyNullable.UseVisualStyleBackColor = true;
      // 
      // cbGroupByModules
      // 
      this.cbGroupByModules.AutoSize = true;
      this.cbGroupByModules.Location = new System.Drawing.Point(15, 331);
      this.cbGroupByModules.Name = "cbGroupByModules";
      this.cbGroupByModules.Size = new System.Drawing.Size(121, 17);
      this.cbGroupByModules.TabIndex = 5;
      this.cbGroupByModules.Text = "Group by modules";
      this.cbGroupByModules.UseVisualStyleBackColor = true;
      // 
      // cbGroupByNamespaces
      // 
      this.cbGroupByNamespaces.AutoSize = true;
      this.cbGroupByNamespaces.Location = new System.Drawing.Point(15, 308);
      this.cbGroupByNamespaces.Name = "cbGroupByNamespaces";
      this.cbGroupByNamespaces.Size = new System.Drawing.Size(182, 17);
      this.cbGroupByNamespaces.TabIndex = 6;
      this.cbGroupByNamespaces.Text = "Group by original namespaces";
      this.cbGroupByNamespaces.UseVisualStyleBackColor = true;
      // 
      // cbUseColumns
      // 
      this.cbUseColumns.AutoSize = true;
      this.cbUseColumns.Location = new System.Drawing.Point(15, 354);
      this.cbUseColumns.Name = "cbUseColumns";
      this.cbUseColumns.Size = new System.Drawing.Size(127, 17);
      this.cbUseColumns.TabIndex = 7;
      this.cbUseColumns.Text = "Use columns in tree";
      this.cbUseColumns.UseVisualStyleBackColor = true;
      // 
      // lvEditors
      // 
      this.lvEditors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription});
      this.lvEditors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvEditors.HideSelection = false;
      this.lvEditors.Location = new System.Drawing.Point(15, 76);
      this.lvEditors.MultiSelect = false;
      this.lvEditors.Name = "lvEditors";
      this.lvEditors.Size = new System.Drawing.Size(377, 121);
      this.lvEditors.SmallImageList = this.imageList;
      this.lvEditors.TabIndex = 8;
      this.lvEditors.UseCompatibleStateImageBehavior = false;
      this.lvEditors.View = System.Windows.Forms.View.Details;
      this.lvEditors.SelectedIndexChanged += new System.EventHandler(this.lvEditors_SelectedIndexChanged);
      this.lvEditors.Resize += new System.EventHandler(this.lvEditors_Resize);
      // 
      // imageList
      // 
      this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imageList.ImageSize = new System.Drawing.Size(32, 32);
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // lblDoubleClick
      // 
      this.lblDoubleClick.AutoSize = true;
      this.lblDoubleClick.Location = new System.Drawing.Point(12, 380);
      this.lblDoubleClick.Name = "lblDoubleClick";
      this.lblDoubleClick.Size = new System.Drawing.Size(109, 13);
      this.lblDoubleClick.TabIndex = 9;
      this.lblDoubleClick.Text = "Double-click action:";
      // 
      // cbDoubleClick
      // 
      this.cbDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDoubleClick.FormattingEnabled = true;
      this.cbDoubleClick.Location = new System.Drawing.Point(127, 377);
      this.cbDoubleClick.Name = "cbDoubleClick";
      this.cbDoubleClick.Size = new System.Drawing.Size(265, 21);
      this.cbDoubleClick.TabIndex = 10;
      // 
      // SettingsForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(404, 453);
      this.ControlBox = false;
      this.Controls.Add(this.cbDoubleClick);
      this.Controls.Add(this.lblDoubleClick);
      this.Controls.Add(this.lvEditors);
      this.Controls.Add(this.cbUseColumns);
      this.Controls.Add(this.cbGroupByNamespaces);
      this.Controls.Add(this.cbGroupByModules);
      this.Controls.Add(this.cbSimplifyNullable);
      this.Controls.Add(this.cbApplyVsToProject);
      this.Controls.Add(this.cbSimplifySystemNames);
      this.Controls.Add(this.lblVS);
      this.Controls.Add(this.cbShowUnicode);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.FillColor = System.Drawing.Color.RoyalBlue;
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HeaderColor = System.Drawing.Color.White;
      this.HeaderPosition = new System.Drawing.Point(55, 7);
      this.HeaderText = "Settings";
      this.Name = "SettingsForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Obfuscar Mapping Parser Settings";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsForm_Paint);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label lblVS;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox cbSimplifySystemNames;
    private System.Windows.Forms.CheckBox cbShowUnicode;
    private System.Windows.Forms.CheckBox cbApplyVsToProject;
    private System.Windows.Forms.CheckBox cbSimplifyNullable;
    private System.Windows.Forms.CheckBox cbGroupByModules;
    private System.Windows.Forms.CheckBox cbGroupByNamespaces;
    private System.Windows.Forms.CheckBox cbUseColumns;
    private System.Windows.Forms.ListView lvEditors;
    private System.Windows.Forms.ColumnHeader chDescription;
    private System.Windows.Forms.ImageList imageList;
    private System.Windows.Forms.Label lblDoubleClick;
    private System.Windows.Forms.ComboBox cbDoubleClick;
  }
}