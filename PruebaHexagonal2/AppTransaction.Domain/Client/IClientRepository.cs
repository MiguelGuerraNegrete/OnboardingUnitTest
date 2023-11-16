namespace AppTransaction.Domain.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAsync();
        Task<Client> GetByIdAsync(Guid clientId);
        Task SaveAsync(Client client);
    }
}
