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
    public DateTime DateRegister { get; set; } = DateTime.Now;

    public List<Membership> Memberships { get; set; } = [];
    public List<Training> Trainings { get; set; } = [];
}
