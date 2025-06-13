using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HorsesWorkController : ControllerBase
{
    private readonly IHorsesWorkService _horsesWorkService;

    public HorsesWorkController(IHorsesWorkService horsesWorkService)
    {
        _horsesWorkService = horsesWorkService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkHorses([FromBody] WorkHorseDto horse, CancellationToken token)
    {
        var result = await _horsesWorkService.AddHorsesWork(horse, token);

        if (result)
        {
            return Ok("Запись добавлена");
        }
        else
        {
            return BadRequest("Ошибка при добавлении");
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllScheduleWorkHorses(CancellationToken token)
    {
        var allSchedule = await _horsesWorkService.GetAllScheduleWorkHorses(token);
        return Ok(allSchedule);
    }

    [HttpGet("week")]
    public async Task<IActionResult> GetScheduleWorkHorsesWeek([FromQuery] DateOnly startWeek, CancellationToken token)
    {
        var week = await _horsesWorkService.GetScheduleWorkHorsesWeek(startWeek, token);
        return Ok(week);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWorkHorse(Guid id, CancellationToken token)
    {
        var result = await _horsesWorkService.DeleteWorkHorseById(id, token);

        if (result)
        {
            return Ok("Запись удалена");
        }
        else
        {
            return BadRequest("Ошибка удаления, проверьте данные");
        }
    }
}
