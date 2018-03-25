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
      BrokenEvent.Shared.Controls.BrokenListColumn brokenListColumn1 = new BrokenEvent.Shared.Controls.BrokenListColumn();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
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
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.lblDoubleClick = new System.Windows.Forms.Label();
      this.cbDoubleClick = new System.Windows.Forms.ComboBox();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.tbEditor = new System.Windows.Forms.TabPage();
      this.blvEditors = new BrokenEvent.Shared.Controls.BrokenListView();
      this.tpSettings = new System.Windows.Forms.TabPage();
      this.lblHeaderOther = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.lblHeaderGrouping = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.lblHeaderVisual = new BrokenEvent.Shared.Controls.ThemedLabel();
      this.cbSimplifyRef = new System.Windows.Forms.CheckBox();
      this.cbWatchClipboard = new System.Windows.Forms.CheckBox();
      this.cbUpdateInterval = new System.Windows.Forms.ComboBox();
      this.lblUpdate = new System.Windows.Forms.Label();
      this.tpHotkeys = new System.Windows.Forms.TabPage();
      this.commandSelector = new BrokenEvent.Shared.CommandManager.CommandSelector();
      this.tabControl.SuspendLayout();
      this.tbEditor.SuspendLayout();
      this.tpSettings.SuspendLayout();
      this.tpHotkeys.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblVS
      // 
      this.lblVS.AutoSize = true;
      this.lblVS.Location = new System.Drawing.Point(6, 6);
      this.lblVS.Margin = new System.Windows.Forms.Padding(3);
      this.lblVS.Name = "lblVS";
      this.lblVS.Size = new System.Drawing.Size(186, 13);
      this.lblVS.TabIndex = 0;
      this.lblVS.Text = "Select external editor to open files:";
      // 
      // cbSimplifySystemNames
      // 
      this.cbSimplifySystemNames.AutoSize = true;
      this.cbSimplifySystemNames.Location = new System.Drawing.Point(9, 68);
      this.cbSimplifySystemNames.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbSimplifySystemNames.Name = "cbSimplifySystemNames";
      this.cbSimplifySystemNames.Size = new System.Drawing.Size(241, 17);
      this.cbSimplifySystemNames.TabIndex = 1;
      this.cbSimplifySystemNames.Text = "Simplify system names (System.Int32 → int)";
      this.cbSimplifySystemNames.UseVisualStyleBackColor = true;
      // 
      // cbShowUnicode
      // 
      this.cbShowUnicode.AutoSize = true;
      this.cbShowUnicode.Location = new System.Drawing.Point(9, 39);
      this.cbShowUnicode.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbShowUnicode.Name = "cbShowUnicode";
      this.cbShowUnicode.Size = new System.Drawing.Size(170, 17);
      this.cbShowUnicode.TabIndex = 0;
      this.cbShowUnicode.Text = "Show Unicode symbols as is";
      this.cbShowUnicode.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(351, 501);
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
      this.btnCancel.Location = new System.Drawing.Point(457, 501);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cbApplyVsToProject
      // 
      this.cbApplyVsToProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbApplyVsToProject.AutoSize = true;
      this.cbApplyVsToProject.Location = new System.Drawing.Point(8, 367);
      this.cbApplyVsToProject.Name = "cbApplyVsToProject";
      this.cbApplyVsToProject.Size = new System.Drawing.Size(370, 17);
      this.cbApplyVsToProject.TabIndex = 3;
      this.cbApplyVsToProject.Text = "Apply to current project (changes default setting when unchecked)";
      this.cbApplyVsToProject.UseVisualStyleBackColor = true;
      // 
      // cbSimplifyNullable
      // 
      this.cbSimplifyNullable.AutoSize = true;
      this.cbSimplifyNullable.Location = new System.Drawing.Point(9, 97);
      this.cbSimplifyNullable.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbSimplifyNullable.Name = "cbSimplifyNullable";
      this.cbSimplifyNullable.Size = new System.Drawing.Size(213, 17);
      this.cbSimplifyNullable.TabIndex = 4;
      this.cbSimplifyNullable.Text = "Simplify nullable (Nullable<A> → A?)";
      this.cbSimplifyNullable.UseVisualStyleBackColor = true;
      // 
      // cbGroupByModules
      // 
      this.cbGroupByModules.AutoSize = true;
      this.cbGroupByModules.Location = new System.Drawing.Point(9, 214);
      this.cbGroupByModules.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbGroupByModules.Name = "cbGroupByModules";
      this.cbGroupByModules.Size = new System.Drawing.Size(121, 17);
      this.cbGroupByModules.TabIndex = 5;
      this.cbGroupByModules.Text = "Group by modules";
      this.cbGroupByModules.UseVisualStyleBackColor = true;
      // 
      // cbGroupByNamespaces
      // 
      this.cbGroupByNamespaces.AutoSize = true;
      this.cbGroupByNamespaces.Location = new System.Drawing.Point(9, 185);
      this.cbGroupByNamespaces.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbGroupByNamespaces.Name = "cbGroupByNamespaces";
      this.cbGroupByNamespaces.Size = new System.Drawing.Size(182, 17);
      this.cbGroupByNamespaces.TabIndex = 6;
      this.cbGroupByNamespaces.Text = "Group by original namespaces";
      this.cbGroupByNamespaces.UseVisualStyleBackColor = true;
      // 
      // cbUseColumns
      // 
      this.cbUseColumns.AutoSize = true;
      this.cbUseColumns.Location = new System.Drawing.Point(9, 243);
      this.cbUseColumns.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbUseColumns.Name = "cbUseColumns";
      this.cbUseColumns.Size = new System.Drawing.Size(127, 17);
      this.cbUseColumns.TabIndex = 7;
      this.cbUseColumns.Text = "Use columns in tree";
      this.cbUseColumns.UseVisualStyleBackColor = true;
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
      this.lblDoubleClick.Location = new System.Drawing.Point(6, 358);
      this.lblDoubleClick.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.lblDoubleClick.Name = "lblDoubleClick";
      this.lblDoubleClick.Size = new System.Drawing.Size(109, 13);
      this.lblDoubleClick.TabIndex = 9;
      this.lblDoubleClick.Text = "Double-click action:";
      // 
      // cbDoubleClick
      // 
      this.cbDoubleClick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDoubleClick.FormattingEnabled = true;
      this.cbDoubleClick.Location = new System.Drawing.Point(220, 355);
      this.cbDoubleClick.Name = "cbDoubleClick";
      this.cbDoubleClick.Size = new System.Drawing.Size(311, 21);
      this.cbDoubleClick.TabIndex = 10;
      // 
      // tabControl
      // 
      this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl.Controls.Add(this.tbEditor);
      this.tabControl.Controls.Add(this.tpSettings);
      this.tabControl.Controls.Add(this.tpHotkeys);
      this.tabControl.Location = new System.Drawing.Point(12, 66);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(545, 416);
      this.tabControl.TabIndex = 11;
      // 
      // tbEditor
      // 
      this.tbEditor.Controls.Add(this.blvEditors);
      this.tbEditor.Controls.Add(this.lblVS);
      this.tbEditor.Controls.Add(this.cbApplyVsToProject);
      this.tbEditor.Location = new System.Drawing.Point(4, 22);
      this.tbEditor.Name = "tbEditor";
      this.tbEditor.Padding = new System.Windows.Forms.Padding(3);
      this.tbEditor.Size = new System.Drawing.Size(537, 390);
      this.tbEditor.TabIndex = 0;
      this.tbEditor.Text = "External Editor";
      this.tbEditor.UseVisualStyleBackColor = true;
      // 
      // blvEditors
      // 
      this.blvEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.blvEditors.AutoScroll = true;
      this.blvEditors.AutoScrollMinSize = new System.Drawing.Size(521, 0);
      this.blvEditors.BackColor = System.Drawing.SystemColors.Window;
      this.blvEditors.ColumnHeaderStyle = BrokenEvent.Shared.Controls.ColumnHeaderStyle.None;
      brokenListColumn1.Autosize = true;
      brokenListColumn1.HeaderAlignment = ((BrokenEvent.Shared.Controls.ItemTextAlignment)((BrokenEvent.Shared.Controls.ItemTextAlignment.HorizontalLeft | BrokenEvent.Shared.Controls.ItemTextAlignment.VerticalTop)));
      brokenListColumn1.HeaderText = null;
      brokenListColumn1.TextAlignment = ((BrokenEvent.Shared.Controls.ItemTextAlignment)((BrokenEvent.Shared.Controls.ItemTextAlignment.HorizontalLeft | BrokenEvent.Shared.Controls.ItemTextAlignment.VerticalTop)));
      brokenListColumn1.Width = 521;
      this.blvEditors.Columns.Add(brokenListColumn1);
      this.blvEditors.DisabledColor = System.Drawing.Color.Empty;
      this.blvEditors.DrawBorder = false;
      this.blvEditors.DropHoverIndicatorColor = System.Drawing.Color.CornflowerBlue;
      this.blvEditors.EmptyListText = "No editors found.";
      this.blvEditors.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.blvEditors.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.blvEditors.IconPadding = new System.Windows.Forms.Padding(2);
      this.blvEditors.ImageList = this.imageList;
      this.blvEditors.Location = new System.Drawing.Point(7, 25);
      this.blvEditors.Name = "blvEditors";
      this.blvEditors.SelectMode = BrokenEvent.Shared.Controls.BrokenListView.SelectionMode.AlwaysSelected;
      this.blvEditors.Size = new System.Drawing.Size(523, 336);
      this.blvEditors.TabIndex = 4;
      this.blvEditors.Text = "brokenListView1";
      // 
      // tpSettings
      // 
      this.tpSettings.Controls.Add(this.lblHeaderOther);
      this.tpSettings.Controls.Add(this.lblHeaderGrouping);
      this.tpSettings.Controls.Add(this.lblHeaderVisual);
      this.tpSettings.Controls.Add(this.cbSimplifyRef);
      this.tpSettings.Controls.Add(this.cbWatchClipboard);
      this.tpSettings.Controls.Add(this.cbUpdateInterval);
      this.tpSettings.Controls.Add(this.lblUpdate);
      this.tpSettings.Controls.Add(this.cbSimplifyNullable);
      this.tpSettings.Controls.Add(this.cbDoubleClick);
      this.tpSettings.Controls.Add(this.cbShowUnicode);
      this.tpSettings.Controls.Add(this.lblDoubleClick);
      this.tpSettings.Controls.Add(this.cbSimplifySystemNames);
      this.tpSettings.Controls.Add(this.cbUseColumns);
      this.tpSettings.Controls.Add(this.cbGroupByModules);
      this.tpSettings.Controls.Add(this.cbGroupByNamespaces);
      this.tpSettings.Location = new System.Drawing.Point(4, 22);
      this.tpSettings.Name = "tpSettings";
      this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
      this.tpSettings.Size = new System.Drawing.Size(537, 390);
      this.tpSettings.TabIndex = 1;
      this.tpSettings.Text = "UI Settings";
      this.tpSettings.UseVisualStyleBackColor = true;
      // 
      // lblHeaderOther
      // 
      this.lblHeaderOther.AutoSizeLabel = true;
      this.lblHeaderOther.BackColor = System.Drawing.Color.Transparent;
      this.lblHeaderOther.CharacterWrap = false;
      this.lblHeaderOther.Location = new System.Drawing.Point(9, 272);
      this.lblHeaderOther.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
      this.lblHeaderOther.Name = "lblHeaderOther";
      this.lblHeaderOther.Size = new System.Drawing.Size(100, 21);
      this.lblHeaderOther.Style = BrokenEvent.Shared.Controls.ThemedLabel.ThemeStyle.ControlPanelSubtitle;
      this.lblHeaderOther.TabIndex = 17;
      this.lblHeaderOther.Text = "Other Settings";
      // 
      // lblHeaderGrouping
      // 
      this.lblHeaderGrouping.AutoSizeLabel = true;
      this.lblHeaderGrouping.BackColor = System.Drawing.Color.Transparent;
      this.lblHeaderGrouping.CharacterWrap = false;
      this.lblHeaderGrouping.Location = new System.Drawing.Point(9, 155);
      this.lblHeaderGrouping.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
      this.lblHeaderGrouping.Name = "lblHeaderGrouping";
      this.lblHeaderGrouping.Size = new System.Drawing.Size(100, 21);
      this.lblHeaderGrouping.Style = BrokenEvent.Shared.Controls.ThemedLabel.ThemeStyle.ControlPanelSubtitle;
      this.lblHeaderGrouping.TabIndex = 16;
      this.lblHeaderGrouping.Text = "Tree Grouping";
      // 
      // lblHeaderVisual
      // 
      this.lblHeaderVisual.AutoSizeLabel = true;
      this.lblHeaderVisual.BackColor = System.Drawing.Color.Transparent;
      this.lblHeaderVisual.CharacterWrap = false;
      this.lblHeaderVisual.Location = new System.Drawing.Point(9, 9);
      this.lblHeaderVisual.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
      this.lblHeaderVisual.Name = "lblHeaderVisual";
      this.lblHeaderVisual.Size = new System.Drawing.Size(76, 21);
      this.lblHeaderVisual.Style = BrokenEvent.Shared.Controls.ThemedLabel.ThemeStyle.ControlPanelSubtitle;
      this.lblHeaderVisual.TabIndex = 15;
      this.lblHeaderVisual.Text = "Tree Visual";
      // 
      // cbSimplifyRef
      // 
      this.cbSimplifyRef.AutoSize = true;
      this.cbSimplifyRef.Location = new System.Drawing.Point(9, 126);
      this.cbSimplifyRef.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbSimplifyRef.Name = "cbSimplifyRef";
      this.cbSimplifyRef.Size = new System.Drawing.Size(204, 17);
      this.cbSimplifyRef.TabIndex = 14;
      this.cbSimplifyRef.Text = "Simplify ref args (int& a → ref int a)";
      this.cbSimplifyRef.UseMnemonic = false;
      this.cbSimplifyRef.UseVisualStyleBackColor = true;
      // 
      // cbWatchClipboard
      // 
      this.cbWatchClipboard.AutoSize = true;
      this.cbWatchClipboard.Location = new System.Drawing.Point(9, 302);
      this.cbWatchClipboard.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
      this.cbWatchClipboard.Name = "cbWatchClipboard";
      this.cbWatchClipboard.Size = new System.Drawing.Size(346, 17);
      this.cbWatchClipboard.TabIndex = 13;
      this.cbWatchClipboard.Text = "Watch clipboard and open Stacktrace Processor (experimental)";
      this.cbWatchClipboard.UseVisualStyleBackColor = true;
      // 
      // cbUpdateInterval
      // 
      this.cbUpdateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUpdateInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbUpdateInterval.FormattingEnabled = true;
      this.cbUpdateInterval.Location = new System.Drawing.Point(220, 328);
      this.cbUpdateInterval.Name = "cbUpdateInterval";
      this.cbUpdateInterval.Size = new System.Drawing.Size(311, 21);
      this.cbUpdateInterval.TabIndex = 12;
      // 
      // lblUpdate
      // 
      this.lblUpdate.AutoSize = true;
      this.lblUpdate.Location = new System.Drawing.Point(6, 331);
      this.lblUpdate.Name = "lblUpdate";
      this.lblUpdate.Size = new System.Drawing.Size(104, 13);
      this.lblUpdate.TabIndex = 11;
      this.lblUpdate.Text = "Check for updates:";
      // 
      // tpHotkeys
      // 
      this.tpHotkeys.Controls.Add(this.commandSelector);
      this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
      this.tpHotkeys.Name = "tpHotkeys";
      this.tpHotkeys.Padding = new System.Windows.Forms.Padding(3);
      this.tpHotkeys.Size = new System.Drawing.Size(537, 390);
      this.tpHotkeys.TabIndex = 2;
      this.tpHotkeys.Text = "Hotkeys";
      this.tpHotkeys.UseVisualStyleBackColor = true;
      // 
      // commandSelector
      // 
      this.commandSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.commandSelector.AutoScroll = true;
      this.commandSelector.AutoScrollMinSize = new System.Drawing.Size(525, 0);
      this.commandSelector.BackColor = System.Drawing.SystemColors.Window;
      this.commandSelector.CategoryFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.commandSelector.CategoryPadding = new System.Windows.Forms.Padding(4);
      this.commandSelector.CommandManager = null;
      this.commandSelector.CommandType = null;
      this.commandSelector.LineHeight = 21;
      this.commandSelector.Location = new System.Drawing.Point(6, 6);
      this.commandSelector.Name = "commandSelector";
      this.commandSelector.NamePercent = 0.5F;
      this.commandSelector.Size = new System.Drawing.Size(525, 355);
      this.commandSelector.TabIndex = 0;
      this.commandSelector.Text = "commandSelector1";
      this.commandSelector.UseThemes = true;
      // 
      // SettingsForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(569, 533);
      this.ControlBox = false;
      this.Controls.Add(this.tabControl);
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
      this.tabControl.ResumeLayout(false);
      this.tbEditor.ResumeLayout(false);
      this.tbEditor.PerformLayout();
      this.tpSettings.ResumeLayout(false);
      this.tpSettings.PerformLayout();
      this.tpHotkeys.ResumeLayout(false);
      this.ResumeLayout(false);

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
    private System.Windows.Forms.ImageList imageList;
    private System.Windows.Forms.Label lblDoubleClick;
    private System.Windows.Forms.ComboBox cbDoubleClick;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tbEditor;
    private System.Windows.Forms.TabPage tpSettings;
    private System.Windows.Forms.TabPage tpHotkeys;
    private BrokenEvent.Shared.CommandManager.CommandSelector commandSelector;
    private System.Windows.Forms.ComboBox cbUpdateInterval;
    private System.Windows.Forms.Label lblUpdate;
    private System.Windows.Forms.CheckBox cbWatchClipboard;
    private BrokenEvent.Shared.Controls.BrokenListView blvEditors;
    private BrokenEvent.Shared.Controls.ThemedLabel lblHeaderOther;
    private BrokenEvent.Shared.Controls.ThemedLabel lblHeaderGrouping;
    private BrokenEvent.Shared.Controls.ThemedLabel lblHeaderVisual;
    private System.Windows.Forms.CheckBox cbSimplifyRef;
  }
}