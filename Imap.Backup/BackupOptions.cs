using CommandLine;
using Imap.Toolkit.Core;

namespace Imap.Backup
{
    class BackupOptions : ProfileOptions
    {
        [Option('b', "backup", HelpText = "root folder for backups", Default = "Backup")]
        public string BackupFolder { get; set; }
    }
}
