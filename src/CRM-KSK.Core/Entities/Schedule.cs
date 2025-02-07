using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Schedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Индивидуальная;
    public Trainer Trainer { get; set; } = default!;
    public ICollection<Client> Clients { get; set; } = [];
}
