
using AppTransaction.Domain;
using AppTransaction.Infraestruture.Contexts;
using AppTransaction.Infraestruture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppTransaction.InfraestructureTest
{
    public class ClientDataBaseFixture : IDisposable
    {
        public TransactionContext TransactionContext { get; private set; }

        public ClientDataBaseFixture()
        {
            var clientId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
            .UseInMemoryDatabase(databaseName: "ClientDatabase")
            .Options;

            TransactionContext = new TransactionContext(options);

            TransactionContext.Clients.Add(new Client { ClientId = clientId, Identification = "m5", Name = "Juan", AvailableBalance = 200 });
            TransactionContext.SaveChanges();
        }

        public void Dispose() => TransactionContext.Dispose();
    }

    public class ClientRepositoryTest : IClassFixture<ClientDataBaseFixture>
    {
        private readonly ClientDataBaseFixture _fixture;

        public ClientRepositoryTest(ClientDataBaseFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetAsync_ClientList_ReturnClientList()
        {
            var clientRepository = new ClientRepository(_fixture.TransactionContext);

            //Act
            var clients = await clientRepository.GetAsync();

            //Assert

            Assert.Single(clients);
        }

        [Fact]
        public async Task GetAsync_EmptyClientList_ReturnEmptyClientList()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "ClientDatabase2")
                .Options;

            using var context = new TransactionContext(options);

            var clientRepository = new ClientRepository(context);

            //Act
            var clients = await clientRepository.GetAsync();

            //Assert

            Assert.Empty(clients);
        }

        [Fact]
        public async Task GetById_SingleClientId_ReturnSameClient()
        {
            var clientId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "ClientDatabase3")
                .Options;

            using var context = new TransactionContext(options);
            context.Clients.Add(new Client { ClientId = clientId, Identification = "m1", Name = "Pedro", AvailableBalance = 500 });
            await context.SaveChangesAsync();

            var clientRepository = new ClientRepository(context);

            //Act

            var client = await clientRepository.GetByIdAsync(clientId);

            //Assert

            Assert.Equal(clientId, client.ClientId);
            Assert.Equal("m1", client.Identification);
            Assert.Equal("Pedro", client.Name);
            Assert.Equal(500, client.AvailableBalance);
        }

        [Fact]
        public async Task GetById_NonExistingClientId_ReturnNull()
        {
            var nonExistingClientId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "ClientDatabase4")
                .Options;

            using var context = new TransactionContext(options);
            var clientRepository = new ClientRepository(context);

            //Act

            var client = await clientRepository.GetByIdAsync(nonExistingClientId);

            //Assert

            Assert.Null(client);
        }

        [Fact]
        public async Task SaveAsync_AddNewClient_AddsNewClientToDatabase()
        {
            var clientRepository = new ClientRepository(_fixture.TransactionContext);

            var newClient = new Client
            {
                ClientId = Guid.NewGuid(),
                Identification = "m1",
                Name = "Juan",
                AvailableBalance = 1000
            };

            // Act

            await clientRepository.SaveAsync(newClient);
            var savedClient = await clientRepository.GetByIdAsync((Guid)newClient.ClientId);

            // Assert

            Assert.NotNull(savedClient);
            Assert.Equal(newClient.ClientId, savedClient.ClientId);
            Assert.Equal(newClient.Identification, savedClient.Identification);
            Assert.Equal(newClient.Name, savedClient.Name);
            Assert.Equal(newClient.AvailableBalance, savedClient.AvailableBalance);
        }
    }
}
