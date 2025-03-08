
namespace CRM_KSK.Application.Interfaces;

public interface IWorkWithMembership
{
    Task DeductTrainingsFromMemberships(CancellationToken token);
}