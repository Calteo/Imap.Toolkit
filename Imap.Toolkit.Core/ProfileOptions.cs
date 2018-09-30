using CommandLine;
using System.Text.RegularExpressions;

namespace Imap.Toolkit.Core
{
    public class ProfileOptions
    {
        private string _userName;
        private string _name;

        [Option('n', "name", HelpText = "name or pattern for names", Default = "*")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NamePattern = CreatePattern(_name);
            }
        }

        [Option('u', "username", HelpText = "username or pattern for usernames", Default = "*")]
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                UserNamePattern = CreatePattern(_userName);
            }
        }

        public Regex NamePattern { get; private set; }
        public Regex UserNamePattern { get; private set; }

        private Regex CreatePattern(string pattern)
        {
            return new Regex("^" + pattern.Replace(".", @"\.").Replace("*", ".*").Replace("?", ".") + "$");
        }

    }
}
