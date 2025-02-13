using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Training
{
    public Guid Id { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;
    public Trainer Trainer { get; set; } = default!;
    public ICollection<Client> Clients { get; set; } = [];
}
