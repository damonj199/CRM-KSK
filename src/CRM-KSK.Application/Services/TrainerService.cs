using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace CRM_KSK.Application.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private const string _dafaultPassword = "12345";

    public TrainerService(ITrainerRepository trainerRepository, IMapper mapper,
        IPasswordHasher passwordHasher, IConfiguration configuration)
    {
        _trainerRepository = trainerRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;;
    }

    public async Task AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken)
    {
        var existingTrainer = await _trainerRepository.GetTrainerByNameAsync(trainerDto.FirstName, trainerDto.LastName, cancellationToken);

        if (existingTrainer != null)
            throw new Exception("Тренер уже есть в системе");

        var trainerEntity = _mapper.Map<Trainer>(trainerDto);
        trainerEntity.PasswordHash = _passwordHasher.Generate(_dafaultPassword);

        await _trainerRepository.AddTrainerAsync(trainerEntity, cancellationToken);
    }

    public async Task<IReadOnlyList<TrainerDto>> GetTrainersAsync(CancellationToken cancellationToken)
    {
        var trainersEntitis = await _trainerRepository.GetTrainersAsync(cancellationToken);
        
        var trainersDtos = _mapper.Map<IReadOnlyList<TrainerDto>>(trainersEntitis);

        return trainersDtos ?? [];
    }

    public async Task<TrainerDto> GetTrainerByName(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var trainerEntity = await _trainerRepository.GetTrainerByNameAsync(firstName, lastName, cancellationToken);
        var trainerDto = _mapper.Map<TrainerDto>(trainerEntity);

        return trainerDto;
    }

    public async Task<TrainerDto> GetTrainerByIdAsync(Guid id, CancellationToken token)
    {
        var trainer = await _trainerRepository.GetTrainerByIdAsync(id, token);
        if (trainer == null)
            throw new Exception("Тренер не найден");

        var trainerDto = _mapper.Map<TrainerDto>(trainer);
        return trainerDto;
    }

    public async Task UpdateTrainerInfoAsync(TrainerDto trainerDto, CancellationToken token)
    {
        var tren = await GetTrainerByIdAsync(trainerDto.Id, token);

        var trainer = _mapper.Map<Trainer>(trainerDto);
        await _trainerRepository.UpdateTrainerInfoAsync(trainer, token);
    }

    public async Task DeleteTrainer(Guid id, CancellationToken cancellationToken)
    {
        await _trainerRepository.SoftDeleteTraner(id, cancellationToken);
    }
}
