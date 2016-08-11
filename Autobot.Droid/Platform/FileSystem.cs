using Autobot.Platform;
using System;
using System.IO;

namespace Autobot.Droid.Platform
{
    public class FileSystem : IFileSystem
    {
        public string Read(string filename)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            return File.ReadAllText(path);
        }

        public void Write(string filename, string fileContent)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            File.WriteAllText(path, fileContent);
        }
    }
}