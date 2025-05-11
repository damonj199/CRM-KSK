using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class ScheduleServiceBlazor
{
    private readonly HttpClient _httpClient;

    public ScheduleServiceBlazor(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task AddScheduleComment(DateOnly date, TimeSpan time, string content, CancellationToken token = default)
    {
        var request = new ScheduleCommentDto
        {
            Date = date,
            Time = time,
            CommentText = content,
        };
        await _httpClient.PostAsJsonAsync("api/Schedules/comment", request, token);
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(DateTime weekStart)
    {
        DateOnly start = DateOnly.FromDateTime(weekStart);
        var schedule = await _httpClient.GetFromJsonAsync<IReadOnlyList<ScheduleDto>>($"api/Schedules/week?weekStart={start:yyyy-MM-dd}");

        return schedule ?? [];
    }

    public async Task<List<ScheduleCommentDto>> GetScheduleComments()
    {
        var comments = await _httpClient.GetFromJsonAsync<List<ScheduleCommentDto>>($"api/Schedules/comments");
        return comments ?? [];
    }

    public async Task DeleteScheduleComment(int id)
    {
        await _httpClient.DeleteAsync($"api/Schedules/comment/{id}");
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetScheduleHistory(DateOnly startDate, DateOnly endDate)
    {
        return await _httpClient.GetFromJsonAsync<List<ScheduleDto>>($"api/Schedules/history?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}") ?? [];
    }
}
