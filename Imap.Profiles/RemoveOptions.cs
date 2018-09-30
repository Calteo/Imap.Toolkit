using CommandLine;
using Imap.Toolkit.Core;

namespace Imap.Profiles
{
    [Verb("remove", HelpText = "Remove profile(s).")]
    class RemoveOptions : ProfileOptions
    {
    }
}
