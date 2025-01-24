using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class TrainerDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Surname { get; set; }

    public string Phone { get; set; }

    public string? Specialization { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }
}
