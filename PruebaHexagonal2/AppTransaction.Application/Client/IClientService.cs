using AppTransaction.Domain;

namespace AppTransaction.Aplication.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAsync();
        Task<Client> GetByIdAsync(Guid clientId);
        Task SaveAsync(Client client);
    }
}
