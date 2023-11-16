using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;

namespace AppTransaction.Aplication.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository; 

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAsync() => await _clientRepository.GetAsync();

        public async Task<Client> GetByIdAsync(Guid clientId) => await _clientRepository.GetByIdAsync(clientId);

        public async Task SaveAsync(Client client)
        {
            await _clientRepository.SaveAsync(client);   
        }
    }
}
