using Imap.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imap.Explorer
{
    internal partial class MainForm : Form
    {
        private Profile _profile;

        public MainForm()
        {
            InitializeComponent();
        }

        public ProfileStorage Storage { get; set; }

        public List<Profile> Profiles { get; } = new List<Profile>();

        public Profile Profile
        {
            get => _profile;
            set
            {
                _profile = value;
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = _profile != null;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Storage = new ProfileStorage();

            Profiles.AddRange(Storage.GetProfiles().ToArray());

            toolStripComboBoxProfiles.Items.AddRange(Profiles.Select(p => GetText(p)).ToArray());
        }

        private string GetText(Profile p)
        {
            return $"{p.Name} ({p.UserName})";
        }

        private void toolStripComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxProfiles.SelectedIndex >= 0)
                Profile = Profiles.First(p => GetText(p) == (string)toolStripComboBoxProfiles.SelectedItem);
            else
                Profile = null;
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            var oldName = Profile.Name;
            var form = new ProfileForm { Profile = Profile };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Storage.Store(Profile);
                if (oldName != Profile.Name)
                {
                    Storage.Remove(oldName);
                    var index = toolStripComboBoxProfiles.SelectedIndex;
                    var text = GetText(Profile);
                    toolStripComboBoxProfiles.Items.RemoveAt(index);
                    toolStripComboBoxProfiles.Items.Add(GetText(Profile));
                    toolStripComboBoxProfiles.SelectedItem = text;
                }
            }
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            var form = new ProfileForm { Profile = new Profile() };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
            }
        }
    }
}
