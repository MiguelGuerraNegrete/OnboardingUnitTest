using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace AppTransaction.Infraestruture.Datos.Contexts.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly TransactionContext _context;

        public ProductRepository(TransactionContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IEnumerable<Product>> GetAsync() => await _context.Products.ToListAsync();

        public async Task<Product> GetByAsync(Guid productId) => await _context.Products.FindAsync(productId);

        public async Task SaveAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
