using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("week")]
    public async Task<ActionResult<List<ScheduleDto>>> GetWeeksScheduleAsync(CancellationToken cancellationToken)
    {
        var schedules = await _scheduleService.GetWeeksSchedule(cancellationToken);
        return Ok(schedules);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateScheduleAsync([FromBody] ScheduleDto scheduleDto, CancellationToken cancellationToken)
    {
        await _scheduleService.AddOrUpdateSchedule(scheduleDto, cancellationToken);
        return Ok();
    }
}
