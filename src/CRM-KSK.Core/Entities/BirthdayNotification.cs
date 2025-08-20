namespace CRM_KSK.Core.Entities;

public class BirthdayNotification
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string PersonType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
}
