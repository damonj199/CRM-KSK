using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Authorize(Policy = "AdminPolicy")]
[Controller]
[Route("api/[controller]")]
public class TrainingsController : ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TrainingsController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> AddTrainingAsync([FromBody] ScheduleFullDto scheduleFull, CancellationToken token)
    {
        await _trainingService.AddTrainingAsync(scheduleFull, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrainingAsync(Guid id, CancellationToken token)
    {
        await _trainingService.DeleteTrainingAsync(id, token);
        return Ok();
    }
}
