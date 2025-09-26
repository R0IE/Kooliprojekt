using KooliProjekt.Controllers;

namespace KooliProjekt
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _webRootPath;

        public LocalFileClient(IWebHostEnvironment webHostEnvironment)
        {
            _webRootPath = webHostEnvironment.WebRootPath ?? webHostEnvironment.ContentRootPath;
        }

        private void EnsureValidName(string name, string paramName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);

            if (name.Contains("..") || name.IndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }) >= 0)
                throw new ArgumentException("Invalid characters in path.", paramName);
        }

        private string EnsureStoreDirectory(string storeName)
        {
            EnsureValidName(storeName, nameof(storeName));

            var path = Path.Combine(_webRootPath, storeName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public string[] List(string storeName)
        {
            var path = EnsureStoreDirectory(storeName);
            var files = System.IO.Directory.GetFiles(path);

            return files.Select(file => "/" + storeName + "/" + Path.GetFileName(file)).ToArray();
        }

        public void Save(Stream inputStream, string fileName, string storeName)
        {
            EnsureValidName(fileName, nameof(fileName));
            var dir = EnsureStoreDirectory(storeName);

            var safeFileName = Path.GetFileName(fileName);
            var path = Path.Combine(dir, safeFileName);

            try
            {
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    inputStream.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save file '{fileName}' to store '{storeName}'", ex);
            }
        }

        public void Delete(string fileName, string storeName)
        {
            EnsureValidName(fileName, nameof(fileName));
            var dir = EnsureStoreDirectory(storeName);

            var safeFileName = Path.GetFileName(fileName);
            var path = Path.Combine(dir, safeFileName);

            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to delete file '{fileName}' from store '{storeName}'", ex);
            }
        }
    }
}
