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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelProfile = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxProfiles = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
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
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Enabled = false;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(31, 22);
            this.toolStripButtonEdit.Text = "Edit";
            this.toolStripButtonEdit.ToolTipText = "Edit current profile";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 485);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MainForm";
            this.Text = "Imap Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
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
    }
}

