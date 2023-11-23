using AppTransaction.Aplication.Interfaces;
using AppTransaction.Aplication.Services;
using AppTransaction.Domain;
using AppTransaction.Infraestructure.API.Controllers;
using AppTransaction.Infraestruture.Contexts;
using AppTransaction.Infraestruture.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AppTransaction.Infrastructure.API.Test
{
    public class ClientControllerTest
    {
        private readonly Mock<IClientService> _clientService;

        public ClientControllerTest()
        {
            _clientService = new Mock<IClientService>();
        }

        [Fact]
        public async Task GetAsync_ClientList_ReturnClientList()
        {
            var clientList = GetClientsData();
            _clientService.Setup(x => x.GetAsync()).ReturnsAsync(clientList);

            //act
            var clientController = new ClientController(_clientService.Object);
            var result = await clientController.GetAsync();

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetAsync_EmptyClientList_ReturnEmptyClientList()
        {
            var clientList = GetNotFoundClientsData();

            _clientService.Setup(x => x.GetAsync()).ReturnsAsync(clientList);


            var clientController = new ClientController(_clientService.Object);

            //Act
            var actionResult = await clientController.GetAsync();

            //Assert

            Assert.IsAssignableFrom<ObjectResult>(actionResult);

            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(200, objectResult.StatusCode);

            var clients = Assert.IsAssignableFrom<IEnumerable<Client>>(objectResult.Value);
            Assert.Empty(clients);
        }

        [Fact]
        public async Task GetById_SingleClientId_ReturnSameClient()
        {
            var clientId = Guid.Parse("207bf2e9-7d5d-4426-a93b-66925bab4b01");
            var clientList = GetClientsData();
            _clientService.Setup(x => x.GetByIdAsync(clientId)).ReturnsAsync(clientList[0]);
            var clientController = new ClientController(_clientService.Object);

            //act
            var clientResult = await clientController.GetByID(clientId);

            //assert
            Assert.IsType<OkObjectResult>(clientResult);
        }

        [Fact]
        public async Task SaveAsync_AddNewClient_AddNewClientToList()
        {
            var clientController = new ClientController(_clientService.Object);

            var newClient = new Client
            {
                ClientId = Guid.NewGuid(),
                Identification = "m1",
                Name = "Juan",
                AvailableBalance = 1000
            };

            // Act

            await clientController.Post(newClient);
            var savedClient = await clientController.GetByID((Guid)newClient.ClientId);

            // Assert

            Assert.IsType<OkObjectResult>(savedClient);
        }

        public static List<Client> GetClientsData()
        {
            var clientId = Guid.Parse("207bf2e9-7d5d-4426-a93b-66925bab4b01");
            List<Client> clients = new() { new Client { ClientId = clientId, Identification = "m1", Name = "Pedro", AvailableBalance = 55000 } };
            return clients;
        }

        public  List<Client> GetNotFoundClientsData()
        {
            List<Client>? clients2 = new();
            return clients2;
        }
        
    }
}
