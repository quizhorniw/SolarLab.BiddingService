using Microsoft.EntityFrameworkCore;

namespace SolarLab.BiddingService.Hosts.DbMigrator;

/// <summary>
/// Сервис, применяющий миграции к БД приложения.
/// </summary>
public class MigrationsWorker(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime,
    ILogger<MigrationsWorker> logger) : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Применение миграций ...");
        
        await using var scope = serviceProvider.CreateAsyncScope();
        var sp = scope.ServiceProvider;

        foreach (var dbContext in sp.GetServices<DbContext>())
        {
            var dbContextName = dbContext.GetType().FullName ?? dbContext.GetType().Name;
            logger.LogInformation("Применение миграций к контексту '{DbContextName}' ...", dbContextName);

            try
            {
                await dbContext.Database.MigrateAsync(stoppingToken);
                logger.LogInformation("Миграции к контексту '{DbContextName}' успешно применены", dbContextName);
            }
            catch (Exception e)
            {
                logger.LogInformation("Ошибка при применении миграций к контексту '{DbContextName}': {Ex}",
                    dbContextName, e.Message);
            }
        }
        
        logger.LogInformation("Применение миграций завершено");
        applicationLifetime.StopApplication();
    }
}