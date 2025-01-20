using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Dtos;

public class ClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ParentName {  get; set; }
    public string? ParentPhone { get; set; }

}
