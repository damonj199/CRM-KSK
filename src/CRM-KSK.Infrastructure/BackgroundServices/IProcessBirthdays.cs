namespace CRM_KSK.Infrastructure.BackgroundServices
{
    public interface IProcessBirthdays
    {
        Task ProcessBodAsync(CancellationToken token);
        Task SeedClients(CancellationToken token);
    }
}