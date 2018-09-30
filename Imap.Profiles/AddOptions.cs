using CommandLine;

namespace Imap.Profiles
{
    [Verb("add", HelpText = "Add a new profile.")]
    class AddOptions
    {
        [Option('n', "name", HelpText = "name of profile", Required = true)]
        public string Name { get; set; }

        [Option('u', "username", HelpText = "user name for login", Required = true)]
        public string UserName{ get; set; }

        [Option('h', "hostname", HelpText = "host name", Required = true)]
        public string HostName { get; set; }

        [Option('p', "port", HelpText = "port (obmitting sets default port)")]
        public int? Port { get; set; }

        [Option('s', "ssl", HelpText = "use ssl")]
        public bool Ssl { get; set; }
        
        [Option('k', "password", HelpText = "password", Required = true)]
        public string Password { get; set; }
    }
}
