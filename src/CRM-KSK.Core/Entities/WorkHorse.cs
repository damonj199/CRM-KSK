namespace CRM_KSK.Core.Entities;

public class WorkHorse
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public DateOnly WeekStartDate { get; private set; }
    public DateOnly Date { get; set; }
    public string ContentText { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    public DateOnly GetCurrentWeekMonday(DateOnly date)
    {
        var dateTime = date.ToDateTime(TimeOnly.MinValue);
        int diff = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;
        return DateOnly.FromDateTime(dateTime.AddDays(-diff));
    }
}
