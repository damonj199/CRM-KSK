namespace CRM_KSK.Application.Dtos;

public class UpdatePasswordDto
{
    public Guid Id { get; set; }
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
