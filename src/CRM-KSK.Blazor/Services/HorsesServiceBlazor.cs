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
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork/work", horseDto);

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

    public async Task<string> AddHorse(HorseDto horseDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork/horse", horseDto);

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

    public async Task<List<HorseDto>> GetHorsesNameWeek(DateOnly sDate)
    {
        var response = await _httpClient.GetFromJsonAsync<List<HorseDto>>($"api/HorsesWork/horsesWeek?sDate={sDate:yyyy-MM-dd}");
        return response ?? [];
    }

    public async Task<List<WorkHorseDto>> GetAllWorkHorses()
    {
        var response = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/all");
        return response ?? [];
    }

    public async Task<List<WorkHorseDto>> GetWeeksSchedule(DateOnly start)
    {
        var schedule = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/workHorsesWeek?weekStart={start:yyyy-MM-dd}");

        return schedule ?? [];
    }

    public async Task<bool> DeleteWorkHorse(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/HorsesWork/{id}");
        return response.IsSuccessStatusCode;
    }
}