using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class AdminRepositiry : IAdminRepository
{
    private readonly CRM_KSKDbContext _context;

    public AdminRepositiry(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task<Admin> AddAdmin(Guid id, string firstName, string lastName, string phone, string passwordHash, CancellationToken cancellationToken)
    {
        var adminEntity = new Admin()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Phone = phone,
            PasswordHash = passwordHash
        };
        await _context.Admins.AddAsync(adminEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return adminEntity;
    }

    public async Task<Admin> GetByPhone(string phone, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Phone == phone);

        return admin;
    }
}
