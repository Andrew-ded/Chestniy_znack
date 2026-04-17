using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using Chestniy_znack.Models;

namespace Chestniy_znack.Services;

public static class ScanSessionStorage
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    // TODO: при необходимости поменяйте путь сохранения здесь.
    private const string StorageDirectory = @"C:\Users\andre\RiderProjects\Chestniy_znack\Chestniy_znack\SavedJson";

    public static string Save(ScanSessionRecord record)
    {
        Directory.CreateDirectory(StorageDirectory);

        var safeFileName = SanitizeFileName(record.OrderBarcode);
        var filePath = Path.Combine(StorageDirectory, $"{safeFileName}.json");
        var output = JsonSerializer.Serialize(record, JsonOptions);

        File.WriteAllText(filePath, output);
        return filePath;
    }

    private static string SanitizeFileName(string value)
    {
        var sanitized = value.Trim().Replace('/', '_');

        foreach (var invalidChar in Path.GetInvalidFileNameChars())
        {
            sanitized = sanitized.Replace(invalidChar, '_');
        }

        return string.IsNullOrWhiteSpace(sanitized) ? $"scan_{DateTime.Now:yyyyMMdd_HHmmss}" : sanitized;
    }
}
