using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken)
    {
        var existingClient = await _clientRepository.GetClientByPhoneNumberAsync(clientDto.Phone, cancellationToken);
        if (existingClient != null)
        {
            throw new InvalidOperationException("Клиент уже зарегистрирован");
        }

        var clientEntity = _mapper.Map<Client>(clientDto);

        //if (!string.IsNullOrWhiteSpace(clientDto.TrainerName))
        //{
        //    clientEntity.Trainer = await _clientRepository.GetTrainerByNameAsync(clientDto.TrainerName);

        //    if (clientEntity.Trainer == null)
        //    {
        //        throw new InvalidOperationException("Тренер не найден");
        //    }
        //}

        await _clientRepository.AddClientAsync(clientEntity, cancellationToken);
        return clientEntity.FirstName.ToString();
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientByName(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        var clientEntity = await _clientRepository.SearchClientByNameAsync(firstName, lastName, cancellationToken, pageNumber, pageSize);

        var clientDtos = _mapper.Map<IReadOnlyList<ClientDto>>(clientEntity);

        return clientDtos;
    }

    public async Task DeleteClientAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByPhoneNumberAsync(phoneNumber, cancellationToken);
        if (client != null)
        {
            await _clientRepository.DeleteClientAsync(client, cancellationToken);
        }
        else
        {
            throw new Exception("Клиент с указанным номером телефона не найден.");
        }
    }
}
