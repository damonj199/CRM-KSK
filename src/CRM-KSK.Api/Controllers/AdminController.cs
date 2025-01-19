using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly HttpContext _context;

    public AdminController(IAdminService adminService, HttpContext context)
    {
        _adminService = adminService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromQuery] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        await _adminService.Register(registerRequest, cancellationToken);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var token = await _adminService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);

        _context.Response.Cookies.Append("jwt", token);
        return Ok(token);
    }
}
