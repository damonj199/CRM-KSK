namespace CRM_KSK.Core.Entities;

public class Trainer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string? Specialization { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public ICollection<Client> Clients { get; set; } = [];
    public ICollection<Schedule> Schedules { get; set; } = [];
}
