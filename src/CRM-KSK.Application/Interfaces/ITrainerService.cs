using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainerService
{
    Task AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken);
    Task DeleteTrainer(Guid id, CancellationToken cancellationToken);
    Task<TrainerDto> GetTrainerByIdAsync(Guid id, CancellationToken token);
    Task<TrainerDto> GetTrainerByName(string firstName, string lastName, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainerDto>> GetTrainersAsync(CancellationToken cancellationToken);
    Task UpdateTrainerInfoAsync(TrainerDto trainerDto, CancellationToken token);
}