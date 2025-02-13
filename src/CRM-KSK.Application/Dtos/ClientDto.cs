using CRM_KSK.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class ClientDto
{
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public TrainerDto Trainer { get; set; }

    public string? ParentName { get; set; }

    public string? ParentPhone { get; set; }

}
