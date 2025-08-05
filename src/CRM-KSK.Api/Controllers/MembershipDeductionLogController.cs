using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet("client/{clientId}")]
    public async Task<ActionResult<IEnumerable<MembershipDeductionLogDto>>> GetLogsByClient(Guid clientId, CancellationToken token)
    {
        var logs = await _logService.GetDeductionLogsByClientAsync(clientId, token);
        return Ok(logs);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetLogsCount([FromQuery] DateOnly date, CancellationToken token)
    {
        var count = await _logService.GetDeductionLogsCountAsync(date, token);
        return Ok(count);
    }
} 