using AppTransaction.Domain;
using AppTransaction.Infraestruture.Contexts;
using AppTransaction.Infraestruture.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTransaction.InfraestructureTest
{
    public class ProductDataBaseFixture : IDisposable
    {
        public TransactionContext TransactionDBContext { get; private set; }

        public ProductDataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase("ProductListDatabase")
                .Options;

            TransactionDBContext = new TransactionContext(options);

            TransactionDBContext.Products.Add(new Product { ProductId = Guid.NewGuid(), ProductCode = 11, ProductName = "pencil", ProductValue = 1 });     
            TransactionDBContext.SaveChanges();
        }

        public void Dispose()
        {
            TransactionDBContext.Dispose();
        }
    }

    public class ProductRepositoryTest : IClassFixture<ProductDataBaseFixture>
    {
        private readonly ProductDataBaseFixture _fixture;

        public ProductRepositoryTest(ProductDataBaseFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetAsync_ClientList_ReturnClientList()
        {
            var productRepository = new ProductRepository(_fixture.TransactionDBContext);

            //Act
            var products = await productRepository.GetAsync();

            //Assert

            Assert.Equal(2, products.Count());
        }

        //[Fact]
        //public async Task GetAsync_EmptyProductList_ReturnEmptyProductList()
        //{
        //    var options = new DbContextOptionsBuilder<TransactionContext>()
        //        .UseInMemoryDatabase(databaseName: "ProductDatabase2")
        //        .Options;

        //    using var context = new TransactionContext(options);
        //    context.Products.Add(new Product { });
        //    await context.SaveChangesAsync();

        //    var productRepository = new ProductRepository(context);

        //    //Act
        //    var products = await productRepository.GetAsync();

        //    //Assert

        //    Assert.Empty(products);
        //}

        [Fact]
        public async Task GetById_SingleProductId_ReturnSameProduct()
        {
            var productId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "ProductDatabase3")
                .Options;

            using var context = new TransactionContext(options);
            context.Products.Add(new Product { ProductId = productId, ProductCode = 10, ProductName = "Chair", ProductValue = 15 });
            await context.SaveChangesAsync();

            var productRepository = new ProductRepository(context);

            //Act

            var product = await productRepository.GetByIdAsync(productId);

            //Assert

            Assert.Equal(productId, product.ProductId);
            Assert.Equal(10, product.ProductCode);
            Assert.Equal("Chair", product.ProductName);
            Assert.Equal(15, product.ProductValue);
        }

        [Fact]
        public async Task GetById_NonExistingProductId_ReturnNullt()
        {
            var nonExistingProductId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "ProductDatabase4")
                .Options;

            using var context = new TransactionContext(options);

            var productRepository = new ProductRepository(context);

            //Act

            var product = await productRepository.GetByIdAsync(nonExistingProductId);

            //Assert

            Assert.Null(product);
        }

        [Fact]
        public async Task SaveAsync_AddNewProduct_AddsNewProductToDatabase()
        {
            var productRepository = new ProductRepository(_fixture.TransactionDBContext);

            var newProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductCode = 10,
                ProductName = "Chair",
                ProductValue = 15
            };

            // Act

            await productRepository.SaveAsync(newProduct);
            var savedProduct = await productRepository.GetByIdAsync(newProduct.ProductId);

            // Assert

            Assert.NotNull(savedProduct);
            Assert.Equal(newProduct.ProductId, savedProduct.ProductId);
            Assert.Equal(newProduct.ProductCode, savedProduct.ProductCode);
            Assert.Equal(newProduct.ProductName, savedProduct.ProductName);
            Assert.Equal(newProduct.ProductValue, savedProduct.ProductValue);
        }
    }
}
