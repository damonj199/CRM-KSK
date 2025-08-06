namespace CRM_KSK.Core.Entities;

public sealed class Horse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public DateOnly StartWeek { get; set; }
}
