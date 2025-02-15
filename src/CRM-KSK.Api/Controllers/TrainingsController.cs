using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

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
    public async Task<IActionResult> AddTrainingAsync([FromBody] TrainingDto trainingDto, CancellationToken token)
    {
        await _trainingService.AddTrainingAsync(trainingDto, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrainingAsync(Guid id, CancellationToken token)
    {
        await _trainingService.DeleteTrainingAsync(id, token);
        return Ok();
    }
}
