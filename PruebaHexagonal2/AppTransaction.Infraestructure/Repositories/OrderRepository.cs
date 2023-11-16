using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;
using AppTransaction.Infraestruture.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AppTransaction.Infraestruture.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TransactionContext _context;

        public OrderRepository(TransactionContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IEnumerable<Order>> GetAsync() => await _context.Orders.ToListAsync();

        public async Task<Order> GetByAsync(Guid id) => await _context.Orders.FindAsync(id);

        public async Task SaveAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
