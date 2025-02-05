using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Membership
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateOnly DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public StatusMembership StatusMembership { get; set; } = StatusMembership.Активный;
    public Client Client { get; set; }
}
