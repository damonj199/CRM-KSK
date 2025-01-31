using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainerService
{
    Task<string> AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken);
    Task DeleteTrainer(string firstName, string lastName, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainerDto>> GetTrainerAsync(string firstName, string lastName, CancellationToken cancellationToken);
}