using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace CRM_KSK.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IBirtDaysRepository _birtDaysRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository, IMapper mapper,
        ILogger<ClientService> logger, IMembershipRepository membershipRepository,
        IBirtDaysRepository birtDaysRepository)
    {
        _clientRepository = clientRepository;
        _membershipRepository = membershipRepository;
        _birtDaysRepository = birtDaysRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken)
    {
        _logger.LogWarning("попали в метод добавления клиента в сервисе, проверяем есть ли уже такой клиент");
        var existingClient = await _clientRepository.ClientVerificationAsync(clientDto.Phone, cancellationToken);

        if (existingClient)
            return "Клиент уже зарегистрирован";

        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted
        };

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var clientEntity = _mapper.Map<Client>(clientDto);
            var newClientId = await _clientRepository.AddClientAsync(clientEntity, cancellationToken);

            var membershipDto = clientDto.Memberships;

            var memberships = _mapper.Map<List<Membership>>(clientDto.Memberships);

            foreach(var membership in memberships)
            {
                membership.ClientId = newClientId;
            }
            await _membershipRepository.AddMembershipAsync(memberships, cancellationToken);

            scope.Complete();

            return clientEntity.FirstName;
        }
    }

    public async Task<List<ClientDto>> GetAllClientsWithMemberships(CancellationToken token)
    {
        var clients = await _clientRepository.GetAllClientWithMemberships(token);

        if (clients.Any())
        {
            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return clientsDto;
        }

        return [];
    }

    public async Task<ClientDto> GetClientById(Guid id, CancellationToken token)
    {
        var client = await _clientRepository.GetClientById(id, token);
        if (client == null)
            throw new DirectoryNotFoundException("Клиент не найден");

        var clientDto = _mapper.Map<ClientDto>(client);
        
        return clientDto;
    }

    public async Task<List<BirthdayDto>> GetAllFromBodAsync(CancellationToken token)
    {
        var person = await _birtDaysRepository.GetAllFromBodAsync(token);
        if(person != null)
        {
            var personDto = _mapper.Map<List<BirthdayDto>>(person);
            return personDto;
        }

        return [];
    }

    public async Task<List<ClientDto>> GetAllClientsAsync(CancellationToken token)
    {
        var clients = await _clientRepository.GetAllClientsAsync(token);
        var clientsDto = _mapper.Map<List<ClientDto>>(clients);

        return clientsDto ?? [];
    }

    public async Task<List<ClientDto>> GetClientsForScheduleAsync(CancellationToken token)
    {
        var clients = await _clientRepository.GetClientsForScheduleAsync(token);
        var clientsDto = _mapper.Map<List<ClientDto>>(clients);

        return clientsDto ?? [];
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        if(firstName == null && lastName == null)
        {
            var clients = await _clientRepository.GetAllClientsAsync(cancellationToken);
            var clientsDto = _mapper.Map<IReadOnlyList<ClientDto>>(clients);
            return clientsDto ?? [];
        }
        var clientEntity = await _clientRepository.SearchClientByNameAsync(firstName, lastName, cancellationToken, pageNumber, pageSize);

        var clientDtos = _mapper.Map<IReadOnlyList<ClientDto>>(clientEntity);

        return clientDtos ?? [];
    }

    public async Task DeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        await _clientRepository.SoftDeleteClientAsync(id, cancellationToken);
    }

    public async Task UpdateClientInfo(ClientDto  clientDto, CancellationToken token)
    {
        var client = _mapper.Map<Client>(clientDto);
        await _clientRepository.UpdateClientInfoAsync(client, token);
    }
}
