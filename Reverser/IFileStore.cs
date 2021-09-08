namespace Reverser
{
    public interface IFileStore
    {
        bool Exists(string file);
        string Read(string file);
        void Write(string file, string content);
    }
}