namespace CRM_KSK.Core.Entities;

public class Schedule
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public ICollection<Training> Trainings { get; set; } = [];
}
