using AppTransaction.Domain;
using AppTransaction.Infraestruture.Contexts;
using AppTransaction.Infraestruture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppTransaction.InfraestructureTest
{
    public class OrderDataBaseFixture : IDisposable
    {
        public TransactionContext TransactionbdContext { get; private set; }

        public OrderDataBaseFixture()
        {
            var orderId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>().UseInMemoryDatabase("OrderDatabase").Options;

            TransactionbdContext = new TransactionContext(options);

            TransactionbdContext.Orders.Add(new Order { OrderId = orderId, Units = 10, ProductValue = 1000, Total = 10000 });
            TransactionbdContext.SaveChanges();
        }

        public void Dispose() => TransactionbdContext.Dispose();
    }

    public class OrderRepositoryTest : IClassFixture<OrderDataBaseFixture>
    {
        private readonly OrderDataBaseFixture _fixture;

        public OrderRepositoryTest(OrderDataBaseFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetAsync_OrderList_ReturnOrderList()
        {
            var orderRepository = new OrderRepository(_fixture.TransactionbdContext);

            //Act
            var orders = await orderRepository.GetAsync();

            //Assert

            Assert.Equal(2, orders.Count());
        }

        [Fact]
        public async Task GetAsync_EmptyOrderList_ReturnEmptyOrderList()
        {
           var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "OrderDatabase2")
                .Options;

            using var context = new TransactionContext(options);

            var orderRepository = new OrderRepository(context);

            //Act
            var orders = await orderRepository.GetAsync();

            //Assert

            Assert.Empty(orders);
        }

        [Fact]
        public async Task GetById_SingleOrderId_ReturnSameOrder()
        {
            var orderId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "OrderDatabase3")
                .Options;

            using var context = new TransactionContext(options);
            context.Orders.Add(new Order { OrderId = orderId, Units = 10, ProductValue = 1000, Total = 10000 });
            await context.SaveChangesAsync();

            var orderRepository = new OrderRepository(context);

            //Act

            var order = await orderRepository.GetByIdAsync(orderId);

            //Assert

            Assert.Equal(orderId, order.OrderId);
            Assert.Equal(10, order.Units);
            Assert.Equal(1000, order.ProductValue);
            Assert.Equal(10000, order.Total);
        }

        [Fact]
        public async Task GetById_NonExistingOrderId_ReturnNull()
        {
            var nonExistingOrderId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "OrderDatabase4")
                .Options;

            using var context = new TransactionContext(options);

            var orderRepository = new OrderRepository(context);

            //Act

            var product = await orderRepository.GetByIdAsync(nonExistingOrderId);

            //Assert

            Assert.Null(product);
        }

        [Fact]
        public async Task SaveAsync_AddNewOrder_AddsNewOrderToDatabase()
        {
            var orderRepository = new OrderRepository(_fixture.TransactionbdContext);

            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                Units = 10,
                Total = 10000,
                ProductValue = 1000
            };

            // Act

            await orderRepository.SaveAsync(newOrder);
            var savedOrder = await orderRepository.GetByIdAsync(newOrder.OrderId);

            // Assert

            Assert.NotNull(savedOrder);
            Assert.Equal(newOrder.OrderId, savedOrder.OrderId);
            Assert.Equal(newOrder.Units, savedOrder.Units);
            Assert.Equal(newOrder.Total, savedOrder.Total);
            Assert.Equal(newOrder.ProductValue, savedOrder.ProductValue);
        }
    }
}
