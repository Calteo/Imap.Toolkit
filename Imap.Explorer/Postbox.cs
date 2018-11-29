using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Imap.Toolkit.Core;
using MailKit.Net.Imap;

namespace Imap.Explorer
{
    class PostBox : INotifyPropertyChanged
    {
        public PostBox(Profile profile)
        {
            Profile = profile;
        }        

        public Profile Profile { get; set; }

        public string Text => $"{Profile.Name} ({Profile.HostName})";
        public void OnTextChanged()
        {
            OnPropertyChanged(nameof(Text));
        }

        private ImapClient _imapClient;
        private List<Folder> _folders;

        #region
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ImapClient ImapClient
        {
            get
            {
                if (_imapClient == null)
                {
                    _imapClient = Profile.CreateClient();
                }
                return _imapClient;
            }
            set
            {
                if (_imapClient != null)
                    _imapClient.Disconnect(true);

                _imapClient = value;
            }
        }

        public List<Folder> Folders
        {
            get
            {
                if (_folders == null)
                {
                    var imapFolders = ImapClient.GetFolders(ImapClient.PersonalNamespaces[0])
                                        .Where(f => f.ParentFolder.ParentFolder == null);
                    _folders = imapFolders.Select(f => new Folder(f)).OrderBy(f => f.Name).ToList();
                }
                return _folders;
            }                
        }
    }
}
