using CRM_KSK.Core.Entities;
using CRM_KSK.Dal.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRM_KSK.Infrastructure;

public class ProcessBirthdays : IProcessBirthdays
{
    private readonly IServiceProvider _service;

    public ProcessBirthdays(IServiceProvider service)
    {
        _service = service;
    }

    public async Task ProcessBodAsync(CancellationToken token)
    {
        using (var scope = _service.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CRM_KSKDbContext>();

            var allNotifications = await dbContext.BirthDays.ToListAsync();
            dbContext.BirthDays.RemoveRange(allNotifications);
            await dbContext.SaveChangesAsync(token);

            var month = DateTime.Today.Month;

            var clientsBod = await dbContext.Clients
                .Where(c => c.DateOfBirth.Month == month)
                .Select(c => new BirthdayNotification
                {
                    PersonId = c.Id,
                    PersonType = "Клиент",
                    Name = $"{c.FirstName} {c.LastName}",
                    Phone = c.Phone,
                    Birthday = c.DateOfBirth
                }).ToListAsync(token);

            var trainerBod = await dbContext.Trainers
                .Where(t => t.DateOfBirth.Month == month)
                .Select(t => new BirthdayNotification
                {
                    PersonId = t.Id,
                    PersonType = "Тренер",
                    Name = $"{t.FirstName} {t.LastName}",
                    Phone = t.Phone,
                    Birthday = t.DateOfBirth
                }).ToListAsync(token);

            var allBodays = clientsBod
                .Concat(trainerBod)
                .ToList();

            await dbContext.BirthDays.AddRangeAsync(allBodays, token);
            await dbContext.SaveChangesAsync(token);
        }
    }
}
