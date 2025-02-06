using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTrainer([FromBody] TrainerDto trainer, CancellationToken cancellationToken)
    {
        try
        {
            await _trainerService.AddTrainerAsync(trainer, cancellationToken);
            return Ok(new { message = $"Тренер {trainer.FirstName}, успешно добавлен" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTrainersAsync(CancellationToken cancellationToken)
    {
        var trainers = await _trainerService.GetTrainersAsync(cancellationToken);

        if (trainers == null)
            return BadRequest(new { message = "Тренеры не найден" });

        return Ok(trainers);
    }

    [HttpGet("by-name")]
    public async Task<IActionResult> GetTrainerByName(
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null,
        CancellationToken cancellationToken = default)
    {
        var trainer = await _trainerService.GetTrainerByName(firstName, lastName, cancellationToken);
        if (trainer == null)
            return BadRequest(new { message = "тренер не найден" });

        return Ok(trainer);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClientAsync([FromQuery] string firstName, string lastName, CancellationToken cancellationToken)
    {
        try
        {
            await _trainerService.DeleteTrainer(firstName, lastName, cancellationToken);
            return Ok(new { message = "Удалено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
