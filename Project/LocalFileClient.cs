using KooliProjekt.Controllers;

namespace KooliProjekt
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _webRootPath;

        public LocalFileClient(IWebHostEnvironment webHostEnvironment)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
        }

        public string[] List(string storeName)
        {
            var path = Path.Combine(_webRootPath, storeName);
            var files = System.IO.Directory.GetFiles(path);

            return files.Select(file => "/" + storeName + "/" + Path.GetFileName(file)).ToArray();
        }

        public void Save(Stream inputStream, string fileName, string storeName)
        {
            var path = Path.Combine(_webRootPath, storeName, Path.GetFileName(fileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(stream);
            }
        }

        public void Delete(string fileName, string storeName)
        {
            var path = Path.Combine(_webRootPath, storeName, Path.GetFileName(fileName));

            if (!File.Exists(path))
            {
                return;
            }

            File.Delete(path);
        }


    }
}
