using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;

namespace AppTransaction.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository repositoryProduct)
        {
            _ProductRepository = repositoryProduct;
        }

        public async Task<IEnumerable<Product>> GetAsync() => await _ProductRepository.GetAsync();

        public async Task<Product> GetByIdAsync(Guid productId) => await _ProductRepository.GetByAsync(productId);

        public async Task SaveAsync(Product product)
        {
             await _ProductRepository.SaveAsync(product);   
        }
    }
}
