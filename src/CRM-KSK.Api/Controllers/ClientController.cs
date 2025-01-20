using CRM_KSK.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CRM_KSK.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ClientsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddClientAsync([FromBody] ClientDto client)
    {
        // Логика добавления клиента в базу данных
        return Ok();
    }
}
