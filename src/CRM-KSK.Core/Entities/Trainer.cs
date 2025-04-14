using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Trainer : IUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Color { get; set; } = "#FFFFFF";
    public Roles Role { get; set; } = Roles.Trainer;
    public string PasswordHash { get; set; }
    public bool IsDeleted { get; set; } = false;

    public List<Training> Trainings { get; set; } = [];

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}
