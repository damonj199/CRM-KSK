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
        _logger.LogInformation("попали в метод добавления клиента в сервисе");
        var existingClient = await _clientRepository.GetClientByPhoneNumberAsync(clientDto.Phone, cancellationToken);

        _logger.LogInformation($"вернулся в сервис после проверки в БД");

        if (existingClient != null)
            throw new InvalidOperationException("Клиент уже зарегистрирован");

        var clientEntity = _mapper.Map<Client>(clientDto);

        if (!string.IsNullOrWhiteSpace(clientDto.TrainerName))
        {
            clientEntity.Trainer = await _clientRepository.GetTrainerByNameAsync(clientDto.TrainerName);
            _logger.LogInformation("нашли тренера?");
            if (clientEntity.Trainer == null)
                throw new InvalidOperationException("Тренер не найден");
        }
        _logger.LogInformation("все проверки пройдены, добавляем нового клиента");
        await _clientRepository.AddClientAsync(clientEntity, cancellationToken);
        return clientEntity.FirstName;
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
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
}
