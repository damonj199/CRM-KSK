using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Models;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password);
