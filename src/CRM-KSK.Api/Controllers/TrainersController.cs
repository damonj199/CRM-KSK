using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM_KSK.Api.Controllers;

[Authorize]
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
    [Authorize(Policy = "AdminPolicy")]
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrainerByIdAsync(Guid id, CancellationToken token)
    {
        var trainerDto = await _trainerService.GetTrainerByIdAsync(id, token);
        if(trainerDto != null)
            return Ok(trainerDto);

        return NotFound();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateTrainerInfo([FromBody] TrainerDto trainerDto, CancellationToken token)
    {
        if(User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("Не удалось определить пользователя");

            if (trainerDto.Id.ToString() != userId)
                return Forbid("Вы можете редактировать только свой профиль");
        }

        await _trainerService.UpdateTrainerInfoAsync(trainerDto, token);

        return Ok(new { message = $"Данный для {trainerDto.FirstName} обновлены"});
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteTrainerAsync(Guid id, CancellationToken cancellationToken)
    {
        await _trainerService.DeleteTrainer(id, cancellationToken);
        return Ok(new { message = "Удалено" });
    }
}
