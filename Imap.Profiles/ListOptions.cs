using CommandLine;
using Imap.Toolkit.Core;

namespace Imap.Profiles
{
    [Verb("list", HelpText = "List the available profiles.")]
    class ListOptions : ProfileOptions
    {
        [Option('k', "password", HelpText = "include passwords in output")]
        public bool Passwords { get; set; }

    }
}
