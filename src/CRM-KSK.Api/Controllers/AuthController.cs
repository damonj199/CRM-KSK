using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    //[Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> Register([FromQuery] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(registerRequest, cancellationToken);
        if (result.Succeeded)
        {
            return Ok(new { message = "Пользователь успешно зарегистрирован!" });
        }

        return Conflict(new { message = result.ErrorMessage });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(loginRequest, cancellationToken);

        if (result.Succeeded)
        {
            HttpContext.Response.Cookies.Append("jwt", result.Token);
            return Ok(new { result.Token });
        }
        return Unauthorized(new { message = result.ErrorMessage });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        
        return Ok();
    }

    [HttpPatch("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto update, CancellationToken token)
    {
        var result = await _authService.UpdatePasswordAsync(update, token);

        if(result == "Успешно")
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }

    }
}
