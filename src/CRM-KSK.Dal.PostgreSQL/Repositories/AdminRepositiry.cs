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

    public async Task<Admin> AddAdmin(Guid id, string firstName, string lastName, string email, string passwordHash, CancellationToken cancellationToken)
    {
        var adminEntity = new Admin()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash
        };
        await _context.Admins.AddAsync(adminEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return adminEntity;
    }

    public async Task<Admin> GetByEmail(string email)
    {
        var admin = await _context.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        return admin;
    }
}
