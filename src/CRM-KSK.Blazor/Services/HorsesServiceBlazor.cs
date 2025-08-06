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

    //Horse 
    public async Task<string> AddHorse(HorseDto horseDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork/horse", horseDto);

        return await HandleResponse(response, "Запись успешно добавлена");
    }

    public async Task<string> AddHorsesLastWeek(DateOnly sDate)
    {
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork/transfer-last-week", sDate);

        return await HandleResponse(response, "Записи успешно перенесены");
    }

    public async Task<List<HorseDto>> GetHorsesByWeek(DateOnly sDate)
    {
        var response = await _httpClient.GetFromJsonAsync<List<HorseDto>>($"api/HorsesWork/horses-week?sDate={sDate:yyyy-MM-dd}");
        return response ?? [];
    }

    public async Task<bool> UpdateHorseName(long id, string name)
    {
        var dto = new UpdateHorseNameDto(id, name);
        var response = await _httpClient.PatchAsJsonAsync("api/HorsesWork/name", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteHorse(long id)
    {
        var response = await _httpClient.DeleteAsync($"api/HorsesWork/name/{id}");
        return response.IsSuccessStatusCode;
    }

    //Work-Horse
    public async Task<string> AddWorkHorse(WorkHorseDto workHorseDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/HorsesWork/work", workHorseDto);

        return await HandleResponse(response, "Запись успешно добавлена");
    }

    public async Task<List<WorkHorseDto>> GetAllWorkHorses()
    {
        var response = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/work-all");
        return response ?? [];
    }

    public async Task<List<WorkHorseDto>> GetWorkHorsesByWeek(DateOnly weekStart)
    {
        var schedule = await _httpClient.GetFromJsonAsync<List<WorkHorseDto>>($"api/HorsesWork/work-horses-week?weekStart={weekStart:yyyy-MM-dd}");

        return schedule ?? [];
    }

    public async Task<bool> UpdateWorkHorse(Guid id, string content)
    {
        var dto = new UpdateWorkHorse(id, content);
        var response = await _httpClient.PatchAsJsonAsync("api/HorsesWork/work", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteWorkHorse(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/HorsesWork/work/{id}");
        return response.IsSuccessStatusCode;
    }


    private static async Task<string> HandleResponse(HttpResponseMessage response, string successMessage)
    {
        if (response.IsSuccessStatusCode)
        {
            return successMessage;
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return $"Ошибка: {errorMessage}";
    }
}