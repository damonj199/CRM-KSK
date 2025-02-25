using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Models;

public record LoginRequest
{
    [Required] 
    public string Phone { get; set; }

    [Required] 
    public string Password { get; set; }
}
    

