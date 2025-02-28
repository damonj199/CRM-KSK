using CRM_KSK.Application.Configurations;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Models;

namespace CRM_KSK.Application.Interfaces;

public interface IAuthService
{
    Task<RegistrationResult> RegisterAsync(RegisterRequest register, CancellationToken cancellationToken);
    Task<LoginResult> LoginAsync(LoginRequest login, CancellationToken cancellationToken);
    Task<string> UpdatePasswordAsync(UpdatePasswordDto update, CancellationToken token);
}
