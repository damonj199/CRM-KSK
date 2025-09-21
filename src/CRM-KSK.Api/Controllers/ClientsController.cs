using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[Authorize]
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
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> AddClient([FromBody] ClientDto client, CancellationToken cancellationToken)
    {
        var clientNew = await _clientService.AddClientAsync(client, cancellationToken);

        if(clientNew != null)
        {
            return Ok();
        }
        else
        {
            return BadRequest(new { message = "Ошибка при добавлении" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients(CancellationToken token)
    {
        var clients = await _clientService.GetAllClientsAsync(token);
        return Ok(clients);
    }

    [HttpGet("with-memberships")]
    public async Task<IActionResult> GetAllClientsWithMemberships(CancellationToken token)
    {
        var clients = await _clientService.GetAllClientsWithMemberships(token);
        return Ok(clients);
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics(CancellationToken token)
    {
        var statistics = await _clientService.GetStatistics(token);
        return Ok(statistics);
    }

    [HttpGet("for-schedule")]
    public async Task<IActionResult> GetClientsForSchedules(CancellationToken token)
    {
        var clients = await _clientService.GetClientsForScheduleAsync(token);
        return Ok(clients);
    }


    [HttpGet("birthdays")]
    public async Task<IActionResult> GetAllFromBod(CancellationToken token)
    {
        var birthdays = await _clientService.GetAllFromBodAsync(token);
        return Ok(birthdays);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClientById(Guid id, CancellationToken token)
    {
        var clientDto = await _clientService.GetClientById(id, token);
        if (clientDto != null)
            return Ok(clientDto);

        return NotFound("Клиент не найден");
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateClientInfo([FromBody] ClientDto clientDto, CancellationToken token)
    {
        await _clientService.UpdateClientInfo(clientDto, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteClient(Guid id, CancellationToken cancellationToken)
    {
        await _clientService.DeleteClientAsync(id, cancellationToken);
        return Ok(new { message = "Удалено" });
    }
}
