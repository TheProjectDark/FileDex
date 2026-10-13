using System.Collections.Generic;

namespace FileDex.Models;

public class FileMetadata
{
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string SizeDisplay => FormatSize(SizeBytes);
    public List<MetadataEntry> Entries { get; set; } = new();

    private static string FormatSize(long bytes)
    {
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        double size = bytes;
        int i = 0;
        while (size >= 1024 && i < units.Length - 1)
        {
            size /= 1024;
            i++;
        }
        return $"{size:0.##} {units[i]}";
    }
}