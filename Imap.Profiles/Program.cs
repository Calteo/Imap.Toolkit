using CommandLine;
using Imap.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imap.Profiles
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<AddOptions, ListOptions, RemoveOptions>(args)
                                .MapResult(
                                    (AddOptions opts) => RunWith(opts),
                                    (ListOptions opts) => RunWith(opts),
                                    (RemoveOptions opts) => RunWith(opts),
                                    errs => RunErrors(errs)
                                );

                return result;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"exception: {exception.Message}");
                return -2;
            }
        }

        private static int RunWith(RemoveOptions opts)
        {
            var storage = new ProfileStorage();

            foreach (var profile in storage.GetProfiles(opts))
            {
                storage.Remove(profile.Name);
                Console.WriteLine($"profile {profile.Name} ({profile.UserName}) removed.");
            }

            return 0;
        }

        private static int RunErrors(IEnumerable<Error> errs)
        {
            /*
            foreach (var error in errs)
            {
                Console.WriteLine($"error {error.Tag} {error.StopsProcessing} {error}");
            }
            */
            return -1;
        }

        private static int RunWith(AddOptions opts)
        {
            var profile = new Profile
            {
                Name = opts.Name,
                UserName = opts.UserName,
                Password = opts.Password,
                HostName = opts.HostName,
                Ssl = opts.Ssl               
            };

            if (opts.Port.HasValue)
                profile.Port = opts.Port.Value;
            else if (profile.Ssl)
                profile.Port = Profile.DefaultPortSll;
            else
                profile.Port = Profile.DefaultPortSll;

            using (var client = profile.CreateClient())
            {
                client.Disconnect(true);                
            }

            var storage = new ProfileStorage();
            storage.Store(profile);

            Console.WriteLine($"profile {profile.UserName} stored.");

            return 0;
        }

        private static int RunWith(ListOptions opts)
        {
            var storage = new ProfileStorage();

            var output = new OutputTable();
            output.AddColumns("name", "username", "hostname")
                .AddColumn("port", "0", OutputAlignment.Right)
                .AddColumns("ssl");

            if (opts.Passwords)
                output.AddColumn("password");

            foreach (var profile in storage.GetProfiles(opts)) 
            {
                var row = output.AddRow(profile.Name, profile.UserName, profile.HostName, profile.Port, profile.Ssl);
                if (opts.Passwords)
                    row.Values.Add(profile.Password);
            }

            output.WriteTo(Console.Out);

            return 0;
        }
    }
}
