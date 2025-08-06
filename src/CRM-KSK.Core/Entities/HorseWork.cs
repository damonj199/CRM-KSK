namespace CRM_KSK.Core.Entities;

public sealed class HorseWork
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public DateOnly StartWeek { get; set; }
    public DateOnly Date { get; set; }
    public string ContentText { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
