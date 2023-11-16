using AppTransaction.Domain;

namespace AppTransaction.Aplication.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAsync();
        Task<Order> GetByAsync(Guid id);
        Task SaveAsync(Order order);
    }
}
