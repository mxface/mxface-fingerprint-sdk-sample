using MxFace.Fingerprint.Shared.Models;

namespace MxFace.Fingerprint.API.Client.Interfaces;

public interface ICapture
{
    Task<CaptureViewModel> StartCaptureAsync(int Timeout = 10, int MinimumQuality = 60);
}
