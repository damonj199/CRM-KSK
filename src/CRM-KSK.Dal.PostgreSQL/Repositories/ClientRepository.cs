using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly CRM_KSKDbContext _context;

    public ClientRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Client>> SearchClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Clients.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(f => f.FirstName.ToLower().Contains(firstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(query => query.LastName.ToLower().Contains(lastName.ToLower()));

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<Client> GetClientByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(n => n.Phone == phoneNumber);

        return client;
    }

    public async Task<Trainer> GetTrainerByNameAsync(string name)
    {
        return await _context.Trainers.AsNoTracking().FirstOrDefaultAsync(n => n.FirstName.ToLower() == name.ToLower());
    }

    public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}
