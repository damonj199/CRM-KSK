using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class Training
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Date { get; set; }
    public Trainer Trainer { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Individual;
    public ICollection<Client> Clients { get; set; } = [];
}
