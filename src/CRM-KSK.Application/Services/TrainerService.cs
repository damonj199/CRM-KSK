using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
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

    public async Task<string> AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken)
    {
        var existingTrainer = await _trainerRepository.GetTrainerByNameAsync(trainerDto.FirstName, trainerDto.LastName, cancellationToken);
        if (existingTrainer.Count != 0)
            throw new Exception("Тренер уже есть в системе");

        var trainerEntity = _mapper.Map<Trainer>(trainerDto);

        await _trainerRepository.AddTrainerAsync(trainerEntity, cancellationToken);

        return trainerDto.FirstName;
    }

    public async Task<TrainerDto> GetTrainerAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var trainerEntity = await _trainerRepository.GetTrainerByNameAsync(firstName, lastName, cancellationToken);
        if (trainerEntity == null)
            throw new Exception("Тренер не найден");

        return _mapper.Map<TrainerDto>(trainerEntity);
    }

    public async Task DeleteTrainer(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetTrainerByNameAsync(firstName, lastName, cancellationToken);
        if (trainer == null)
            throw new Exception("Тренер не найден");

        await _trainerRepository.DeleteTraner(trainer.FirstOrDefault(), cancellationToken);
    }
}
