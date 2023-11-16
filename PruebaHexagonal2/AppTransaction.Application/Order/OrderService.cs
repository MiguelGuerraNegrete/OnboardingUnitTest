using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;

namespace AppTransaction.Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repositoryOrder;

        public OrderService(IOrderRepository repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
        }

        public async Task<IEnumerable<Order>> GetAsync() => await _repositoryOrder.GetAsync();

        public async Task<Order> GetByAsync(Guid id) => await _repositoryOrder.GetByAsync(id);

        public async Task SaveAsync(Order order)
        {
            await _repositoryOrder.SaveAsync(order);
            
        }
    }   
}
