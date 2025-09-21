using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly CRM_KSKDbContext _context;
    private readonly ILogger<MembershipRepository> _logger;

    public MembershipRepository(CRM_KSKDbContext context, ILogger<MembershipRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Membership>> AddMembershipAsync(List<Membership> memberships, CancellationToken token)
    {
        _context.Memberships.AddRange(memberships);
        await _context.SaveChangesAsync(token);
        
        var membershipIds = memberships.Select(m => m.Id).ToList();
        var addedMemberships = await _context.Memberships
            .Include(m => m.Client)
            .Where(m => membershipIds.Contains(m.Id))
            .ToListAsync(token);
        
        return addedMemberships;
    }

    public async Task<List<Membership>> GetAllMembershipClientAsync(Guid id, CancellationToken token)
    {
        var memberships = await _context.Memberships
            .Where(c => c.ClientId == id)
            .ToListAsync(token);

        return memberships ?? [];
    }

    public async Task<List<Membership>> GetMembershipsByClientAndTypeAsync(Guid id, TypeTrainings type, CancellationToken token)
    {
        var memberships = await _context.Memberships
            .Where(c => c.ClientId == id && c.TypeTrainings == type)
            .ToListAsync(token);
            
        return memberships ?? [];
    }


    public async Task UpdateMembershipAsync(Membership membership, CancellationToken token)
    {
        var exMembership = await _context.Memberships.FindAsync(membership.Id, token);
        if (exMembership != null)
        {
            exMembership.DateStart = membership.DateStart;
            exMembership.DateEnd = membership.DateEnd;
            exMembership.AmountTraining = membership.AmountTraining;
            exMembership.TypeTrainings = membership.TypeTrainings;
            exMembership.StatusMembership = membership.StatusMembership;
            exMembership.IsMorning = membership.IsMorning;
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task SoftDeleteMembershipAsync(Guid id, CancellationToken token)
    {
        var mem = await _context.Memberships
            .Include(m => m.Client)
            .FirstOrDefaultAsync(m => m.Id == id, token);
            
        if (mem != null)
        {
            _logger.LogWarning($"Абонемент {mem.TypeTrainings.ToString()}" +
                $"для клиента {mem.Client?.LastName ?? "Неизвестно"} {mem.Client?.FirstName ?? "Неизвестно"} был удален. Остаток занятий был {mem.AmountTraining}");

            mem.SoftDelete();
            await _context.SaveChangesAsync(token);
        }
    }
}
