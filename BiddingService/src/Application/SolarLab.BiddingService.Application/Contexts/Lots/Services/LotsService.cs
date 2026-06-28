using Microsoft.EntityFrameworkCore;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Domain.Contexts.Lots;

namespace SolarLab.BiddingService.Application.Contexts.Lots.Services;

/// <inheritdoc />
public class LotsService(IAppDbContext dbContext) : ILotsService
{
    /// <inheritdoc />
    public Task<Lot?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Lots.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}