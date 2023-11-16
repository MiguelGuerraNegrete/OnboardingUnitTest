using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;
using AppTransaction.Infraestruture.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AppTransaction.Infraestruture.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TransactionContext _context;

        public ClientRepository(TransactionContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IEnumerable<Client>> GetAsync() => await _context.Clients.ToListAsync();

        public async Task<Client> GetByIdAsync(Guid id) => await _context.Clients.FindAsync(id);

        public async Task SaveAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
    }
}
