using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Application.Contexts.Lots.Options;
using SolarLab.BiddingService.Application.Contexts.Lots.Services;
using SolarLab.BiddingService.Infrastructure.Contexts.Lots.Services;
using SolarLab.BiddingService.Infrastructure.DataAccess.DbContexts;
using JwtBearerOptions = SolarLab.BiddingService.Application.Contexts.Auth.Options.JwtBearerOptions;

namespace SolarLab.BiddingService.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистратор сервисов приложения.
/// </summary>
public static class ComponentRegistrar
{
    private const string ConnectionStringField = "BiddingServiceDb";
    private const string JwtBearerOptionsField = "JwtBearer";
    private const string BiddingOptionsField = "Bidding";
    
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Добавить сервисы приложения в IoC контейнер.
        /// </summary>
        public IServiceCollection RegisterApplicationServices(IConfiguration configuration) => services
            .AddDatabase(configuration)
            .AddServices(configuration)
            .AddAuthentication();

        /// <summary>
        /// Добавить сервисы БД.
        /// </summary>
        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            services.AddDbContext<IAppDbContext, AppDbContext>(builder => builder
                .UseNpgsql(configuration.GetConnectionString(ConnectionStringField))
                .UseSnakeCaseNamingConvention()
                .EnableDetailedErrors());
            
            return services;
        }

        /// <summary>
        /// Добавить контекстные сервисы.
        /// </summary>
        private IServiceCollection AddServices(IConfiguration configuration)
        {
            services.Configure<JwtBearerOptions>(configuration.GetRequiredSection(JwtBearerOptionsField));
            services.Configure<BiddingOptions>(configuration.GetRequiredSection(BiddingOptionsField));
            
            services.AddScoped<ILotsService, LotsService>();
            
            return services;
        }
        
        /// <summary>
        /// Добавить middleware для аутентификации.
        /// </summary>
        private IServiceCollection AddAuthentication()
        {
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();

            var jwtBearerOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtBearerOptions>>().Value;
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerOptions.SecretKey)),
                        ValidIssuer = jwtBearerOptions.Issuer,
                        ValidAudience = jwtBearerOptions.Audience
                    };
                });

            services.AddAuthorization();
            
            return services;
        }
    }
}