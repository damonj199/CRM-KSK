using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class MembershipDeductionLogServiceBlazor
{
    private readonly HttpClient _httpClient;

    public MembershipDeductionLogServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsAsync(DateOnly date)
    {
        var response = await _httpClient.GetAsync($"api/MembershipDeductionLog?date={date:yyyy-MM-dd}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<MembershipDeductionLogDto>>() ?? Enumerable.Empty<MembershipDeductionLogDto>();
        }

        return Enumerable.Empty<MembershipDeductionLogDto>();
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsByClientAsync(Guid clientId)
    {
        var response = await _httpClient.GetAsync($"api/MembershipDeductionLog/client/{clientId}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<MembershipDeductionLogDto>>() ?? Enumerable.Empty<MembershipDeductionLogDto>();
        }

        return Enumerable.Empty<MembershipDeductionLogDto>();
    }

    public async Task<int> GetDeductionLogsCountAsync(DateOnly date)
    {
        var response = await _httpClient.GetAsync($"api/MembershipDeductionLog/count?date={date:yyyy-MM-dd}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<int>();
        }

        return 0;
    }
} 