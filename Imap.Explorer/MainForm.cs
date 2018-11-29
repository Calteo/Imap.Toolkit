using Imap.Toolkit.Core;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Imap.Explorer
{
    internal partial class MainForm : Form
    {
        private PostBox _postBox;
        
        public MainForm()
        {
            InitializeComponent();

            toolStripComboBoxProfiles.ComboBox.DataSource = null;
            toolStripComboBoxProfiles.ComboBox.DisplayMember = "Text";
            toolStripComboBoxProfiles.ComboBox.DataSource = Postboxes;
            
            treeListView.CanExpandGetter = o => ((Folder)o).CanExpand;
            treeListView.ChildrenGetter = o => ((Folder)o).ChildFolders;
        }

        public ProfileStorage Storage { get; set; }

        public BindingList<PostBox> Postboxes { get; } = new BindingList<PostBox>();

        public PostBox PostBox
        {
            get => _postBox;
            set
            {
                _postBox = value;
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = _postBox != null;
                OnPostBoxChanged();
            }
        }

        private void OnPostBoxChanged()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                treeListView.Roots = null;
                if (PostBox != null)
                {
                    treeListView.Roots = PostBox.Folders;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SafeExecute(Action action)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                action();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Storage = new ProfileStorage();

            Storage.GetProfiles().ToList().ForEach(p => Postboxes.Add(new PostBox(p)));

            toolStripComboBoxProfiles.SelectedIndex = -1;
        }

        private void toolStripComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxProfiles.SelectedIndex >= 0)
                PostBox = (PostBox)toolStripComboBoxProfiles.SelectedItem;
            else
                PostBox = null;
        }

        private void ToolStripButtonEditClick(object sender, EventArgs e)
        {
            var form = new PostBoxForm { Storage = Storage, PostBox = PostBox };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var ordered = Postboxes.OrderBy(p => p.Text).ToList();
                Postboxes.Clear();
                ordered.ForEach(Postboxes.Add);                                // to reinforce the sorting                                
                
            }
            toolStripComboBoxProfiles.SelectedItem = PostBox;
        }

        private void ToolStripButtonNewClick(object sender, EventArgs e)
        {
            var form = new PostBoxForm { Storage = Storage, PostBox = new PostBox(new Profile()) };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Postboxes.Add(form.PostBox);
            }
        }

        private void treeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SafeExecute(() => 
            { 
                objectListViewMessages.Objects = null;
                var folder = (Folder)treeListView.SelectedObject;            
                objectListViewMessages.Objects = folder?.GetMessages();
            });
        }
    }
}
