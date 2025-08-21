using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Membership
{
    public Guid Id { get; set; }
    public DateOnly DateStart { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly DateEnd { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));
    public int AmountTraining { get; set; }
    public StatusMembership StatusMembership { get; set; } = StatusMembership.Active;
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;
    public bool IsOneTimeTraining { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    public Guid ClientId { get; set; }
    public Client Client { get; set; } = default!;

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}
