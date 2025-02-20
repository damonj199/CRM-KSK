using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
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

    [HttpGet("by-name")]
    public async Task<IActionResult> GetClientByName(
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null,
        CancellationToken cancellationToken = default)
    {
        var client = await _clientService.GetClientByNameAsync(firstName, lastName, cancellationToken);

        if (client == null)
            return NotFound(new { message = "Клиент не найден" });

        return Ok(client);
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
    public async Task<IActionResult> UpdateClientInfo([FromBody] ClientDto clientDto, CancellationToken token)
    {
        await _clientService.UpdateClientInfo(clientDto, token);
        return Ok();
    }

    [HttpDelete("{Phone}")]
    public async Task<IActionResult> DeleteClient(string phone, CancellationToken cancellationToken)
    {
        await _clientService.DeleteClientAsync(phone, cancellationToken);
        return Ok(new { message = "Удалено" });
    }
}
