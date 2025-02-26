using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class ClientServiceBlazor
{
    private readonly HttpClient _httpClient;

    public ClientServiceBlazor(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<ClientDto>> GetAllClientsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<ClientDto>>("api/Clients/");
        return response ?? [];
    }

    public async Task<List<ClientDto>> GetClientsForSchedulesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<ClientDto>>("api/Clients/for-schedule");
        return response ?? [];
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsByNameAsync(string? firstName = null, string? lastName = null)
    {
        var url = "api/Clients/by-name";
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(firstName))
        {
            queryParams.Add($"firstName={Uri.EscapeDataString(firstName)}");
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            queryParams.Add($"lastName={Uri.EscapeDataString(lastName)}");
        }

        if (queryParams.Any())
        {
            url += "?" + string.Join("&", queryParams);
        }
        var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<ClientDto>>(url);

        return response ?? [];
    }

    public async Task<List<BirthdayDto>> GetFromAllBodAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<BirthdayDto>>("api/Clients/birthdays");
        return response ?? [];
    }

    public async Task<string> AddClientAsync(ClientDto clientDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Clients", clientDto);

        if (response.IsSuccessStatusCode)
        {
            return $"Клиент {clientDto.FirstName}, успешно добавлен!";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Ошибка: {errorMessage}";
        }
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Clients/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<ClientDto> GetClientById(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<ClientDto>($"api/Clients/{id}");
        return response;
    }

    public async Task<bool> UpdateClientInfo(ClientDto clientDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Clients", clientDto);
        return response.IsSuccessStatusCode;
    }
}

