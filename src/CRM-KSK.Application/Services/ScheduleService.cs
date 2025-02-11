using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(CancellationToken cancellationToken)
    {
        var startOfWeek = DateOnly.FromDateTime(DateTime.Today);
        var endOfWeek = startOfWeek.AddDays(6);

        var schedules = await _scheduleRepository.GetWeeksSchedule(startOfWeek, endOfWeek, cancellationToken);
        var scheduleDto = _mapper.Map<IReadOnlyList<ScheduleDto>>(schedules);

        return scheduleDto ?? [];
    }

    public async Task AddOrUpdateSchedule(ScheduleDto scheduleDto, CancellationToken cancellationToken)
    {
        var schedule = _mapper.Map<Schedule>(scheduleDto);
        await _scheduleRepository.AddOrUpdateSchedule(schedule, cancellationToken);
    }

    public async Task DeleteSchedule(Guid id, CancellationToken cancellationToken)
    {
        if(id != Guid.Empty)
        {
            await _scheduleRepository.DeleteSchedule(id, cancellationToken);
        }
    }
}
