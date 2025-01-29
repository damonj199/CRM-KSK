using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class ClientServiceBlazor
{
    private readonly HttpClient _httpClient;

    public ClientServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsByNameAsync(string firstName, string lastName)
    {
        var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<ClientDto>>($"api/Clients/search-by-name?firstName={firstName}&lastName={lastName}");

        return response ?? [];
    }

    public async Task<string> AddClientAsync(ClientDto clientDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Clients", clientDto);

        if (response.IsSuccessStatusCode)
        {
            return "Клиент успешно добавлен!";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Ошибка: {errorMessage}";
        }
    }

    public async Task<bool> DeleteClientAsync(string phoneNumber)
    {
        var response = await _httpClient.DeleteAsync($"/{phoneNumber}");
        return response.IsSuccessStatusCode;
    }
}

