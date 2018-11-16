using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Search;

namespace Imap.Explorer
{
    class Folder
    {
        private IMailFolder _imapFolder;

        public Folder(IMailFolder imapFolder)
        {
            _imapFolder = imapFolder;
            _imapFolder.Open(FolderAccess.ReadOnly);
            NumberOfMessages = _imapFolder.Count;            
            _imapFolder.Close();
        }

        public string Name => _imapFolder.Name;
        public int NumberOfMessages { get; private set; }

        public bool CanExpand => _imapFolder.Attributes.HasFlag(FolderAttributes.HasChildren);

        private List<Folder> _childFolders;
        public List<Folder> ChildFolders
        {
            get
            {
                if (CanExpand && _childFolders == null)
                {
                    _childFolders = _imapFolder.GetSubfolders().Select(f => new Folder(f)).ToList();
                }
                return _childFolders;
            }
        }

        internal IEnumerable GetMessages()
        {
            // var hl = _imapFolder.Search(SearchQuery.All).Select(u => _imapFolder.GetHeaders(u));
            return null;
        }
    }
}
