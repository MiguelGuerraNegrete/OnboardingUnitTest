using AppTransaction.Aplication.Interfaces;
using AppTransaction.Aplication.Services;
using AppTransaction.Domain;
using AppTransaction.Domain.Interfaces.Repository;
using Moq;
using System.Collections.Generic;

namespace AppTransaction.ApplicationTest
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _clientRepository; 

        public ClientServiceTest()
        {
            _clientRepository = new Mock<IClientRepository>();
        }

        [Fact]
        public async Task GetAsync_ClientList_ReturnClientList()
        {
            var clientList = GetClientsData();

            _clientRepository.Setup(x => x.GetAsync()).ReturnsAsync(clientList);
            //act
            var clientService = new ClientService(_clientRepository.Object);
            var result = await clientService.GetAsync();
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_SingleClientId_ReturnSameClient()
        {
            var clientId = Guid.Parse("207bf2e9-7d5d-4426-a93b-66925bab4b01");
            var clientList = GetClientsData();
            _clientRepository.Setup(x => x.GetByIdAsync(clientId)).ReturnsAsync(clientList[0]);
            var clientService = new ClientService(_clientRepository.Object);
            //act
            var clientResult = await clientService.GetByIdAsync(clientId);
            //assert
            Assert.NotNull(clientResult);
        }

        public static List<Client> GetClientsData()
        {
            var clientId = Guid.Parse("207bf2e9-7d5d-4426-a93b-66925bab4b01");
            List<Client> clients = new() { new Client { ClientId = clientId, Identification = "m1", Name = "Pedro", AvailableBalance = 55000 } };
            return clients;          
        }      
    }
}
