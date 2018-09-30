using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Imap.Toolkit.Core
{
    public static class ObjectSerializer
    {
        public const string Namespace = "http://famger.de/imap.toolkit";

        const string phrase = "Some paraphase to create a hash.";

        static ObjectSerializer()
        {
            using (var hash = new SHA384Managed())
            {
                var buffer = hash.ComputeHash(Encoding.Default.GetBytes(phrase));
                Array.Copy(buffer, _iv, _iv.Length);
                Array.Copy(buffer, _iv.Length, _key, 0, _key.Length);
            }
        }

        private static byte[] _iv = new byte[16];
        private static byte[] _key = new byte[32];

        static public void Serialize(object obj, Stream stream, bool obfuscate = false)
        {
            if (obfuscate)
            {
                using (var aes = Aes.Create())
                {
                    ICryptoTransform encryptor = aes.CreateEncryptor(_key, _iv);
                    stream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
                }
            }
            var setting = new XmlWriterSettings
            {
                CloseOutput = false,
                Encoding = Encoding.UTF8,
                Indent = false,
                NewLineHandling = NewLineHandling.None,
                NewLineOnAttributes = false,
            };
            using (var writer = XmlWriter.Create(stream, setting))
            {
                using (var dictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer))
                {
                    var serializer = new DataContractSerializer(obj.GetType());
                    serializer.WriteObject(dictionaryWriter, obj);
                }
            }
            if (obfuscate)
            {
                ((CryptoStream)stream).FlushFinalBlock();
            }

        }

        static public void Serialize(object obj, string filename, bool obfuscate = false)
        {
            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {            
                Serialize(obj, stream, obfuscate);
            }
        }

        static public T Deserialize<T>(Stream stream, bool obfuscated = false)
        {
            if (obfuscated)
            {
                using (var aes = Aes.Create())
                {
                    ICryptoTransform decryptor = aes.CreateDecryptor(_key, _iv);
                    stream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
                }
            }
            var setting = new XmlReaderSettings
            {
            };
            using (var reader = XmlReader.Create(stream, setting))
            {
                using (var dictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(dictionaryReader);
                }
            }
        }

        static public T Deserialize<T>(string filename, bool obfuscated = false)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Deserialize<T>(stream, obfuscated);
            }
        }
    }
}

