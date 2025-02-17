using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using System.Transactions;

namespace CRM_KSK.Application.Services;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public TrainingService(ITrainingRepository trainingRepository, IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _mapper = mapper;
        _trainingRepository = trainingRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task AddTrainingAsync(ScheduleFullDto scheduleFull, CancellationToken token)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted
        };

        using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var schedule = _mapper.Map<Schedule>(scheduleFull);
            await _scheduleRepository.AddOrUpdateSchedule(schedule, token);

            var training = _mapper.Map<Training>(scheduleFull);
            await _trainingRepository.AddTainingAsync(training, token);

            scope.Complete();
        }
        
    }

    public async Task DeleteTrainingAsync(Guid id, CancellationToken token)
    {
        await _trainingRepository.DeleteTrainingAsync(id, token);
    }
}
