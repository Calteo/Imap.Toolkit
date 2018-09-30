using CommandLine;
using Imap.Toolkit.Core;
using MailKit;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.IO;

namespace Imap.Backup
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<BackupOptions>(args)
                                .MapResult(
                                    (BackupOptions opts) => RunWith(opts),
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

        private static int RunErrors(IEnumerable<Error> errs)
        {
            return -1;
        }

        private static int RunWith(BackupOptions opts)
        {
            var storage = new ProfileStorage();

            foreach (var profile in storage.GetProfiles(opts))
            {
                Console.WriteLine($"{profile.Name} - {profile.UserName}");
                using (var client = profile.CreateClient())
                {
                    foreach (var ns in client.PersonalNamespaces)
                    {
                        foreach (var folder in client.GetFolders(ns))
                        {
                            folder.Open(FolderAccess.ReadOnly);
                            Console.WriteLine($"{folder.FullName} - {folder.Count} messages");

                            var backupFolder = Path.Combine(opts.BackupFolder, profile.Name, folder.FullName);
                            if (!Directory.Exists(backupFolder))
                                Directory.CreateDirectory(backupFolder);

                            var count = 0;

                            foreach (var id in folder.Search(SearchQuery.All))
                            {
                                count++;
                                Console.Write($"... {count}/{folder.Count} ...\r");

                                var filename = Path.Combine(backupFolder, $"{id}.eml");
                                if (!File.Exists(filename))
                                {
                                    var message = folder.GetMessage(id);
                                    message.WriteTo(filename);
                                }
                            }
                            
                            Console.Write("                                                  \r");
                            folder.Close();
                        }
                    }
                }
            }

            return 0;
        }

    }
}
