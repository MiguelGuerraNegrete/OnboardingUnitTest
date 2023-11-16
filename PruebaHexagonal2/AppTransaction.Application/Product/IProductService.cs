using AppTransaction.Domain;

namespace AppTransaction.Aplication.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetByIdAsync(Guid productId);
        Task SaveAsync(Product product);
    }
}
