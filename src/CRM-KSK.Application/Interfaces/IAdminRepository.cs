using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IAdminRepository
{
    Task<Admin> AddAdmin(Guid id, string firstName, string lastName, string phone, string passwordHash, CancellationToken cancellationToken);
    Task<Admin> GetByPhone(string phone, CancellationToken cancellationToken);
}
