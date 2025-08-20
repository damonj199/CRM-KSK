using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class AuthRepositiry : IAuthRepository
{
    private readonly CRM_KSKDbContext _context;

    public AuthRepositiry(CRM_KSKDbContext context)
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

    public async Task<Admin> GetAdminByIdAsync(Guid id, CancellationToken token)
    {
        var admin = await _context.Admins.FindAsync(id, token);
        return admin;
    }

    public async Task UpdatePasswordAsync(Admin admin, CancellationToken token)
    {
        _context.Admins
            .Where(u => u.Id == admin.Id)
            .ExecuteUpdate(u => u
                .SetProperty(p => p.PasswordHash, admin.PasswordHash));

        await _context.SaveChangesAsync(token);
    }
}
