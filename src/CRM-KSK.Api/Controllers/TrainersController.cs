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
            string trainerNew = await _trainerService.AddTrainerAsync(trainer, cancellationToken);
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

    [HttpGet("by-name")]
    public async Task<IActionResult> GetTrainerByNameAsync(
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null, 
        CancellationToken cancellationToken = default)
    {
        var client = await _trainerService.GetTrainerAsync(firstName, lastName, cancellationToken);

        if (client == null)
            return BadRequest(new { message = "Клиент не найден" });

        return Ok(client);
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
