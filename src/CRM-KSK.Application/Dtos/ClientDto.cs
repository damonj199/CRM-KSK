using CRM_KSK.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class ClientDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    public LevelOfTraining LevelOfTraining { get; set; } = LevelOfTraining.Newbie;

    public string? TrainerName { get; set; }

    public string? ParentName { get; set; }

    public string? ParentPhone { get; set; }

}
