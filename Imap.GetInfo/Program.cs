using CommandLine;
using Imap.Toolkit.Core;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.GetInfo
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<InfoOptions>(args)
                                .MapResult(
                                    (InfoOptions opts) => RunWith(opts),
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

        private static int RunWith(InfoOptions opts)
        {
            if (opts.Messages)
                RunWithMessages(opts);
            else
                RunWithNotAll(opts);
            return 0;
        }

        private static void RunWithMessages(InfoOptions opts)
        {
            var storage = new ProfileStorage();

            bool needLine = false;

            foreach (var profile in storage.GetProfiles(opts))
            {
                using (var client = profile.CreateClient())
                {
                    var output = new OutputTable();
                    output
                        .AddColumn(profile.Name)
                        .AddColumn("messages", "#,##0", OutputAlignment.Right)
                        .AddColumn("unread", "#,##0", OutputAlignment.Right);

                    var messages = 0;
                    var unread = 0;

                    foreach (var ns in client.PersonalNamespaces)
                    {                                              
                        foreach (var folder in client.GetFolders(ns).Where(f=>f.ParentFolder.ParentFolder == null))
                        {
                            OutputFolder(output, folder, ref messages, ref unread);
                        }
                    }

                    output.AddSeparator();
                    output.AddRow(profile.UserName, messages, unread);

                    if (needLine)
                        Console.WriteLine();
                    else
                        needLine = true;

                    output.WriteTo(Console.Out);
                }
            }
        }

        private static void OutputFolder(OutputTable output, IMailFolder folder, ref int messages, ref int unread, int indent = 0)
        {
            var a = folder.Open(FolderAccess.ReadOnly);

            output.AddRow("".PadRight(indent) + folder.Name, folder.Count, folder.Unread);
            var q = folder.GetQuota();

            messages += folder.Count;
            unread += folder.Unread;

            folder.Close();

            foreach (var subFolder in folder.GetSubfolders())
            {
                OutputFolder(output, subFolder, ref messages, ref unread, indent + 1);
            }
        }

        private static void RunWithNotAll(InfoOptions opts)
        {
            var storage = new ProfileStorage();

            var output = new OutputTable();
            output
                .AddColumns("name", "username")
                .AddColumn("messages", "#,##0", OutputAlignment.Right)
                .AddColumn("limit", "#,##0", OutputAlignment.Right)
                .AddColumn("storage", "#,##0", OutputAlignment.Right)
                .AddColumn("limit", "#,##0", OutputAlignment.Right);

            foreach (var profile in storage.GetProfiles(opts))
            {
                using (var client = profile.CreateClient())
                {
                    var quota = client.Inbox.GetQuota();
                    output.AddRow(profile.Name, profile.UserName, quota.CurrentMessageCount, quota.MessageLimit, quota.CurrentStorageSize, quota.StorageLimit);
                }
            }
            output.WriteTo(Console.Out);
        }
    }
}
