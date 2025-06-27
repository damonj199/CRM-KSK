using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class HorsesWorkService : IHorsesWorkService
{
    private readonly IHorsesRepository _horsesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<HorsesWorkService> _logger;
    public HorsesWorkService(IHorsesRepository horsesRepository, IMapper mapper, ILogger<HorsesWorkService> logger)
    {
        _horsesRepository = horsesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    private DateOnly today = DateOnly.FromDateTime(DateTime.Today);

    public async Task<bool> AddOrUpdateHorsesWork(WorkHorseDto horseDto, CancellationToken token)
    {
        if (!string.IsNullOrWhiteSpace(horseDto.ContentText) || horseDto.Date >= today)
        {
            var horse = _mapper.Map<WorkHorse>(horseDto);

            horse.GetCurrentWeekMonday(horse.Date);
            await _horsesRepository.AddOrUpdateHorseWork(horse, token);

            return true;
        }

        _logger.LogWarning("Нельзя добавить пустое расписание или задним числом");
        return false;
    }

    public async Task AddHorse(HorseDto horseDto, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(horseDto.Name))
        {
            _logger.LogWarning("Имя не может быть пустым");
            return;
        }

        var horse = _mapper.Map<Horse>(horseDto);
        await _horsesRepository.AddHorse(horse, token);
    }

    public async Task<List<HorseDto>> GetHorsesNameWeek(DateOnly sDate, CancellationToken token)
    {
        var horses = await _horsesRepository.GetHorsesNameWeek(sDate, token);
        var horsesDto = _mapper.Map<List<HorseDto>>(horses);

        return horsesDto ?? [];
    }

    public async Task<List<WorkHorseDto>> GetAllScheduleWorkHorses(CancellationToken token)
    {
        var horses = await _horsesRepository.GetAllScheduleWorkHorses(token);
        var horsesDto = _mapper.Map<List<WorkHorseDto>>(horses);

        return horsesDto ?? [];
    }

    public async Task<List<WorkHorseDto>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token)
    {
        var horses = await _horsesRepository.GetScheduleWorkHorsesWeek(sDate, token);
        var horsesDto = _mapper.Map<List<WorkHorseDto>>(horses);

        _logger.LogWarning($"ПРОВЕРЯЕМ СКОЛЬКО ЭЛЕМЕНТОВ ВОЗВРАЩАЕТСЯ - {horsesDto.Count}");

        return horsesDto ?? [];
    }

    public async Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token)
    {
        var result = await _horsesRepository.DeleteWorkHorseById(id, token);

        return result;
    }
}
