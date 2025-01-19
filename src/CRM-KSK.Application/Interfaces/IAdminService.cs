using CRM_KSK.Application.Models;

namespace CRM_KSK.Application.Interfaces;

public interface IAdminService
{
    Task<string> Login(string email, string password, CancellationToken cancellationToken);
    Task Register(RegisterRequest register, CancellationToken cancellationToken);
}
