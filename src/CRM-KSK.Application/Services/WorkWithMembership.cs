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
    private readonly DateOnly daductionDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

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
        //var day = DateOnly.FromDateTime(DateTime.Today); //для тестирования
        var day = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var schedules = await _scheduleRepository.GetYesterdaysScheduleAsync(day, token);

        foreach (var schedule in schedules)
        {
            foreach (var training in schedule.Trainings)
            {
                foreach (var client in training.Clients)
                {
                    // Проверяем, является ли время тренировки утренним (до 14:00 включительно)
                    var isMorningTime = schedule.Time <= new TimeSpan(14, 0, 0);
                    
                    // Получаем все абонементы клиента данного типа
                    var memberships = await _membershipRepository
                        .GetMembershipsByClientAndTypeAsync(client.Id, training.TypeTrainings, token);

                    if (memberships != null && memberships.Any())
                    {
                        // Выбираем подходящий абонемент
                        Membership? membershipToUse = null;
                        
                        if (isMorningTime)
                        {
                            // Если время утреннее, сначала ищем утренний абонемент
                            membershipToUse = memberships.FirstOrDefault(m => m.IsMorning && m.AmountTraining > 0);
                            
                            // Если утреннего нет, берем обычный
                            if (membershipToUse == null)
                            {
                                membershipToUse = memberships.FirstOrDefault(m => !m.IsMorning && m.AmountTraining > 0);
                            }
                        }
                        else
                        {
                            // Если время не утреннее, берем только обычный абонемент
                            membershipToUse = memberships.FirstOrDefault(m => !m.IsMorning && m.AmountTraining > 0);
                        }

                        if (membershipToUse != null)
                        {
                            var trainingsBefore = membershipToUse.AmountTraining;
                            membershipToUse.AmountTraining--;
                            var trainingsAfter = membershipToUse.AmountTraining;
                            
                            _logger.LogWarning($"Удачно списали занятие у {client.LastName} {client.FirstName} с абонемента {(membershipToUse.IsMorning ? "утреннего" : "обычного")}");

                            // Создаем лог списания
                            var deductionLog = new MembershipDeductionLog
                            {
                                Id = Guid.NewGuid(),
                                DeductionDate = daductionDate,
                                ClientId = client.Id,
                                MembershipId = membershipToUse.Id,
                                ScheduleId = schedule.Id,
                                TrainingId = training.Id,
                                TrainingType = training.TypeTrainings,
                                TrainingsBeforeDeduction = trainingsBefore,
                                TrainingsAfterDeduction = trainingsAfter,
                                MembershipExpired = false,
                                IsMorningMembership = membershipToUse.IsMorning,
                                IsOneTimeTraining = membershipToUse.IsOneTimeTraining
                            };

                            if (membershipToUse.AmountTraining == 0)
                            {
                                membershipToUse.StatusMembership = Core.Enums.StatusMembership.Ended;
                                deductionLog.MembershipExpired = true;
                                _logger.LogError("Абонемент закончился!");

                                await _membershipRepository.SoftDeleteMembershipAsync(membershipToUse.Id, token);
                            }
                            else
                            {
                                await _membershipRepository.UpdateMembershipAsync(membershipToUse, token);
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
                            _logger.LogError($"Не нашли подходящий абонемент для клиента {client.LastName} {client.FirstName} на время {schedule.Time}");
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
