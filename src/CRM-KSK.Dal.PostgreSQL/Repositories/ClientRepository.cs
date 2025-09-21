using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly CRM_KSKDbContext _context;
    private readonly ILogger<ClientRepository> _logger;

    public ClientRepository(CRM_KSKDbContext context, ILogger<ClientRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken); 

        _logger.LogWarning($"Клиент {client.FirstName} {client.LastName} добавлен!");
        return client.Id;
    }

    public async Task<List<Client>> GetAllClientWithMemberships(CancellationToken token)
    {
        var clients = await _context.Clients
            .Include(m => m.Memberships)
            .ToListAsync(token);

        return clients ?? [];
    }

    public async Task<List<Client>> GetStatistics(CancellationToken token)
    {
        var twoMonthsAgo = DateOnly.FromDateTime(DateTime.Today.AddMonths(-2));
        
        var clients = await _context.Clients
            .Include(m => m.Memberships.Where(mem => 
                !mem.IsDeleted || // Все активные абонементы
                mem.DateEnd >= twoMonthsAgo)) // Или абонементы за последние 2 месяца (включая удаленные)
            .ToListAsync(token);

        return clients ?? [];
    }

    public async Task<Client> GetClientById(Guid id, CancellationToken token)
    {
        var client = await _context.Clients.Include(m => m.Memberships).FirstOrDefaultAsync(c => c.Id == id, token);
        
        return client;
    }

    public async Task<List<Client>> GetAllClientsAsync(CancellationToken token)
    {
        var clients = await _context.Clients.ToListAsync(token);

        return clients ?? [];
    }

    public async Task<List<Client>> GetClientsForScheduleAsync(CancellationToken token)
    {
        var clients = await _context.Clients
            .Include(c => c.Memberships)
            .Where(c => c.Memberships.Any(m => 
                (m.StatusMembership == StatusMembership.Active || 
                 m.StatusMembership == StatusMembership.OneTime || 
                 m.IsOneTimeTraining == true) && 
                m.AmountTraining > 0))
            .ToListAsync(token);

        return clients ?? [];
    }

    public async Task<List<BirthdayNotification>> GetClientWithBirthDaysThisMonthAsync(int month, CancellationToken token)
    {
        var clientsBod = await _context.Clients
            .Where(c => c.DateOfBirth.Month == month)
            .Select(c => new BirthdayNotification
            {
                PersonId = c.Id,
                PersonType = "Клиент",
                Name = $"{c.FirstName} {c.LastName}",
                Phone = c.Phone,
                Birthday = c.DateOfBirth
            }).ToListAsync(token);

        return clientsBod ?? [];
    }


    public async Task SoftDeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            client.SoftDelete();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ClientVerificationAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var clientExist = await _context.Clients.AnyAsync(c => c.Phone == phoneNumber, cancellationToken);
        _logger.LogWarning("Провряем что у нас там в БД, {0}", clientExist);
        return clientExist;
    }

    public async Task UpdateClientInfoAsync(Client client, CancellationToken token)
    {
        var existingClient = await _context.Clients.FindAsync(client.Id);
        if(existingClient != null)
        {
            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Phone = client.Phone;
            existingClient.DateOfBirth = client.DateOfBirth;
            existingClient.ParentName = client.ParentName;
            existingClient.ParentPhone = client.ParentPhone;
        }
        await _context.SaveChangesAsync(token);
    }
}
