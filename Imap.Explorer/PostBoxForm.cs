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
    internal partial class PostBoxForm : Form
    {
        public PostBoxForm()
        {
            InitializeComponent();
        }

        public PostBox PostBox { get; set; }
        public ProfileStorage Storage { get; set; }

        private void PostBoxForm_Load(object sender, EventArgs e)
        {
            var profile = PostBox.Profile;

            textBoxName.Text = profile.Name;
            textBoxName_TextChanged(textBoxName, EventArgs.Empty);

            textBoxUserName.Text = profile.UserName;
            textBoxUserName_TextChanged(textBoxUserName, EventArgs.Empty);

            textBoxPassword.Text = profile.Password;
            textBoxPassword_TextChanged(textBoxPassword, EventArgs.Empty);

            textBoxHostName.Text = profile.HostName;
            textBoxHostName_TextChanged(textBoxHostName, EventArgs.Empty);

            numericUpDownPort.Value = profile.Port;

            checkBoxSll.Checked = profile.Ssl;
        }

        public HashSet<Control> ErrorControls { get; } = new HashSet<Control>();

        private void SetError(Control control, string text)
        {
            ErrorControls.Add(control);
            errorProvider.SetError(control, text);

            buttonTest.Enabled = buttonOk.Enabled = false;
        }

        private void ClearError(Control control)
        {
            ErrorControls.Remove(control);
            errorProvider.SetError(control, "");

            buttonTest.Enabled = buttonOk.Enabled = ErrorControls.Count == 0;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            var text = textBoxName.Text;
            if (text == "")
                SetError(textBoxName, "Name is required.");
            else
                ClearError(textBoxName);
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            var text = textBoxUserName.Text;
            if (text == "")
                SetError(textBoxUserName, "User name is required.");
            else
                ClearError(textBoxUserName);
        }

        private void checkBoxSll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSll.Checked)
            {
                if (numericUpDownPort.Value == Profile.DefaultPort)
                    numericUpDownPort.Value = Profile.DefaultPortSll;
            }
            else
            {
                if (numericUpDownPort.Value == Profile.DefaultPortSll)
                    numericUpDownPort.Value = Profile.DefaultPort;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            var text = textBoxPassword.Text;
            if (text == "")
                SetError(textBoxPassword, "Password is required.");
            else
                ClearError(textBoxPassword);
        }

        private void textBoxHostName_TextChanged(object sender, EventArgs e)
        {
            var text = textBoxHostName.Text;
            if (text == "")
                SetError(textBoxHostName, "Host name is required.");
            else
                ClearError(textBoxHostName);
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;
        }

        private void ButtonTestClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var profile = new Profile
                {
                    HostName = textBoxHostName.Text,
                    UserName = textBoxUserName.Text,
                    Password = textBoxPassword.Text,
                    Port = (int)numericUpDownPort.Value,
                    Ssl = checkBoxSll.Checked
                };

                using (var client = profile.CreateClient())
                {
                    client.Disconnect(true);
                }
                MessageBox.Show(this, "Connection successful.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ButtonOkClick(object sender, EventArgs e)
        {
            var profile = PostBox.Profile;

            if (profile.Name!="" && profile.Name!=textBoxName.Text)
                Storage.Remove(profile.Name);

            profile.Name = textBoxName.Text;
            profile.UserName = textBoxUserName.Text;
            profile.HostName = textBoxHostName.Text;
            profile.Password = textBoxPassword.Text;
            profile.Port = (int)numericUpDownPort.Value;
            profile.Ssl = checkBoxSll.Checked;

            Storage.Store(profile);

            PostBox.ImapClient = null;
        }
    }
}
