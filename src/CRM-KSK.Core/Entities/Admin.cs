using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Admin : IUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public string Phone { get; set; }
    public Roles Role { get; set; } = Roles.Admin;
}
