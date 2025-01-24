using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Models;

namespace CRM_KSK.Application.Interfaces;

public interface IClientService
{
    Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken);
    Task DeleteClientAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClientDto>> GetClientByNameAsync(SearchByNameRequest request, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
}