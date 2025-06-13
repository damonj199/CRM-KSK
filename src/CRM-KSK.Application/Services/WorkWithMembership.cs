using CRM_KSK.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class WorkWithMembership : IWorkWithMembership
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly ILogger<WorkWithMembership> _logger;

    public WorkWithMembership(IScheduleRepository scheduleRepository, ILogger<WorkWithMembership> logger, IMembershipRepository membershipRepository)
    {
        _scheduleRepository = scheduleRepository;
        _logger = logger;
        _membershipRepository = membershipRepository;
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
                        membership.AmountTraining--;
                        _logger.LogWarning($"Удачно списали занятие у {client.LastName} {client.FirstName}");

                        if (membership.AmountTraining == 0)
                        {
                            membership.StatusMembership = Core.Enums.StatusMembership.Закончился;
                            _logger.LogError("Абонемент закончился!");

                            await _membershipRepository.DeleteMembershipAsync(membership.Id, token);
                            continue;
                        }

                        await _membershipRepository.UpdateMembershipAsync(membership, token);
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
