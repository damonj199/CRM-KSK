namespace CRM_KSK.Core.Entities;

public class Admin
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
