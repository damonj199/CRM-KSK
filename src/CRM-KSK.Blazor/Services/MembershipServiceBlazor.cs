using CRM_KSK.Application.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class MembershipServiceBlazor
{
    private readonly HttpClient _httpClient;

    public MembershipServiceBlazor(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<bool> AddMembership(List<MembershipDto> membershipsDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Memberships", membershipsDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<MembershipDto>> GetAllMembershipClient(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<List<MembershipDto>>($"api/Memberships/all-of-client/{id}");
        return response ?? [];
    }

    public async Task<List<MembershipDto>> GetExpiringMemberships()
    {
        var response = await _httpClient.GetFromJsonAsync<List<MembershipDto>>("api/Memberships/expiring");
        return response ?? [];
    }

    public async Task<bool> DeleteMembership(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Memberships/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateMembership(MembershipDto membershipDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Memberships", membershipDto);
        return response.IsSuccessStatusCode;
    }
}
