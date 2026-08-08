using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Infrastructure.DataAccess.DbContexts;

namespace SolarLab.BiddingService.Hosts.DbMigrator;

/// <summary>
/// Контекст БД для миграций.
/// </summary>
public class MigrationsAppDbContext(DbContextOptions options) : AppDbContext(options);