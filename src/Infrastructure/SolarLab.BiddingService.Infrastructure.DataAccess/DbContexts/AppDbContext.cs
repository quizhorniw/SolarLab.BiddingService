using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Domain.Contexts.Lots;

namespace SolarLab.BiddingService.Infrastructure.DataAccess.DbContexts;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Lot> Lots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}