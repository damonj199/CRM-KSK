
namespace CRM_KSK.Infrastructure
{
    public interface IProcessBirthdays
    {
        Task ProcessBodAsync(CancellationToken token);
    }
}