using Berlin.Domain.Entities;
using Berlin.Domain.Entities.Interfaces;
using Berlin.Domain.Entities.ProductManagement;
using Berlin.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Berlin.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericService<GenericArticle>,GenericArticleService>();
            services.AddScoped<IGenericService<Site>,GenericService<Site>>();
            services.AddScoped<IGenericService<Division>,GenericService<Division>>();
            services.AddScoped<IGenericService<Service>,GenericService<Service>>();
            services.AddScoped<IGenericService<ServiceType>,GenericService<ServiceType>>();
            services.AddScoped<IGenericService<Device>,GenericService<Device>>();
            services.AddScoped<IGenericService<User>,GenericService<User>>();
            services.AddScoped<IGenericService<SelledService>,GenericService<SelledService>>();
            services.AddScoped<IGenericService<Receipt>,GenericService<Receipt>>();
            services.AddScoped<IGenericService<Bill>,GenericService<Bill>>();
            services.AddScoped<IGenericService<Deviz>,GenericService<Deviz>>();
            services.AddScoped<IGenericService<Invoice>,GenericService<Invoice>>();
            services.AddScoped<IGenericService<BillDetails>,GenericService<BillDetails>>();
            services.AddScoped<IGenericService<Product>,GenericService<Product>>();
            services.AddScoped<IGenericService<ProductHistory>,GenericService<ProductHistory>>();
            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IEntitiesManager, EnitiesManager>();
            return services;
        }
    }
}
