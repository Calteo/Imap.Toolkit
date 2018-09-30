using CommandLine;
using Imap.Toolkit.Core;
using System.Collections.Generic;

namespace Imap.Restore
{
    class RestoreOptions : ProfileOptions
    {
        [Option('b', "backup", HelpText = "root folder for backups", Default = "Backup")]
        public string BackupFolder { get; set; }

        [Option('m', "messageboxes", HelpText = "messagebox to restore, separted by ':', mapping to a different messagebox by adding '=target'", Separator = ':')]
        public IEnumerable<string> MessageBoxes { get; set; }

        [Option('s', "source", HelpText = "name of alternate source profile", Default = "")]
        public string SourceProfile { get; set; }

        [Option('c', "create-id", HelpText = "create Message-ID if it is missing", Default = false)]
        public bool CreateId{ get; set; }

        [Option('p', "progress", HelpText = "report progress every n-th message", Default = 10)]
        public int Progress { get; set; }
        
    }
}
