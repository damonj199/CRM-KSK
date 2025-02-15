using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class TrainingServiceBlazor
{
    private readonly HttpClient _httpClient;

    public TrainingServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddTrainingAsync(TrainingDto trainingDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Trainings", trainingDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTrainingAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Trainings/{id}");
        return response.IsSuccessStatusCode;
    }
}
