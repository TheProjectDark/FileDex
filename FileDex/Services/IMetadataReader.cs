using System.Threading.Tasks;
using FileDex.Models;

namespace FileDex.Services;

public interface IMetadataReader
{
    bool CanRead(string filePath);
    Task<FileMetadata> ReadAsync(string filePath);
}