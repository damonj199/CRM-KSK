using CRM_KSK.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetScheduleAsync()
    {
        var schedules = new List<ScheduleDto>
        {
            new ScheduleDto { Day = "Понедельник", Time = DateTime.Now, ClientName = "Ivanov Ivan",
                TrainerName = "Dariy Andreevna", TrainingType = "Individual'na", Description = "На манеже" }
        };
        return Ok(schedules);
    }
}
