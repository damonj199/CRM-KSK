namespace CRM_KSK.Application.Dtos;

public class ScheduleDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public ICollection<TrainingDto> Trainings { get; set; } = [];
}
