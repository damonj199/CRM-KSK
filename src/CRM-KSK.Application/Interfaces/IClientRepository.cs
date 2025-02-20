using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IClientRepository
{
    Task AddClientAsync(Client client, CancellationToken cancellationToken);
    Task<bool> ClientVerificationAsync(string phoneNumber, CancellationToken cancellationToken);
    Task DeleteClientAsync(Client client, CancellationToken cancellationToken);
    Task<List<Client>> GetAllClientsAsync(CancellationToken token);
    Task<List<BirthdayNotification>> GetAllFromBodAsync(CancellationToken token);
    Task<Client> GetClientById(Guid id, CancellationToken token);
    Task<Client> GetClientByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<IReadOnlyList<Client>> SearchClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    Task UpdateClientInfoAsync(Client client, CancellationToken token);
}