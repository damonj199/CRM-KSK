using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace CRM_KSK.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IBirtDaysRepository _birtDaysRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository, IMapper mapper,
        ILogger<ClientService> logger, IMembershipRepository membershipRepository,
        IBirtDaysRepository birtDaysRepository, ITrainerRepository trainerRepository,
        IScheduleService scheduleService)
    {
        _clientRepository = clientRepository;
        _membershipRepository = membershipRepository;
        _birtDaysRepository = birtDaysRepository;
        _trainerRepository = trainerRepository;
        _scheduleService = scheduleService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> AddClientAsync(ClientDto clientDto, CancellationToken cancellationToken)
    {
        var existingClient = await _clientRepository.ClientVerificationAsync(clientDto.Phone, cancellationToken);

        if (existingClient)
        { 
            _logger.LogWarning("Клиент с номером {0} уже существует", clientDto.Phone);
            return "Клиент уже зарегистрирован";
        }

        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted
        };
            
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var clientEntity = _mapper.Map<Client>(clientDto);
        var newClientId = await _clientRepository.AddClientAsync(clientEntity, cancellationToken);

        var membershipDto = clientDto.Memberships;

        var memberships = _mapper.Map<List<Membership>>(clientDto.Memberships);

        foreach (var membership in memberships)
        {
            membership.ClientId = newClientId;
        }
        var addedMemberships = await _membershipRepository.AddMembershipAsync(memberships, cancellationToken);

        scope.Complete();

        foreach (var addedMembership in addedMemberships)
        {
            _logger.LogWarning($"Добавлен абонемент для клиента {clientEntity.FirstName} {clientEntity.LastName}," +
                $"на {addedMembership.AmountTraining} занятий, абонемент типа - {addedMembership.TypeTrainings}");
        }

        _logger.LogWarning($"{clientEntity.FirstName} добавлен");
        return $"{clientEntity.FirstName} добавлен";
    }

    public async Task<List<ClientDto>> GetAllClientsWithMemberships(CancellationToken token)
    {
        var clients = await _clientRepository.GetAllClientWithMemberships(token);

        if (clients.Any())
        {
            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return clientsDto;
        }

        return [];
    }

    public async Task<ClientDto> GetClientById(Guid id, CancellationToken token)
    {
        var client = await _clientRepository.GetClientById(id, token);
        if (client == null)
            throw new DirectoryNotFoundException("Клиент не найден");

        var clientDto = _mapper.Map<ClientDto>(client);
        
        return clientDto;
    }

    public async Task<List<BirthdayDto>> GetAllFromBodAsync(CancellationToken token)
    {
        var person = await _birtDaysRepository.GetAllFromBodAsync(token);
        if(person != null)
        {
            var personDto = _mapper.Map<List<BirthdayDto>>(person);
            return personDto;
        }

        return [];
    }

    public async Task<List<ClientDto>> GetAllClientsAsync(CancellationToken token)
    {
        var clients = await _clientRepository.GetAllClientsAsync(token);
        var clientsDto = _mapper.Map<List<ClientDto>>(clients);

        return clientsDto ?? [];
    }

    public async Task<List<ClientDto>> GetClientsForScheduleAsync(CancellationToken token)
    {
        var clients = await _clientRepository.GetClientsForScheduleAsync(token);
        var clientsDto = _mapper.Map<List<ClientDto>>(clients);

        return clientsDto ?? [];
    }


    public async Task DeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        await _clientRepository.SoftDeleteClientAsync(id, cancellationToken);
    }

    public async Task UpdateClientInfo(ClientDto  clientDto, CancellationToken token)
    {
        var client = _mapper.Map<Client>(clientDto);
        await _clientRepository.UpdateClientInfoAsync(client, token);
    }

    public async Task<StatisticsDto> GetStatistics(CancellationToken token)
    {
        // Получаем клиентов с абонементами для статистики (включая удаленные за последние 2 месяца)
        var allClients = await _clientRepository.GetStatistics(token);
        var allTrainers = await _trainerRepository.GetTrainersAsync(token);

        var twoMonthsAgo = DateOnly.FromDateTime(DateTime.Today.AddMonths(-2));

        // Статистика по клиентам
        var totalClients = allClients.Count;
        
        // Клиенты с активными абонементами (исключая мягко удаленные)
        var clientsWithActiveMemberships = allClients.Count(c =>
            c.Memberships?.Any(m =>
                !m.IsDeleted && (
                    m.StatusMembership == StatusMembership.Active ||
                    m.IsOneTimeTraining)) == true);

        // Клиенты без активных абонементов, но с активностью за последние 2 месяца
        var clientsWithRecentActivity = allClients.Count(c =>
        {
            var hasActiveMembership = c.Memberships?.Any(m =>
                !m.IsDeleted && (
                    m.StatusMembership == StatusMembership.Active ||
                    m.IsOneTimeTraining)) == true;

            if (hasActiveMembership) return false; // Уже учтены в активных

            var hasRecentActivity = c.Memberships?.Any(m =>
                m.DateEnd >= twoMonthsAgo) == true;

            return hasRecentActivity;
        });

        // Неактивные клиенты
        var inactiveClients = totalClients - clientsWithActiveMemberships - clientsWithRecentActivity;

        // Для статистики абонементов используем только не удаленные
        var activeMembershipsOnly = allClients.SelectMany(c => c.Memberships ?? new()).Where(m => !m.IsDeleted).ToList();

        var activeMemberships = activeMembershipsOnly.Count(m => m.StatusMembership == StatusMembership.Active || m.StatusMembership == StatusMembership.OneTime);
        var oneTimeMemberships = activeMembershipsOnly.Count(m => m.IsOneTimeTraining);
        var morningMemberships = activeMembershipsOnly.Count(m => m.IsMorning);

        // Статистика по типам тренировок (только для активных абонементов)
        var trainingTypes = Enum.GetValues<TypeTrainings>().Where(t => t != TypeTrainings.Unknown).ToList();
        var totalActiveMemberships = activeMembershipsOnly.Count;
        
        var trainingTypeStats = trainingTypes
            .Select(type => new TrainingTypeStatDto
            {
                Name = GetTrainingTypeName(type),
                Count = activeMembershipsOnly.Count(m => m.TypeTrainings == type),
                Percentage = totalActiveMemberships > 0
                    ? (double)activeMembershipsOnly.Count(m => m.TypeTrainings == type) / totalActiveMemberships * 100
                    : 0
            })
            .Where(stat => stat.Count > 0) // Показываем только типы с абонементами
            .OrderByDescending(s => s.Count) // Сортируем по убыванию количества
            .ToList();

        // Получаем статистику по тренировкам из истории за весь месяц одним запросом
        var today = DateOnly.FromDateTime(DateTime.Today);
        var monthStart = new DateOnly(today.Year, today.Month, 1);
        
        // Получаем все тренировки с начала месяца по сегодня
        var monthSchedules = await _scheduleService.GetScheduleHistory(monthStart, today, token);
        
        // Рассчитываем статистику из полученных данных
        var todayTrainings = monthSchedules.Where(s => s.Date == today).Sum(s => s.Trainings?.Count ?? 0);
        
        var weekStart = today.AddDays(-(int)today.DayOfWeek + 1); // Начало текущей недели (понедельник)
        var weekTrainings = monthSchedules.Where(s => s.Date >= weekStart && s.Date <= today).Sum(s => s.Trainings?.Count ?? 0);
        
        var monthTrainings = monthSchedules.Sum(s => s.Trainings?.Count ?? 0);

        return new StatisticsDto
        {
            TotalClients = totalClients,
            ClientsWithActiveMemberships = clientsWithActiveMemberships,
            ClientsWithRecentActivity = clientsWithRecentActivity,
            InactiveClients = inactiveClients,
            ActiveMemberships = activeMemberships,
            OneTimeMemberships = oneTimeMemberships,
            MorningMemberships = morningMemberships,
            TotalTrainers = allTrainers.Count,
            ActiveTrainers = allTrainers.Count(t => !t.IsDeleted),
            TotalTrainingsThisMonth = monthTrainings,
            TodayTrainings = todayTrainings,
            WeekTrainings = weekTrainings,
            MonthTrainings = monthTrainings,
            TrainingTypeStats = trainingTypeStats
        };
    }



    private string GetTrainingTypeName(TypeTrainings type) =>
        type switch
        {
            TypeTrainings.Sport => "60 Спорт",
            TypeTrainings.Light => "60 Лайт",
            TypeTrainings.BabiesHalf => "30М",
            TypeTrainings.BabiesMixed => "30+30",
            TypeTrainings.Babies => "60М",
            TypeTrainings.Hippotherapy => "Иппотерапия",
            TypeTrainings.PhysicalTraining => "ОФП",
            TypeTrainings.Rent => "Аренда",
            TypeTrainings.Owner => "Частный владелец",
            TypeTrainings.Voltiger => "Вольтижировка",
            TypeTrainings.Group => "Группа",
            TypeTrainings.ExerciseMachine30 => "Тренажёры 30",
            TypeTrainings.ExerciseMachine45 => "Тренажёры 45",
            TypeTrainings.Unknown => "Неизвестно",
            _ => type.ToString()
        };
}
