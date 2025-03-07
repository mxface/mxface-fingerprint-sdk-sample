using System.Collections.Generic;
using System.Net.Http.Json;
using MxFace.Fingerprint.Shared.Models;

namespace MxFace.Fingerprint.API.Client.Services;

public class FingerprintApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/Fingerprint";

    public FingerprintApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnrollmentResponse> EnrollAsync(
        byte[] templateData,
        string personId,
        string group
    )
    {
        var request = new EnrollRequest
        {
            TemplateData = templateData,
            PersonId = personId,
            Group = group,
        };

        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/enroll", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EnrollmentResponse>();
    }

    public async Task<List<SearchResponse>> SearchAsync(byte[] templateData, string group)
    {
        var request = new SearchRequest { TemplateData = templateData, Group = group };
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/search", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<SearchResponse>>();
    }

    public async Task<MatchResponse> VerifyAsync(byte[] sourceTemplate, byte[] targetTemplate)
    {
        var request = new MatchRequest
        {
            SourceTemplate = sourceTemplate,
            TargetTemplate = targetTemplate,
        };
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/verify", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MatchResponse>();
    }
}
