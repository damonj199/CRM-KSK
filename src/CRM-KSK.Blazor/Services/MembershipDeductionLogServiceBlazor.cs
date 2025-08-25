using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class MembershipDeductionLogServiceBlazor
{
    private readonly HttpClient _httpClient;

    public MembershipDeductionLogServiceBlazor(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsAsync(DateOnly date)
    {
        var response = await _httpClient.GetAsync($"api/MembershipDeductionLog?date={date:yyyy-MM-dd}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<MembershipDeductionLogDto>>() ?? [];
        }

        return [];
    }
} 