namespace CRM_KSK.Core.Entities;

public class ScheduleComment
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public string CommentText { get; set; } = string.Empty;
}
