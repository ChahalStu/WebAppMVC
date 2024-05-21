using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IFileService
    {
        Task Upload(FileModel fileModel);
        Task<Stream> Get(string name);
        Task Delete(string name);
        Task<IEnumerable<string>> List();
        Task DownloadToFile(string name, string filePath);
        Task Update(string name, Stream newContent);
        Task<IDictionary<string, string>> GetMetadata(string name);
    }
}
