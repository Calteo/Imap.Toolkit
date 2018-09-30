using CommandLine;
using Imap.Toolkit.Core;

namespace Imap.GetInfo
{
    class InfoOptions : ProfileOptions
    {
        [Option('m', "messages", HelpText = "include all messageboxes")]
        public bool Messages { get; set; }
    }
}
