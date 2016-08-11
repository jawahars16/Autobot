namespace Autobot.Platform
{
    public interface IFileSystem
    {
        string Read(string filename);

        void Write(string filename, string fileContent);
    }
}