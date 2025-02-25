using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminsController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminsController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> Register([FromQuery] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _adminService.RegisterAsync(registerRequest, cancellationToken);
        if(result.Succeeded)
        {
            return Ok(new { message = "Пользователь успешно зарегистрирован!"});
        }

        return Conflict(new { message = result.ErrorMessage });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var result = await _adminService.LoginAsync(loginRequest, cancellationToken);

        if (result.Succeeded)
        {
            HttpContext.Response.Cookies.Append("jwt", result.Token);
            return Ok(new { token = result.Token });
        }

        return Unauthorized(new { message = result.ErrorMessage });
    }
}
