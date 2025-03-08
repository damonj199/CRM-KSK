using CRM_KSK.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class TrainerDto
{
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Surname { get; set; }

    public string Phone { get; set; }

    public Roles Role { get; set; } = Roles.Trainer;

    [Required]
    public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public string Color { get; set; } = default!;
}
