using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

namespace SolarLab.BiddingService.Infrastructure.DataAccess.DbContexts;

/// <inheritdoc cref="IAppDbContext" />
public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
{
    /// <inheritdoc />
    public DbSet<Lot> Lots { get; set; }

    /// <inheritdoc />
    public DbSet<Bid> Bids { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}