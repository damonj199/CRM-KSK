namespace CRM_KSK.Application.Interfaces;

public interface IProcessBirthdays
{
    Task ProcessBodAsync(CancellationToken token);
}