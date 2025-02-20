using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class MembershipServiceBlazor
{
    private readonly HttpClient _httpClient;

    public MembershipServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MembershipDto>> GetAllMembershipClient(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<List<MembershipDto>>($"api/Memberships/all-of-client/{id}");
        return response ?? [];
    }

    public async Task<bool> AddMembership(MembershipDto membershipDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Memberships", membershipDto);
        return response.IsSuccessStatusCode;
    }
}
