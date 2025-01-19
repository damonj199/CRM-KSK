namespace CRM_KSK.Core.Entities;

public class Training
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public Trainer Trainer { get; set; }
    public ICollection<Client> Clients { get; set; } = [];
}
