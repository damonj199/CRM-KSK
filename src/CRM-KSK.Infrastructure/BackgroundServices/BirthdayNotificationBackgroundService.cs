using CRM_KSK.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Infrastructure.BackgroundServices;

public sealed class BirthdayNotificationBackgroundService : BackgroundService
{
    private readonly ILogger<BirthdayNotificationBackgroundService> _logger;
    private readonly IServiceProvider _service;

    public BirthdayNotificationBackgroundService(ILogger<BirthdayNotificationBackgroundService> logger,
        IServiceProvider service)
    {
        _logger = logger;
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("BackgroundService запустился");

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
                //var delay = TimeSpan.FromMinutes(2); //для тестирования

                await Task.Delay(delay, stoppingToken);

                using(var scope = _service.CreateScope())
                {
                    var processBirthdays = scope.ServiceProvider.GetRequiredService<IProcessBirthdays>();
                    var workWithMembership = scope.ServiceProvider.GetRequiredService<IWorkWithMembership>();

                    await processBirthdays.ProcessBodAsync(stoppingToken);
                    await workWithMembership.DeductTrainingsFromMemberships(stoppingToken);
                }
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
        _logger.LogWarning("Background service завершает работу.");
    }
}
