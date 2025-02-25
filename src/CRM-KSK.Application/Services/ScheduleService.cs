using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;

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

    public async Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(DateOnly weekStart, CancellationToken cancellationToken)
    {
        var endOfWeek = weekStart.AddDays(6);

        var schedules = await _scheduleRepository.GetWeeksSchedule(weekStart, endOfWeek, cancellationToken);
        var scheduleDto = _mapper.Map<IReadOnlyList<ScheduleDto>>(schedules);

        return scheduleDto ?? [];
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetScheduleHistory(DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var schedulesHistory = await _scheduleRepository.GetWeeksSchedule(start, end, cancellationToken);
        var sortedScheduleHistory = schedulesHistory.OrderBy(s => s.Date).ThenBy(s => s.Time).ToList();

        var scheduleDtos = _mapper.Map<IReadOnlyList<ScheduleDto>>(sortedScheduleHistory);
        return scheduleDtos ?? [];
    }

    public async Task DeleteSchedule(Guid id, CancellationToken cancellationToken)
    {
        if (id != Guid.Empty)
        {
            await _scheduleRepository.DeleteSchedule(id, cancellationToken);
        }
    }
}
