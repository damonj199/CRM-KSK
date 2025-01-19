using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IAdminRepository
{
    Task<Admin> AddAdmin(Guid id, string firstName, string lastName, string email, string passwordHash, CancellationToken cancellationToken);
    Task<Admin> GetByEmail(string email);
}
