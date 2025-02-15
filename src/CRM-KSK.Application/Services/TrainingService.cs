using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Services;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IMapper _mapper;

    public TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
    {
        _mapper = mapper;
        _trainingRepository = trainingRepository;
    }

    public async Task AddTrainingAsync(TrainingDto trainingDto, CancellationToken token)
    {
        var training = _mapper.Map<Training>(trainingDto);
        await _trainingRepository.AddTainingAsync(training, token);
    }

    public async Task DeleteTrainingAsync(Guid id, CancellationToken token)
    {
        await _trainingRepository.DeleteTrainingAsync(id, token);
    }
}
