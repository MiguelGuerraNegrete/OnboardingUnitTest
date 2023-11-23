namespace AppTransaction.Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetByIdAsync(Guid productId);
        Task SaveAsync(Product product);
    }
}
