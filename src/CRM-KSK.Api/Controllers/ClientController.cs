using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]/")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public IActionResult AddClient([FromBody] ClientDto client, CancellationToken cancellationToken)
    {
        try
        {
            var clientNew = _clientService.AddClientAsync(client, cancellationToken);
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

    [HttpGet]
    public async Task<IActionResult> GetClientByName([FromHeader]SearchByNameRequest searchByName, CancellationToken cancellationToken)
    {
        var client = await _clientService.GetClientByNameAsync(searchByName, cancellationToken);

        if (client == null)
        {
            return BadRequest(new { message = "Клиент не найден" });
        }
        return Ok(client);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClient(string phoneNumber, CancellationToken cancellationToken)
    {
        try
        {
            await _clientService.DeleteClientAsync(phoneNumber, cancellationToken);
            return Ok(new { message = "Удалено"});
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }
}
