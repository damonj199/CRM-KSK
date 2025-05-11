namespace CRM_KSK.Application.Dtos;

public class ScheduleCommentDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public string CommentText { get; set; } = string.Empty;
}
