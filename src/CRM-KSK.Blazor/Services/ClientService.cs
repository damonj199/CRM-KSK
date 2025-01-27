using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Models;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class ClientService
{
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClientDto?> GetClientsByNameAsync(SearchByNameRequest nameRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "clients");
        request.Headers.Add("Name", nameRequest.FirstName);
        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ClientDto>();
        }

        return null;
    }

    public async Task<bool> AddClientAsync(ClientDto clientDto)
    {
        var response = await _httpClient.PostAsJsonAsync("clients", clientDto);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteClientAsync(string phoneNumber)
    {
        var response = await _httpClient.DeleteAsync($"client/{phoneNumber}");
        return response.IsSuccessStatusCode;
    }
}

