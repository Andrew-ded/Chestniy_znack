using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chestniy_znack.Models;

public class ScanSessionRecord
{
    public DateTime SavedAt { get; set; }

    [JsonPropertyName("ШрихкодПриказа")]
    public string OrderBarcode { get; set; } = string.Empty;

    [JsonPropertyName("Product")]
    public List<ScanSessionProductRecord> Products { get; set; } = [];
}

public class ScanSessionProductRecord
{
    public string GazIdKISU { get; set; } = string.Empty;

    public List<string> KizCodes { get; set; } = [];
}
