using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IClientRepository
{
    Task<Guid> AddClientAsync(Client client, CancellationToken cancellationToken);
    Task<bool> ClientVerificationAsync(string phoneNumber, CancellationToken cancellationToken);
    Task SoftDeleteClientAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Client>> GetAllClientsAsync(CancellationToken token);
    Task<Client> GetClientById(Guid id, CancellationToken token);
    Task<List<Client>> GetClientsForScheduleAsync(CancellationToken token);
    Task<List<BirthdayNotification>> GetClientWithBirthDaysThisMonthAsync(int month, CancellationToken token);
    Task UpdateClientInfoAsync(Client client, CancellationToken token);
    Task<List<Client>> GetAllClientWithMemberships(CancellationToken token);
    Task<List<Client>> GetStatistics(CancellationToken token);
}