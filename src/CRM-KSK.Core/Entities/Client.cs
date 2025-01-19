namespace CRM_KSK.Core.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Parent Parent { get; set; }
    public Trainer? Trainer { get; set; }

}
