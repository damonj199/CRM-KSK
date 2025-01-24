using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Models;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainerService
{
    Task AddTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken);
    Task DeleteTrainer(SearchByNameRequest nameRequest, CancellationToken cancellationToken);
    Task<TrainerDto> GetTrainerAsync(SearchByNameRequest nameRequest, CancellationToken cancellationToken);
}