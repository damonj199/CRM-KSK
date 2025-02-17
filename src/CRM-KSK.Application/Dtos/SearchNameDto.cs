using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Application.Dtos;

public class SearchNameDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}