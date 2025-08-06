namespace CRM_KSK.Application.Dtos;

public class HorseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public DateOnly StartWeek { get; set; }
}
