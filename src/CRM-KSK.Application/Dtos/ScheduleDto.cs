using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Dtos;

public class ScheduleDto
{
    public string Day {  get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
    public string TrainerName { get; set; }
    public string ClientName { get; set; }
    public string TrainingType { get; set; }
}
