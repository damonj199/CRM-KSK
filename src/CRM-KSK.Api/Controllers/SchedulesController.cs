using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost("comment")]
    public async Task<IActionResult> AddScheduleComment([FromBody] ScheduleCommentDto commentDto, CancellationToken token)
    {
        await _scheduleService.AddScheduleComment(commentDto, token);
        return Ok();
    }

    [HttpGet("comments")]
    public async Task<ActionResult<List<ScheduleCommentDto>>> GetScheduleComments(CancellationToken token)
    {
        var comments = await _scheduleService.GetScheduleComments(token);
        return Ok(comments);
    }

    [HttpGet("week")]
    public async Task<ActionResult<List<ScheduleDto>>> GetWeeksScheduleAsync([FromQuery] DateOnly weekStart, CancellationToken cancellationToken)
    {
        var schedules = await _scheduleService.GetWeeksSchedule(weekStart, cancellationToken);
        return Ok(schedules);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetScheduleHistory([FromQuery] DateOnly start, [FromQuery] DateOnly end, CancellationToken cancellationToken)
    {
        var history = await _scheduleService.GetScheduleHistory(start, end, cancellationToken);
        return Ok(history);
    }

    [HttpDelete("comment/{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteComment(int id, CancellationToken token)
    {
        await _scheduleService.DeleteComment(id, token);
        return Ok();
    }
}
