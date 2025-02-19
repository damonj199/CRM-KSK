using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainingRepository
{
    Task AddTainingAsync(Training training, CancellationToken token);
    Task DeleteTrainingAsync(Guid id, CancellationToken token);
}
