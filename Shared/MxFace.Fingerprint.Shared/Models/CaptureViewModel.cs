using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFace.Fingerprint.Shared.Models;

public class CaptureViewModel
{
    public string? BitmapData { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorDescription { get; set; }
    public int? Nfiq { get; set; }
    public int? Quality { get; set; }
    public string? WSQInfo { get; set; }

    public byte[] _data => Convert.FromBase64String(this.BitmapData);
}