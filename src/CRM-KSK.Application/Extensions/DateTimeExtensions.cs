namespace CRM_KSK.Application.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset StartOfWeek(this DateTimeOffset date, DayOfWeek startOfWeek)
    {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-diff).Date;
    }
}
