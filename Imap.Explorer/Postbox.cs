using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Imap.Toolkit.Core;
using MailKit.Net.Imap;

namespace Imap.Explorer
{
    class PostBox
    {
        public PostBox(Profile profile)
        {
            Profile = profile;
        }        

        public Profile Profile { get; set; }

        public string Text => $"{Profile.Name} ({Profile.HostName})";

        private ImapClient _imapClient;
        private List<Folder> _folders;

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
