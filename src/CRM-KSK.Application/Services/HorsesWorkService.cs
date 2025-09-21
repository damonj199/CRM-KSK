using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Services;

public class HorsesWorkService : IHorsesWorkService
{
    private readonly IHorsesRepository _horsesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<HorsesWorkService> _logger;
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Today);

    public HorsesWorkService(IHorsesRepository horsesRepository, IMapper mapper, ILogger<HorsesWorkService> logger)
    {
        _horsesRepository = horsesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddHorsesLastWeek(DateOnly sDate, CancellationToken token)
    {
        var startLastWeek = sDate.AddDays(-7);
        var horsesLastWeek = await _horsesRepository.GetHorsesNameWeek(startLastWeek, token);
        var horsesCurrentWeek = await _horsesRepository.GetHorsesNameWeek(sDate, token);

        if (horsesLastWeek.Count == 0)
        {
            _logger.LogWarning("Не будем переносить пустую неделю");
            return;
        }

        if (horsesCurrentWeek.Count > 0)
        {
            foreach (var horse in horsesCurrentWeek)
            {
                await _horsesRepository.DeleteHorseName(horse.Id, token);
            }
        }

        var newHorse = horsesLastWeek.Select(h => new Horse
        {
            RowNumber = h.RowNumber,
            Name = h.Name,
            StartWeek = sDate
        }).ToList();

        await _horsesRepository.AddHorsesLastWeek(newHorse, token);
    }

    public async Task<bool> AddWorkHorse(WorkHorseDto horseDto, CancellationToken token)
    {
        if (!string.IsNullOrWhiteSpace(horseDto.ContentText))
        {
            var horse = _mapper.Map<HorseWork>(horseDto);

            await _horsesRepository.AddWorkHorse(horse, token);

            return true;
        }

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

        return horsesDto ?? [];
    }

    public async Task UpdateWorkHorse(Guid id, string content, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ValidationException("Текст не может быть пустым");

        await _horsesRepository.UpdateWorkHorse(id, content, token);
        
    }

    public async Task UpdateHorseName(long id, string name, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Кличка не может быть пустой");

        await _horsesRepository.UpdateHorseName(id, name, token);
    }

    public async Task<bool> DeleteHorseById(long id, CancellationToken token)
    {
        var result = await _horsesRepository.DeleteHorseName(id, token);

        return result;
    }

    public async Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token)
    {
        var result = await _horsesRepository.DeleteWorkHorseById(id, token);

        return result;
    }
}
