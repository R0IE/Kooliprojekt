using System.IO;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.Services
{
    public class LocalFileClientTests
    {
        [Fact]
        public void Save_List_Delete_Works()
        {
            var tempRoot = Path.Combine(Path.GetTempPath(), "LocalFileClientTests", Path.GetRandomFileName());
            Directory.CreateDirectory(tempRoot);

            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(tempRoot);

            var client = new LocalFileClient(envMock.Object);

            var store = "uploads";
            var fileName = "test.txt";
            var content = "hello world";

            using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
            {
                client.Save(ms, fileName, store);
            }

            var list = client.List(store);
            Assert.Single(list);
            Assert.Contains("/uploads/test.txt", list[0]);

            client.Delete(fileName, store);

            var listAfter = client.List(store);
            Assert.Empty(listAfter);

            // cleanup
            Directory.Delete(tempRoot, true);
        }

        [Fact]
        public void Save_InvalidFileName_Throws()
        {
            var tempRoot = Path.Combine(Path.GetTempPath(), "LocalFileClientTests", Path.GetRandomFileName());
            Directory.CreateDirectory(tempRoot);

            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(tempRoot);

            var client = new LocalFileClient(envMock.Object);

            var store = "uploads";
            var fileName = "../evil.txt";

            using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("x")))
            {
                Assert.Throws<ArgumentException>(() => client.Save(ms, fileName, store));
            }

            Directory.Delete(tempRoot, true);
        }
    }
}
