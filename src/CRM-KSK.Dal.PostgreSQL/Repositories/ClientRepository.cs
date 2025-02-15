using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
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

    public async Task AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        _logger.LogInformation("передаем клиента на добавление в БД");
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken); 
        _logger.LogInformation("Клиент добавлен!");
    }

    public async Task<Client> GetClientById(Guid id, CancellationToken token)
    {
        var client = await _context.Clients.FindAsync(id, token);
        return client;
    }

    public async Task<IReadOnlyList<Client>> SearchClientByNameAsync(string firstName, string lastName, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Clients.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(f => f.FirstName.ToLower().Contains(firstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(query => query.LastName.ToLower().Contains(lastName.ToLower()));

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken) ?? [];
    }

    public async Task<Client> GetClientByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        _logger.LogDebug("проверяем есть ли в БД клиент с таким номером");
        var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(n => n.Phone == phoneNumber);

        _logger.LogDebug("вот что в бд по этому клиенту {0}", client);
        return client;
    }

    public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ClientVerificationAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var clientExist = await _context.Clients.AnyAsync(c => c.Phone == phoneNumber, cancellationToken);
        _logger.LogDebug("Провряем что у нас там в БД, {0}", clientExist);
        return clientExist;
    }
}
