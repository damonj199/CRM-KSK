using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Training
{
    public Guid Id { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;

    public Guid TrainerId { get; set; }
    public Trainer Trainer { get; set; } = default!;
    public ICollection<Client> Clients { get; set; } = [];
    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = default!;
}
