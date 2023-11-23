namespace AppTransaction.Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync();
        Task<Order> GetByIdAsync(Guid orderId);
        Task SaveAsync(Order order);
    }
}
