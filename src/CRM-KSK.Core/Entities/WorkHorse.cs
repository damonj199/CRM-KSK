namespace CRM_KSK.Core.Entities;

public class WorkHorse
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public string HorseName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string ContentText { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
