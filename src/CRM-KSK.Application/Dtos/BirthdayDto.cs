namespace CRM_KSK.Application.Dtos;

public class BirthdayDto
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string PersonType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateOnly Birthday { get; set; }
}
