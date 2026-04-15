using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Chestniy_znack.Models;

namespace Chestniy_znack.Services;

public static class ScanSessionStorage
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static string FilePath => Path.Combine(AppContext.BaseDirectory, "scan-sessions.json");

    public static void Save(ScanSessionRecord record)
    {
        List<ScanSessionRecord> records;

        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            records = JsonSerializer.Deserialize<List<ScanSessionRecord>>(json, JsonOptions) ?? [];
        }
        else
        {
            records = [];
        }

        records.Add(record);

        var output = JsonSerializer.Serialize(records, JsonOptions);
        File.WriteAllText(FilePath, output);
    }
}
