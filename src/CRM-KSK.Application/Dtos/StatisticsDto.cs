namespace CRM_KSK.Application.Dtos;

public class StatisticsDto
{
    // Статистика по клиентам
    public int TotalClients { get; set; }
    public int ClientsWithActiveMemberships { get; set; }
    public int ClientsWithRecentActivity { get; set; }
    public int InactiveClients { get; set; }

    // Статистика по абонементам
    public int ActiveMemberships { get; set; }
    public int OneTimeMemberships { get; set; }
    public int MorningMemberships { get; set; }

    // Статистика по тренерам
    public int TotalTrainers { get; set; }
    public double AverageTrainerWorkload { get; set; } // Средняя загруженность тренера в месяц
    public string MostBusyTrainer { get; set; } = string.Empty; // Самый загруженный тренер
    public string LeastBusyTrainer { get; set; } = string.Empty; // Наименее загруженный тренер

    // Статистика по тренировкам
    public int YesterdayTrainings { get; set; }
    public int WeekTrainings { get; set; }
    public int MonthTrainings { get; set; }

    // Статистика по типам тренировок
    public List<TrainingTypeStatDto> TrainingTypeStats { get; set; } = new();
}

public class TrainingTypeStatDto
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

