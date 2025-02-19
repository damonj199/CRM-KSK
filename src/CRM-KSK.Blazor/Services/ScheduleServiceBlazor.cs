using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class ScheduleServiceBlazor
{
    private readonly HttpClient _httpClient;

    public ScheduleServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule()
    {
        var schedule = await _httpClient.GetFromJsonAsync<IReadOnlyList<ScheduleDto>>("api/Schedules/week");

        return schedule ?? [];
    }

    public async Task AddOrUpdateSchedule(ScheduleDto scheduleDto)
    {
        await _httpClient.PostAsJsonAsync("api/Schedules", scheduleDto);
    }

    public async Task<bool> DeleteTrainingAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Schedules/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetScheduleHistory(DateOnly startDate, DateOnly endDate)
    {
        return await _httpClient.GetFromJsonAsync<List<ScheduleDto>>($"api/Schedules/history?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}") ?? [];
    }
}
