using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IBirtDaysRepository
{
    Task AddPeopleWithBirthDaysThisMonth(List<BirthdayNotification> people, CancellationToken token);
    Task DeleteAllDataAsync(CancellationToken token);
    Task<List<BirthdayNotification>> GetAllFromBodAsync(CancellationToken token);
}