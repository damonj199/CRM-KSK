using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Schedule
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string? Description { get; set; }
    public Trainer TrainerName { get; set; }
    public Client ClientName { get; set; }
}
