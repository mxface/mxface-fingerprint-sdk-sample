using MxFace.Fingerprint.API.GRPC.Client.Interfaces;
using MxFace.Fingerprint.Shared.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MxFace.Fingerprint.API.GRPC.Client.Services;

public class DeviceService(HttpClient httpClient, ILogger<DeviceService> logger) : IDevice
{
    private readonly string remoteServiceBaseUrl = "https://localhost:8034/mfscan/";

    public async Task<int> GetConnectedDevices(List<string> devices)
    {
        var response = (await PostRequestAsync("connecteddevicelist")).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization
            Regex regex = new Regex("\"Connected Device :(.*?)\",");

            Match match = regex.Match(response.Value);

            devices.Add(match.Groups[1].Value);

            return devices.Count;
        }
        else
            return 0;
    }

    public async Task<Device> GetDeviceInfoAsync(string deviceName)
    {
        var response = (
            await PostRequestAsync("info", new { ConnectedDvc = deviceName })
        ).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            var deviceResponse = JsonSerializer.Deserialize<Device>(response.Value);
            if (deviceResponse.ErrorCode == "0")
            {
                return JsonSerializer.Deserialize<Device>(response.Value);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
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

