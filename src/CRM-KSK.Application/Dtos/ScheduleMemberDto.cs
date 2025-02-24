namespace CRM_KSK.Application.Dtos;

public record ScheduleMemberDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Color { get; set; } = default!;
}

