using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository, IMapper mapper, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken)
    {
        _logger.LogDebug("попали в метод добавления клиента в сервисе, проверяем есть ли уже такой клиент");
        var existingClient = await _clientRepository.ClientVerificationAsync(clientDto.Phone, cancellationToken);

        if (existingClient)
            throw new InvalidOperationException("Клиент уже зарегистрирован");

        var clientEntity = _mapper.Map<Client>(clientDto);

        _logger.LogDebug("все проверки пройдены, добавляем нового клиента");
        await _clientRepository.AddClientAsync(clientEntity, cancellationToken);
        return clientEntity.FirstName;
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
        var person = await _clientRepository.GetAllFromBodAsync(token);
        if(person != null)
        {
            var personDto = _mapper.Map<List<BirthdayDto>>(person);
            return personDto;
        }

        return [];
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

    public async Task DeleteClientAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByPhoneNumberAsync(phoneNumber, cancellationToken);
        if (client == null)
            throw new Exception("Клиент с указанным номером телефона не найден.");

        await _clientRepository.DeleteClientAsync(client, cancellationToken);
    }

    public async Task UpdateClientInfo(ClientDto  clientDto, CancellationToken token)
    {
        var client = _mapper.Map<Client>(clientDto);
        await _clientRepository.UpdateClientInfoAsync(client, token);
    }

    public (string firstName, string? lastName) SplitFullName(string fullName)
    {
        var parts = fullName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        return parts.Length switch
        {
            1 => (parts[0], null),
            2 => (parts[0], parts[1])
        };
    }
}
