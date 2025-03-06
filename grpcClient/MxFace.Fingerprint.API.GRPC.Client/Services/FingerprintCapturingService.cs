using MxFace.Fingerprint.API.GRPC.Client.Interfaces;
using MxFace.Fingerprint.Shared.Models;
using System.Text.Json;

namespace MxFace.Fingerprint.API.GRPC.Client.Services;

public class FingerprintCapturingService(HttpClient httpClient) : ICapture
{
    private readonly string remoteServiceBaseUrl = "https://localhost:8034/mfscan";

    public async Task<CaptureViewModel> StartCaptureAsync(int Timeout = 10, int MinimumQuality = 60)
    {
        var response = (
            await PostRequestAsync(
                "capture",
                new { Quality = MinimumQuality, TimeOut = Timeout }
            )
        ).FirstOrDefault();
        if (IsSuccessStatusCode(response.Key))
        {
            return JsonSerializer.Deserialize<CaptureViewModel>(response.Value);
        }
        else
            return null;
    }

    private async Task<Dictionary<int, string>> PostRequestAsync(
        string endpoint,
        object content = null
    )
    {
        endpoint = Path.Combine(remoteServiceBaseUrl, endpoint);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);

        if (content != null)
            requestMessage.Content = new StringContent(
                JsonSerializer.Serialize(content),
                null,
                "application/json"
            );

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            return new Dictionary<int, string>
            {
                { (int)response.StatusCode, await response.Content.ReadAsStringAsync() },
            };
        }
        else
            return new Dictionary<int, string> { { 0, string.Empty } };
    }

    private bool IsSuccessStatusCode(int statusCode)
    {
        return statusCode >= 200 && statusCode <= 299;
    }
}

