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
    internal partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
        }

        public Profile Profile { get; set; }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            textBoxName.Text = Profile.Name;
            textBoxName_TextChanged(textBoxName, EventArgs.Empty);

            textBoxUserName.Text = Profile.UserName;
            textBoxUserName_TextChanged(textBoxUserName, EventArgs.Empty);

            textBoxPassword.Text = Profile.Password;
            textBoxPassword_TextChanged(textBoxPassword, EventArgs.Empty);

            textBoxHostName.Text = Profile.HostName;
            textBoxHostName_TextChanged(textBoxHostName, EventArgs.Empty);

            numericUpDownPort.Value = Profile.Port;

            checkBoxSll.Checked = Profile.Ssl;
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

        private void buttonTest_Click(object sender, EventArgs e)
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Profile.Name = textBoxName.Text;
            Profile.UserName = textBoxUserName.Text;
            Profile.HostName = textBoxHostName.Text;
            Profile.Password = textBoxPassword.Text;
            Profile.Port = (int)numericUpDownPort.Value;
            Profile.Ssl = checkBoxSll.Checked;
        }
    }
}
