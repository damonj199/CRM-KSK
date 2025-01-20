using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_KSK.Core.Entities;

public class Schedule
{
    public Guid Id { get; set; }
    public string? Day { get; set; }
    public DateTime Time { get; set; }
    public string? Description { get; set; }
    public string TrainerName { get; set; }
    public string ClientName { get; set; }
    public string TrainingType { get; set; }
}
