using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class WorkWithMembership : IWorkWithMembership
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IMembershipDeductionLogRepository _logRepository;
    private readonly ILogger<WorkWithMembership> _logger;

    public WorkWithMembership(
        IScheduleRepository scheduleRepository, 
        ILogger<WorkWithMembership> logger, 
        IMembershipRepository membershipRepository,
        IMembershipDeductionLogRepository logRepository)
    {
        _scheduleRepository = scheduleRepository;
        _logger = logger;
        _membershipRepository = membershipRepository;
        _logRepository = logRepository;
    }

    public async Task DeductTrainingsFromMemberships(CancellationToken token)
    {
        var day = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var schedules = await _scheduleRepository.GetYesterdaysScheduleAsync(day, token);

        foreach (var schedule in schedules)
        {
            foreach (var training in schedule.Trainings)
            {
                foreach (var client in training.Clients)
                {
                    var membership = await _membershipRepository
                        .GetMembershipByClientAndTypeAsync(client.Id, training.TypeTrainings, token);

                    if (membership != null)
                    {
                        var trainingsBefore = membership.AmountTraining;
                        membership.AmountTraining--;
                        var trainingsAfter = membership.AmountTraining;
                        
                        _logger.LogWarning($"Удачно списали занятие у {client.LastName} {client.FirstName}");

                        // Создаем лог списания
                        var deductionLog = new MembershipDeductionLog
                        {
                            Id = Guid.NewGuid(),
                            DeductionDate = DateOnly.FromDateTime(DateTime.Today),
                            ClientId = client.Id,
                            MembershipId = membership.Id,
                            ScheduleId = schedule.Id,
                            TrainingId = training.Id,
                            TrainingType = training.TypeTrainings,
                            TrainingsBeforeDeduction = trainingsBefore,
                            TrainingsAfterDeduction = trainingsAfter,
                            MembershipExpired = false
                        };

                        if (membership.AmountTraining == 0)
                        {
                            membership.StatusMembership = Core.Enums.StatusMembership.Закончился;
                            deductionLog.MembershipExpired = true;
                            _logger.LogError("Абонемент закончился!");

                            await _membershipRepository.DeleteMembershipAsync(membership.Id, token);
                        }
                        else
                        {
                            await _membershipRepository.UpdateMembershipAsync(membership, token);
                        }

                        // Сохраняем лог
                        try
                        {
                            await _logRepository.CreateLogAsync(deductionLog, token);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Ошибка при сохранении лога списания для клиента {ClientName}", $"{client.LastName} {client.FirstName}");
                        }
                    }
                    else
                    {
                        _logger.LogError("Не нашли абонемент такого типа!");
                    }
                }
            }
        }
        _logger.LogWarning("Списание со всех абонементов прошло успешно");
    }
}
