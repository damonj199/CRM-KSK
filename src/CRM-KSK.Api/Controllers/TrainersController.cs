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
        await _trainerService.AddTrainerAsync(trainer, cancellationToken);
        return Ok(new { message = $"Тренер {trainer.FirstName}, успешно добавлен" });
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
    public async Task<IActionResult> GetTrainerByName([FromQuery] SearchNameDto nameDto, CancellationToken cancellationToken)
    {
        var trainer = await _trainerService.GetTrainerByName(nameDto.FirstName, nameDto.LastName, cancellationToken);
        if (trainer == null)
            return NotFound(new { message = "тренер не найден" });

        return Ok(trainer);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTrainerAsync([FromQuery] string firstName, string lastName, CancellationToken cancellationToken)
    {
        await _trainerService.DeleteTrainer(firstName, lastName, cancellationToken);
        return Ok(new { message = "Удалено" });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrainerByIdAsync(Guid id, CancellationToken token)
    {
        var trainerDto = await _trainerService.GetTrainerByIdAsync(id, token);
        if(trainerDto != null)
            return Ok(trainerDto);

        return NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTrainerInfo([FromBody] TrainerDto trainerDto, CancellationToken token)
    {
        await _trainerService.UpdateTrainerInfoAsync(trainerDto, token);
        return Ok();
    }
}
