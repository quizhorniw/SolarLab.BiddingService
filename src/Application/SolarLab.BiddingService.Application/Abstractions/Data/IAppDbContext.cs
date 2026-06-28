using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Domain.Contexts.Lots;

namespace SolarLab.BiddingService.Application.Abstractions.Data;

public interface IAppDbContext
{
    DbSet<Lot> Lots { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}