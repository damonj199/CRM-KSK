using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
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
    public IActionResult AddTrainer([FromBody] TrainerDto trainer, CancellationToken cancellationToken)
    {
        try
        {
            var trainerNew = _trainerService.AddTrainerAsync(trainer, cancellationToken);
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
    public async Task<IActionResult> GetTrainerByNameAsync([FromHeader] SearchByNameRequest searchByName, CancellationToken cancellationToken)
    {
        var client = await _trainerService.GetTrainerAsync(searchByName, cancellationToken);

        if (client == null)
            return BadRequest(new { message = "Клиент не найден" });

        return Ok(client);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClientAsync(SearchByNameRequest nameRequest, CancellationToken cancellationToken)
    {
        try
        {
            await _trainerService.DeleteTrainer(nameRequest, cancellationToken);
            return Ok(new { message = "Удалено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
