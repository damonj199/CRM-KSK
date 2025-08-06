namespace CRM_KSK.Application.Dtos;

public class WorkHorseDto
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public DateOnly StartWeek { get; set; }
    public DateOnly Date { get; set; }
    public string ContentText { get; set; } = string.Empty;
}