using CRM_KSK.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class AdminDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Phone { get; set; }

    public Roles Role { get; set; } = Roles.Admin;
}
