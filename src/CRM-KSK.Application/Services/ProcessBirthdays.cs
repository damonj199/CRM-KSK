using CRM_KSK.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class ProcessBirthdays : IProcessBirthdays
{
    private readonly IClientRepository _clientRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IBirtDaysRepository _birtDaysRepository;
    private readonly ILogger<ProcessBirthdays> _logger;

    public ProcessBirthdays(ITrainerRepository trainerRepository, IClientRepository clientRepository,
        IBirtDaysRepository birtDaysRepository, ILogger<ProcessBirthdays> logger)
    {
        _trainerRepository = trainerRepository;
        _clientRepository = clientRepository;
        _birtDaysRepository = birtDaysRepository;
        _logger = logger;
    }

    public async Task ProcessBodAsync(CancellationToken token)
    {
        var month = DateTime.Today.Month;

        await _birtDaysRepository.DeleteAllDataAsync(token);

        var clientsBod = await _clientRepository.GetClientWithBirthDaysThisMonthAsync(month, token);
        var trainerBod = await _trainerRepository.GetTrainerWithBirthDaysThisMonthAsync(month, token);

        var allBodays = clientsBod
            .Concat(trainerBod)
            .ToList();

        await _birtDaysRepository.AddPeopleWithBirthDaysThisMonth(allBodays, token);
        _logger.LogWarning($"Обновили данные о днях рождениях в ДБ. для {allBodays.Count} человек");
    }
}
