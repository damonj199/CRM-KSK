using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IAuthRepository
{
    Task<Admin> AddAdmin(Guid id, string firstName, string lastName, string phone, string passwordHash, CancellationToken cancellationToken);
    Task<Admin> GetAdminByIdAsync(Guid id, CancellationToken token);
    Task<Admin> GetByPhone(string phone, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(Admin admin, CancellationToken token);
}
