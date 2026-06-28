using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Application.Contexts.Lots.Services;
using SolarLab.BiddingService.Infrastructure.DataAccess.DbContexts;

namespace SolarLab.BiddingService.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистратор сервисов приложения.
/// </summary>
public static class ComponentRegistrar
{
    private const string ConnectionStringField = "BiddingServiceDb";
    
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Добавить сервисы приложения в IoC контейнер.
        /// </summary>
        /// <returns></returns>
        public IServiceCollection RegisterApplicationServices(IConfiguration configuration) => services
            .AddDatabase(configuration)
            .AddServices();

        /// <summary>
        /// Добавить сервисы БД.
        /// </summary>
        /// <returns></returns>
        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(builder => builder
                .UseNpgsql(configuration.GetConnectionString(ConnectionStringField))
                .EnableDetailedErrors());

            services.AddScoped<IAppDbContext, AppDbContext>();
            
            return services;
        }

        /// <summary>
        /// Добавить контекстные сервисы.
        /// </summary>
        /// <returns></returns>
        private IServiceCollection AddServices()
        {
            services.AddScoped<ILotsService, LotsService>();
            
            return services;
        }
    }
}