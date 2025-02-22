using CRM_KSK.Dal.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Infrastructure.BackgroundServices;

public class WorkWithMembership : IWorkWithMembership
{
    private readonly IServiceProvider _service;
    private readonly ILogger<WorkWithMembership> _logger;

    public WorkWithMembership(IServiceProvider service, ILogger<WorkWithMembership> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task DeductTrainingsFromMemberships(CancellationToken token)
    {
        using (var scope = _service.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CRM_KSKDbContext>();

            var day = DateOnly.FromDateTime(DateTime.Today);

            var schedules = await dbContext.Schedules
                .Where(t => t.Date == day)
                .Include(c => c.Trainings)
                    .ThenInclude(c => c.Clients)
                .ToListAsync(token);

            foreach (var schedule in schedules)
            {
                foreach (var training in schedule.Trainings)
                {
                    foreach (var client in training.Clients)
                    {
                        var membership = await dbContext.Memberships
                            .Where(c => c.ClientId == client.Id
                                        && c.TypeTrainings == training.TypeTrainings)
                            .FirstOrDefaultAsync(token);

                        if (membership != null)
                        {
                            membership.AmountTraining--;
                            _logger.LogInformation($"Удачно списали занятие у {client.LastName} {client.FirstName}");

                            if (membership.AmountTraining == 0)
                            {
                                membership.StatusMembership = Core.Enums.StatusMembership.Закончился;
                                _logger.LogError("Абонемент закончился!");
                            }

                            dbContext.Memberships.Update(membership);
                        }
                        else
                        {
                            _logger.LogWarning("Не нашли абонемент такого типа!");
                        }

                        await dbContext.SaveChangesAsync(token);
                    }
                }
            }
            _logger.LogWarning("Списание со всех абонементов прошло успешно");
        }
    }
}
