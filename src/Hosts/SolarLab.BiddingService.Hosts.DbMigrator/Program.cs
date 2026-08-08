using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Hosts.DbMigrator;

const string connectionStringField = "BiddingServiceDb";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DbContext, MigrationsAppDbContext>(builder => builder
            .UseNpgsql(context.Configuration.GetConnectionString(connectionStringField))
            .UseSnakeCaseNamingConvention());
        
        services.AddHostedService<MigrationsWorker>();
    })
    .Build();

await host.RunAsync();