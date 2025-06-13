namespace CRM_KSK.Application.Dtos;

public class WorkHorseDto
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public string HorseName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string ContentText { get; set; } = string.Empty;
}