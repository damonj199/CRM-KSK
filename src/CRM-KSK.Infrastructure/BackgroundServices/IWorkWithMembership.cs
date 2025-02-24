
namespace CRM_KSK.Infrastructure.BackgroundServices
{
    public interface IWorkWithMembership
    {
        Task DeductTrainingsFromMemberships(CancellationToken token);
    }
}