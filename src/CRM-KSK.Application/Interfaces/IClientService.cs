using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IClientService
{
    Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken);
    Task DeleteClientAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<ClientDto> GetClientById(Guid id, CancellationToken token);
    Task<IReadOnlyList<ClientDto>> GetClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    Task UpdateClientInfo(ClientDto clientDto, CancellationToken token);
}