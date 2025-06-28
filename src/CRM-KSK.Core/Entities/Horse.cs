namespace CRM_KSK.Core.Entities;

public sealed class Horse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public DateOnly StartWeek { get; set; }

    //private static DateOnly GetCurrentWeekMonday()
    //{
    //    DateTime today = DateTime.Today;

    //    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
    //    DateTime monday = today.AddDays(-diff);
    //    return DateOnly.FromDateTime(monday);
    //}
}
