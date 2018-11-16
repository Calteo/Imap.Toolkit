namespace Imap.Explorer
{
    partial class MainForm
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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelProfile = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxProfiles = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMsg = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objectListViewMessages = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnMessageSubject = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelProfile,
            this.toolStripComboBoxProfiles,
            this.toolStripButtonNew,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(868, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripLabelProfile
            // 
            this.toolStripLabelProfile.Name = "toolStripLabelProfile";
            this.toolStripLabelProfile.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabelProfile.Text = "Profile";
            // 
            // toolStripComboBoxProfiles
            // 
            this.toolStripComboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxProfiles.Name = "toolStripComboBoxProfiles";
            this.toolStripComboBoxProfiles.Size = new System.Drawing.Size(250, 25);
            this.toolStripComboBoxProfiles.Sorted = true;
            this.toolStripComboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxProfiles_SelectedIndexChanged);
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonNew.Text = "New";
            this.toolStripButtonNew.ToolTipText = "Create new profile";
            this.toolStripButtonNew.Click += new System.EventHandler(this.ToolStripButtonNewClick);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Enabled = false;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(31, 22);
            this.toolStripButtonEdit.Text = "Edit";
            this.toolStripButtonEdit.ToolTipText = "Edit current profile";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.ToolStripButtonEditClick);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Enabled = false;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.ToolTipText = "Delete current profile";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.objectListViewMessages);
            this.splitContainer1.Size = new System.Drawing.Size(868, 460);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.olvColumnName);
            this.treeListView.AllColumns.Add(this.olvColumnMsg);
            this.treeListView.CellEditUseWholeCell = false;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnMsg});
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Location = new System.Drawing.Point(0, 0);
            this.treeListView.Name = "treeListView";
            this.treeListView.ShowGroups = false;
            this.treeListView.Size = new System.Drawing.Size(289, 460);
            this.treeListView.TabIndex = 0;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.SelectedIndexChanged += new System.EventHandler(this.treeListView_SelectedIndexChanged);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.Text = "Folder";
            this.olvColumnName.Width = 177;
            // 
            // olvColumnMsg
            // 
            this.olvColumnMsg.AspectName = "NumberOfMessages";
            this.olvColumnMsg.AspectToStringFormat = "{0:#,##0}";
            this.olvColumnMsg.IsEditable = false;
            this.olvColumnMsg.Text = "#Msg";
            this.olvColumnMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnMsg.Width = 64;
            // 
            // objectListViewMessages
            // 
            this.objectListViewMessages.AllColumns.Add(this.olvColumnMessageSubject);
            this.objectListViewMessages.CellEditUseWholeCell = false;
            this.objectListViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnMessageSubject});
            this.objectListViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewMessages.Location = new System.Drawing.Point(0, 0);
            this.objectListViewMessages.Name = "objectListViewMessages";
            this.objectListViewMessages.ShowGroups = false;
            this.objectListViewMessages.Size = new System.Drawing.Size(575, 460);
            this.objectListViewMessages.TabIndex = 0;
            this.objectListViewMessages.UseCompatibleStateImageBehavior = false;
            this.objectListViewMessages.View = System.Windows.Forms.View.Details;
            this.objectListViewMessages.VirtualMode = true;
            // 
            // olvColumnMessageSubject
            // 
            this.olvColumnMessageSubject.AspectName = "Subject";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MainForm";
            this.Text = "Imap Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProfiles;
        private System.Windows.Forms.ToolStripLabel toolStripLabelProfile;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.TreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnMsg;
        private BrightIdeasSoftware.FastObjectListView objectListViewMessages;
        private BrightIdeasSoftware.OLVColumn olvColumnMessageSubject;
    }
}

