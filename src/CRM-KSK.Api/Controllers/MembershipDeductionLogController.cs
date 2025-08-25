using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MembershipDeductionLogController : ControllerBase
{
    private readonly IMembershipDeductionLogService _logService;
    private readonly ILogger<MembershipDeductionLogController> _logger;

    public MembershipDeductionLogController(IMembershipDeductionLogService logService, ILogger<MembershipDeductionLogController> logger)
    {
        _logService = logService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipDeductionLogDto>>> GetLogs([FromQuery] DateOnly date, CancellationToken token)
    {
        var logs = await _logService.GetDeductionLogsAsync(date, token);
        return Ok(logs);
    }
}