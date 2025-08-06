using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MembershipDeductionLogController : ControllerBase
{
    private readonly IMembershipDeductionLogService _logService;

    public MembershipDeductionLogController(IMembershipDeductionLogService logService)
    {
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipDeductionLogDto>>> GetLogs([FromQuery] DateOnly date, CancellationToken token)
    {
        var logs = await _logService.GetDeductionLogsAsync(date, token);
        return Ok(logs);
    }
}