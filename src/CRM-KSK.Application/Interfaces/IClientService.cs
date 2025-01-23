using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces
{
    public interface IClientService
    {
        Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken);
        Task DeleteClientAsync(string phoneNumber, CancellationToken cancellationToken);
        Task<IReadOnlyList<ClientDto>> GetClientByName(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    }
}