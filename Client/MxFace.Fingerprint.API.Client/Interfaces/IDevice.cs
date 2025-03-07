using MxFace.Fingerprint.Shared.Models;

namespace MxFace.Fingerprint.API.Client.Interfaces;

public interface IDevice
{
    Task<int> GetConnectedDevices(List<string> devices);
    Task<Device> GetDeviceInfoAsync(string deviceName);
    
}
