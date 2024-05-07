using Berlin.Domain.Entities;
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
            services.AddScoped<ITestService, TestService>();
            return services;
        }
    }
}
