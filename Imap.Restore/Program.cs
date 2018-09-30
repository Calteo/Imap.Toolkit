using CommandLine;
using Imap.Toolkit.Core;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Restore
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<RestoreOptions>(args)
                                .MapResult(
                                    (RestoreOptions opts) => RunWith(opts),
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

        private const string MessageId = "Message-ID";

        private static int RunWith(RestoreOptions opts)
        {
            var storage = new ProfileStorage();

            foreach (var profile in storage.GetProfiles(opts))
            {
                Console.Write($"{profile.Name} - {profile.UserName}");
                if (opts.SourceProfile != "")
                    Console.WriteLine($" from {opts.SourceProfile}");
                else
                    Console.WriteLine();

                var profileFolder = opts.SourceProfile == "" ? profile.Name : opts.SourceProfile;
                var backupFolder = Path.Combine(opts.BackupFolder, profileFolder);

                var filters = opts.MessageBoxes.Select(m => m.Split("=".ToCharArray(), 2)).ToDictionary(p => p[0], p => p.Length == 1 ? p[0] : p[1]);

                using (var client = profile.CreateClient())
                {
                    foreach (var directory in Directory.EnumerateDirectories(backupFolder, "*", SearchOption.AllDirectories))
                    {
                        var foldername = directory.Substring(backupFolder.Length+1).Replace('\\','/');

                        var targetFoldername = foldername;

                        if (filters.Count == 0 || filters.TryGetValue(foldername, out targetFoldername))
                        {
                            Console.WriteLine($"restore {foldername} to {targetFoldername}");

                            var folder = GetFolder(client.GetFolder(client.PersonalNamespaces[0]), new Queue<string>(targetFoldername.Split('/')));


                            var filenames = Directory.GetFiles(directory, "*.eml");

                            folder.Open(FolderAccess.ReadWrite);
                            Console.WriteLine($"{folder.FullName} - {filenames.Length} messages");

                            var count = 0;
                            var added = 0;
                            var ids = new HashSet<string>();

                            foreach (var filename in filenames)
                            {
                                var sourceMessage = MimeMessage.Load(filename);
                                if (!sourceMessage.Headers.Contains(MessageId) && opts.CreateId)
                                {
                                    var id = $"{Guid.NewGuid()}@id-made@home";
                                    sourceMessage.Headers.Add(MessageId, id);
                                    sourceMessage.WriteTo(filename);
                                }

                                if (sourceMessage.Headers.Contains(MessageId))
                                {
                                    if (ids.Contains(sourceMessage.Headers[MessageId]))
                                    {
                                        if (opts.CreateId)
                                        {
                                            var id = $"{Guid.NewGuid()}@id-made@home";
                                            sourceMessage.Headers[MessageId] = id;
                                            sourceMessage.WriteTo(filename);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"message {filename} has already processed {MessageId}");
                                        }
                                    }

                                    ids.Add(sourceMessage.Headers[MessageId]);
                                    var targets = folder.Search(SearchQuery.HeaderContains(MessageId, sourceMessage.Headers[MessageId]));
                                    if (targets.Count == 0)
                                    {
                                        folder.Append(sourceMessage);
                                        added++;
                                    }
                                    count++;
                                    if (opts.Progress==1 || (count % opts.Progress) == 0)
                                        Console.Write($"... {count}/{filenames.Length} ...\r");
                                }
                                else
                                {                                    
                                    Console.WriteLine($"message {filename} has no {MessageId}");
                                }
                            }
                            Console.WriteLine($"{added} messages uploaded            ");
                            folder.Close();
                        }
                    }
                }
            }
            return 0;
        }

        private static IMailFolder GetFolder(IMailFolder parent, Queue<string> names)
        {
            var name = names.Dequeue();

            var folder = parent.GetSubfolders().FirstOrDefault(f => f.Name == name);
            if (folder == null)
                folder = parent.Create(name, true);

            if (names.Count == 0)
                return folder;
            else
                return GetFolder(folder, names);
        }

    }    
}
