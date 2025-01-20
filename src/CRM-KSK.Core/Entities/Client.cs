namespace CRM_KSK.Core.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ParentName { get; set; }
    public string? ParentPhone { get; set; }
    public Trainer? Trainer { get; set; }
    public ICollection<Schedule> Schedules { get; set; } = [];

}
