using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFace.Fingerprint.Shared.Models;

public class DeviceInfo
{
    public string Certificate { get; set; }
    public int Height { get; set; }
    public string LocalIP { get; set; }
    public string LocalMac { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string PublicIP { get; set; }
    public string SerialNo { get; set; }
    public string SystemID { get; set; }
    public int Width { get; set; }

}

public class Device
{
    public required DeviceInfo DeviceInfo { get; set; }
    public required string ErrorCode { get; set; }
    public required string ErrorDescription { get; set; }
    public required string WSQInfo { get; set; }
}
