using System;
using System.Collections.Generic;

namespace Chestniy_znack.Models;

public class ScanSessionRecord
{
    public DateTime SavedAt { get; set; }

    public string DocumentBarcode { get; set; } = string.Empty;

    public string ProductBarcode { get; set; } = string.Empty;

    public List<string> KizCodes { get; set; } = [];
}
