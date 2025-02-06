using CRM_KSK.Application.Dtos;
using System.Net.Http.Json;

namespace CRM_KSK.Blazor.Services;

public class TrainerServiceBlazor
{
    private readonly HttpClient _httpClient;

    public TrainerServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TrainerDto> GetTrainerByName(SearchNameDto nameDto)
    {
        var url = "api/Trainers/by-name";
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(nameDto.FirstName))
        {
            queryParams.Add($"firstName={Uri.EscapeDataString(nameDto.FirstName)}");
        }

        if (!string.IsNullOrEmpty(nameDto.LastName))
        {
            queryParams.Add($"lastName={Uri.EscapeDataString(nameDto.LastName)}");
        }

        if (queryParams.Any())
        {
            url += "?" + string.Join("&", queryParams);
        }

        var response = await _httpClient.GetFromJsonAsync<TrainerDto>(url);

        return response;
    }

    public async Task<List<TrainerDto>> GetAllTrainers()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TrainerDto>>("api/Trainers");

        return response ?? [];
    }

    public async Task<string> AddTrainer(TrainerDto trainerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Trainers", trainerDto);

        if (response.IsSuccessStatusCode)
        {
            return "Тренер успешно добавлен!";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Ошибка: {errorMessage}";
        }
    }

    public async Task<bool> DeleteTrainer(string firstName, string lastName)
    {
        var response = await _httpClient.DeleteAsync($"api/Trainers?firstName={firstName}&lastName={lastName}");
        return response.IsSuccessStatusCode;
    }
}
