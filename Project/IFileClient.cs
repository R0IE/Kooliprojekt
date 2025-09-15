namespace KooliProjekt
{
    public interface IFileClient
    {
        string[] List(string storeName);
        void Save(Stream inputStream, string fileName, string storeName);
        void Delete(string fileName, string storeName);
    }
}