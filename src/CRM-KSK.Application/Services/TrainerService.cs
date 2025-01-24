using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IMapper _mapper;

    public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
    {
        _trainerRepository = trainerRepository;
        _mapper = mapper;
    }

    public async Task AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken)
    {
        var existingTrainer = await _trainerRepository.GetTrainerByNameAsync(trainerDto.FirstName, trainerDto.LastName, cancellationToken);
        if (existingTrainer != null)
            throw new Exception("Тренер уже есть в системе");

        var trainerEntity = _mapper.Map<Trainer>(trainerDto);

        await _trainerRepository.AddTrainerAsync(trainerEntity, cancellationToken);
    }

    public async Task<TrainerDto> GetTrainerAsync(SearchByNameRequest nameRequest, CancellationToken cancellationToken)
    {
        var trainerEntity = await _trainerRepository.GetTrainerByNameAsync(nameRequest.FirstName, nameRequest.LastName, cancellationToken);
        if (trainerEntity == null)
            throw new Exception("Тренер не найден");

        return _mapper.Map<TrainerDto>(trainerEntity);
    }

    public async Task DeleteTrainer(SearchByNameRequest nameRequest, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetTrainerByNameAsync(nameRequest.FirstName, nameRequest.LastName, cancellationToken);
        if (trainer == null)
            throw new Exception("Тренер не найден");

        await _trainerRepository.DeleteTraner(trainer, cancellationToken);
    }
}
