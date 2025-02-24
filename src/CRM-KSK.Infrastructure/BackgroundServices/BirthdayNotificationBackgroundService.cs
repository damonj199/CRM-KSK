using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Infrastructure.BackgroundServices;

public sealed class BirthdayNotificationBackgroundService : BackgroundService
{
    private readonly ILogger<BirthdayNotificationBackgroundService> _logger;
    private readonly IProcessBirthdays _processBirthdays;
    private readonly IWorkWithMembership _workWithMembership;

    public BirthdayNotificationBackgroundService(ILogger<BirthdayNotificationBackgroundService> logger,
        IProcessBirthdays processBirthdays, IWorkWithMembership workWithMembership)
    {
        _logger = logger;
        _processBirthdays = processBirthdays;
        _workWithMembership = workWithMembership;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundService запустился");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var dateTimeNow = DateTime.Now;
                var targetTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, 1, 0, 0);
                
                if (dateTimeNow > targetTime)
                {
                    targetTime = targetTime.AddDays(1);
                }
                var delay = targetTime - dateTimeNow;

                await Task.Delay(delay, stoppingToken);

                await _processBirthdays.SeedClients(stoppingToken);

                await _processBirthdays.ProcessBodAsync(stoppingToken);

                await _workWithMembership.DeductTrainingsFromMemberships(stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в выполнении синхронизации");
            }
        }
        _logger.LogInformation("Background service завершает работу.");
    }
}
