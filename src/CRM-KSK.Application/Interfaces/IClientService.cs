using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IClientService
{
    Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken);
    Task DeleteClientAsync(Guid id, CancellationToken cancellationToken);
    Task<List<ClientDto>> GetAllClientsAsync(CancellationToken token);
    Task<List<ClientDto>> GetAllClientsWithMemberships(CancellationToken token);
    Task<StatisticsDto> GetStatistics(CancellationToken token);
    Task<List<BirthdayDto>> GetAllFromBodAsync(CancellationToken token);
    Task<ClientDto> GetClientById(Guid id, CancellationToken token);
    Task<List<ClientDto>> GetClientsForScheduleAsync(CancellationToken token);
    Task UpdateClientInfo(ClientDto clientDto, CancellationToken token);
}