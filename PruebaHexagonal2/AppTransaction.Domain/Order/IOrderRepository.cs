namespace AppTransaction.Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync();
        Task<Order> GetByAsync(Guid orderId);
        Task SaveAsync(Order order);
    }
}
