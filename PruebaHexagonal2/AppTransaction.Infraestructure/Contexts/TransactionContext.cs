using Microsoft.EntityFrameworkCore;
using AppTransaction.Domain;

namespace AppTransaction.Infraestruture.Datos.Contexts
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        
    }
}

