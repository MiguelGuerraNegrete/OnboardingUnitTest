﻿namespace AppTransaction.Infrastructure.API.Extensions
{
    using Aplication.Interfaces;
    using Aplication.Services;
    using Domain.Interfaces.Repository;
    using Infraestruture.Datos.Contexts;
    using Infraestruture.Datos.Contexts.Repositorys;
    using Microsoft.EntityFrameworkCore;

    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services,
                IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("cnTransaction");
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddDbContext<TransactionContext>(options => options.UseSqlServer(connectionString));
        }
    }
}