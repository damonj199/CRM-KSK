using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class HorsesServiceBlazor
{
    private readonly HttpClient _httpClient;

    public HorsesServiceBlazor(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<string> AddWorkHorse(WorkHorseDto horseDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork", horseDto);

        if (response.IsSuccessStatusCode)
        {
            return "Запись, успешно добавлена!";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Ошибка: {errorMessage}";
        }
    }

    public async Task<List<WorkHorseDto>> GetAllWorkHorses()
    {
        var response = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/all");
        return response ?? [];
    }

    public async Task<List<WorkHorseDto>> GetWeeksSchedule(DateOnly start)
    {
        var schedule = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/week?weekStart={start:yyyy-MM-dd}");

        return schedule ?? [];
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/HorsesWork/{id}");
        return response.IsSuccessStatusCode;
    }
}