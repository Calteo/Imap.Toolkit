using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Imap.Toolkit.Core
{
    public class ProfileStorage
    {
        private const string extension = ".profile";

        public ProfileStorage(string folder = null)
        {
            Folder = folder ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Imap.Toolkit", "Profiles");
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
        }
        public string Folder { get; private set; }

        public void Store(Profile profile)
        {
            var filename = Path.Combine(Folder, profile.Name + extension);
            ObjectSerializer.Serialize(profile, filename, true);
        }

        public Profile Retrieve(string name)
        {
            var filename = Path.Combine(Folder, name + extension);
            var profile = ObjectSerializer.Deserialize<Profile>(filename, true);
            profile.Name = name;
            return profile;
        }

        public void Remove(string name)
        {
            var filename = Path.Combine(Folder, name + extension);
            File.Delete(filename);
        }

        public string[] GetNames()
        {
            return Directory.GetFiles(Folder, "*" + extension)
                .Select(Path.GetFileNameWithoutExtension).ToArray();
        }

        public Profile[] GetProfiles(ProfileOptions options)
        {
            return GetNames().Where(n => options.NamePattern.IsMatch(n))
                .Select(n => Retrieve(n))
                .Where(p => options.UserNamePattern.IsMatch(p.UserName))
                .OrderBy(p=>p.Name)
                .ToArray();
        }
    }
}