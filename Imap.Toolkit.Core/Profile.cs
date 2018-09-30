using MailKit.Net.Imap;
using System.Runtime.Serialization;

namespace Imap.Toolkit.Core
{
    /// <summary>
    /// Imap Profile
    /// </summary>
    [DataContract(Namespace = ObjectSerializer.Namespace)]
    public class Profile
    {
        public string Name { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
        
        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public bool Ssl { get; set; }

        public ImapClient CreateClient()
        {
            var client = new ImapClient();
            
            // accept all SSL certificates
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect(HostName, Port, Ssl);
            client.Authenticate(UserName, Password);

            return client;
        }
    }
}
