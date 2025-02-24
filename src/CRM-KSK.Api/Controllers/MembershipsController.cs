using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipsController : ControllerBase
{
    private readonly IMembershipService _membershipService;

    public MembershipsController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    [HttpPost]
    public async Task<IActionResult> AddMembershipAsync([FromBody] List<MembershipDto> membership, CancellationToken token)
    {
        await _membershipService.AddMembershipAsync(membership, token);
        return Ok();
    }

    [HttpGet("all-of-client/{id:guid}")]
    public async Task<IActionResult> GetAllMembershipClientAsync(Guid id, CancellationToken token)
    {
        var membershipsDtos = await _membershipService.GetAllMembershipClientAsync(id, token);
        return Ok(membershipsDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMembershipByIdAsync(Guid id, CancellationToken token)
    {
        var membership = await _membershipService.GetMembershipByIdAsync(id, token);
        return Ok(membership);
    }

    [HttpGet("expiring")]
    public async Task<IActionResult> GetExpiringMemberships(CancellationToken token)
    {
        var membershipsDto = await _membershipService.GetExpiringMembershipsAsync(token);
        return Ok(membershipsDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMembershipAsync([FromBody] MembershipDto membership, CancellationToken token)
    {
        await _membershipService.UpdateMembershipAsync(membership, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMembershipAsync(Guid id, CancellationToken token)
    {
        await _membershipService.DeleteMembershipAsync(id, token);
        return Ok();
    }
}
