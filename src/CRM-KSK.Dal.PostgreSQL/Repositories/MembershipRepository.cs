using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly CRM_KSKDbContext _context;

    public MembershipRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddMembershipAsync(List<Membership> memberships, CancellationToken token)
    {
        _context.Memberships.AddRange(memberships);
        await _context.SaveChangesAsync(token);
    }

    public async Task<List<Membership>> GetAllMembershipClientAsync(Guid id, CancellationToken token)
    {
        var memberships = await _context.Memberships
            .Where(c => c.ClientId == id)
            .ToListAsync(token);

        return memberships ?? [];
    }

    public async Task<Membership> GetMembershipByClientAndTypeAsync(Guid id, TypeTrainings type, CancellationToken token)
    {
        var membership = await _context.Memberships
            .Where(c => c.ClientId == id
                        && c.TypeTrainings == type)
            .FirstOrDefaultAsync(token);

        return membership;
    }

    public async Task<List<Membership>> GetExpiringMembershipAsync(CancellationToken token)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var expiringPeriod = today.AddDays(3);

        var expiringMembership = await _context.Memberships
            .Where(m => m.DateEnd <= expiringPeriod
                || m.AmountTraining <= 2)
            .Include(c => c.Client)
            .ToListAsync(token);

        return expiringMembership ?? [];
    }

    public async Task<Membership> GetMembershipByIdAsync(Guid id, CancellationToken token)
    {
        var membership = await _context.Memberships.FindAsync(id);

        return membership;
    }

    public async Task UpdateMembershipAsync(Membership membership, CancellationToken token)
    {
        var exMembership = await _context.Memberships.FindAsync(membership.Id);
        if (exMembership != null)
        {
            exMembership.DateStart = membership.DateStart;
            exMembership.DateEnd = membership.DateEnd;
            exMembership.AmountTraining = membership.AmountTraining;
            exMembership.TypeTrainings = membership.TypeTrainings;
            exMembership.StatusMembership = membership.StatusMembership;
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteMembershipAsync(Guid id, CancellationToken token)
    {
        var mem = new Membership { Id = id };

        _context.Memberships.Attach(mem);
        _context.Memberships.Remove(mem);
        await _context.SaveChangesAsync(token);
    }
}
