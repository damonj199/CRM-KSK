using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainerRepository
{
    Task AddTrainerAsync(Trainer trainer, CancellationToken cancellationToken);
    Task<IReadOnlyList<Trainer>> GetTrainersAsync(CancellationToken cancellationToken);
    Task<Trainer> GetTrainerByNameAsync(string firstName, string lastName, CancellationToken cancellationToken);
    Task DeleteTraner(Guid id, CancellationToken cancellationToken);
    Task<Trainer> GetTrainerByIdAsync(Guid id, CancellationToken token);
    Task UpdateTrainerInfoAsync(Trainer trainer, CancellationToken token);
    Task<Trainer> GetTrainerByPhone(string phone, CancellationToken token);
}