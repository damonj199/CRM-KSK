using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class MembershipDto
{
    public Guid Id { get; set; }
    public DateOnly DateStart { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly DateEnd { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));
    public int AmountTraining { get; set; } = 1;
    public StatusMembership StatusMembership { get; set; } = StatusMembership.Active;
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;
    public bool IsOneTimeTraining { get; set; } = false;
    public bool IsMorning { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
       
    public Guid ClientId { get; set; }
    public ClientDto? Client { get; set; }
}
