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

    [HttpPost("transfer-last-week")]
    public async Task<IActionResult> AddHorsesLastWeek([FromBody] DateOnly sDate, CancellationToken token)
    {
        await _horsesWorkService.AddHorsesLastWeek(sDate, token);
        return Ok();
    }

    [HttpPost("work")]
    public async Task<IActionResult> AddWorkHorses([FromBody] WorkHorseDto horse, CancellationToken token)
    {
        var result = await _horsesWorkService.AddWorkHorse(horse, token);

        if (result)
        {
            return Ok("Запись добавлена");
        }
        else
        {
            return BadRequest("Ошибка при добавлении");
        }
    }

    [HttpPost("horse")]
    public async Task<IActionResult> AddHorse([FromBody] HorseDto horse, CancellationToken token)
    {
        await _horsesWorkService.AddHorse(horse, token);
        return Ok();
    }

    [HttpGet("horses-week")]
    public async Task<IActionResult> GetHorsesNameWeek([FromQuery] DateOnly sDate, CancellationToken token)
    {
        var horsesWeek = await _horsesWorkService.GetHorsesNameWeek(sDate, token);
        return Ok(horsesWeek);
    }

    [HttpGet("work-all")]
    public async Task<IActionResult> GetAllScheduleWorkHorses(CancellationToken token)
    {
        var allSchedule = await _horsesWorkService.GetAllScheduleWorkHorses(token);
        return Ok(allSchedule);
    }

    [HttpGet("work-horses-week")]
    public async Task<IActionResult> GetScheduleWorkHorsesWeek([FromQuery] DateOnly weekStart, CancellationToken token)
    {
        var week = await _horsesWorkService.GetScheduleWorkHorsesWeek(weekStart, token);
        return Ok(week);
    }

    [HttpPatch("name")]
    public async Task<IActionResult> UpdateHorseName([FromBody] UpdateHorseNameDto dto, CancellationToken token)
    {
        await _horsesWorkService.UpdateHorseName(dto.Id, dto.Name, token);
        return Ok();
    }

    [HttpPatch("work")]
    public async Task<IActionResult> UpdateWorkHorse([FromBody] UpdateWorkHorse dto, CancellationToken token)
    {
        await _horsesWorkService.UpdateWorkHorse(dto.Id, dto.Content, token);
        return Ok();
    }

    [HttpDelete("name/{id:long}")]
    public async Task<IActionResult> DeleteHorse(long id, CancellationToken token)
    {
        var result = await _horsesWorkService.DeleteHorseById(id, token);

        if (result)
        {
            return Ok("Запись удалена");
        }
        else
        {
            return BadRequest("Ошибка удаления, проверьте данные");
        }
    }

    [HttpDelete("work/{id:guid}")]
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
