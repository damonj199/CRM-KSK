namespace CRM_KSK.Core.Entities;

public class Client
{
    public Guid Id { get; init; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? ParentName { get; set; }
    public string? ParentPhone { get; set; }
    public DateTime DateRegister { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public List<Membership> Memberships { get; set; } = [];
    public List<Training> Trainings { get; set; } = [];

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}
