using System;
using System.IO;
using System.Threading.Tasks;
using FileDex.Models;

namespace FileDex.Services;

public class FileMetadataReader : IMetadataReader
{
    public bool CanRead(string filePath) => File.Exists(filePath);

    public Task<FileMetadata> ReadAsync(string filePath)
    {
        return Task.Run(() =>
        {
            var info = new FileInfo(filePath);
            var meta = new FileMetadata
            {
                FilePath = filePath,
                FileName = info.Name,
                SizeBytes = info.Length,
            };

            void Add(string key, string? value, string category)
            {
                meta.Entries.Add(new MetadataEntry
                {
                    Key = key,
                    Value = value ?? "-",
                    Category = category
                });
            }

            Add("File Name", info.Name, "File");
            Add("Full path", info.FullName, "File");
            Add("Extension", info.Extension, "File");
            Add("Size", meta.SizeDisplay, "File");
            Add("Created", info.CreationTime.ToString("g"), "Dates");
            Add("Last Modified", info.LastWriteTime.ToString("g"), "Dates");
            Add("Last opened", info.LastAccessTime.ToString("g"), "Dates");
            Add("Attributes", info.Attributes.ToString(), "Attributes");
            Add("Read-only", info.IsReadOnly.ToString(), "Attributes");

            return meta;
        });
    }
}