using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] ClientDto client, CancellationToken cancellationToken)
    {
        try
        {
            var clientNew = await _clientService.AddClientAsync(client, cancellationToken);
            return Ok(new { message = $"Клиент {client.FirstName}, успешно добавлен" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("search-by-name")]
    public async Task<IActionResult> GetClientByName(
        [FromQuery] string? firstName = null, 
        [FromQuery] string? lastName = null, 
        CancellationToken cancellationToken = default)
    {
        var client = await _clientService.GetClientByNameAsync(firstName, lastName, cancellationToken);

        if (client == null)
            return BadRequest(new { message = "Клиент не найден" });
        
        return Ok(client);
    }

    [HttpDelete("/{Phone}")]
    public async Task<IActionResult> DeleteClient(string phone, CancellationToken cancellationToken)
    {
        try
        {
            await _clientService.DeleteClientAsync(phone, cancellationToken);
            return Ok(new { message = "Удалено"});
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }
}
