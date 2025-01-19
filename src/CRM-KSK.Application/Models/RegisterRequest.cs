using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Models;

public record RegisterRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Password,
    [Required] string Email);

