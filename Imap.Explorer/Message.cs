using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Explorer
{
    class Message
    {
        public Message( UniqueId uid, HeaderList headers)
        {
            Uid = uid;
            From = headers[HeaderId.From];
            Subject = headers[HeaderId.Subject];
            if (DateTime.TryParse(headers[HeaderId.Date], out DateTime parsedDate))
                Date = parsedDate;
            else
                Date = DateTime.MinValue;
        }
        public UniqueId Uid { get; }
        public string From { get; }
        public string Subject { get; }
        public DateTime Date { get; }
    }
}
