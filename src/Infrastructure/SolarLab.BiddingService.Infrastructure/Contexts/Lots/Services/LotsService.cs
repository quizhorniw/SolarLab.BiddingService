using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SolarLab.BiddingService.Application.Abstractions.Data;
using SolarLab.BiddingService.Application.Contexts.Lots.Options;
using SolarLab.BiddingService.Application.Contexts.Lots.Services;
using SolarLab.BiddingService.Contracts.Contexts.Lots.Models;
using SolarLab.BiddingService.Contracts.Contexts.Lots.Requests;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;
using SolarLab.BiddingService.Domain.Contexts.Lots.Exeptions;
using SolarLab.BiddingService.Infrastructure.Contexts.Lots.Mappers;

namespace SolarLab.BiddingService.Infrastructure.Contexts.Lots.Services;

/// <inheritdoc />
public class LotsService(IAppDbContext dbContext, TimeProvider timeProvider,
    IOptions<BiddingOptions> biddingOptions, ILogger<LotsService> logger) : ILotsService
{
    private readonly BiddingOptions _biddingOptions = biddingOptions.Value;

    /// <inheritdoc />
    public Task<LotDtoWithBids?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.Lots
            .Include(x => x.Bids)
            .Where(x => x.Id == id)
            .ProjectToDtoWithBids()
            .FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<LotDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) =>
        await dbContext.Lots
            .Where(x => x.UserId == userId)
            .ProjectToDto()
            .ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Guid userId, CreateLotRequest request, CancellationToken cancellationToken)
    {
        var lot = new Lot
        {
            UserId = userId,
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            StartingPrice = request.StartingPrice,
            PlacementDateTime = timeProvider.GetUtcNow().UtcDateTime
        };

        await dbContext.Lots.AddAsync(lot, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return lot.Id;
    }

    /// <inheritdoc />
    public async Task PlaceBidAsync(Guid lotId, Guid userId, PlaceBidRequest request,
        CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable,
            cancellationToken);

        try
        {
            var lot = await dbContext.Lots
                .Include(x => x.Bids)
                .Where(x => x.Id == lotId)
                .ProjectToDtoWithBids()
                .FirstOrDefaultAsync(cancellationToken)
                 ?? throw new KeyNotFoundException("Лот не найден");

            var actualBidPrice = lot.Bids?
                .OrderByDescending(x => x.BidDateTime)
                .Select(x => x.Price)
                .Take(1)
                .FirstOrDefault()
                 ?? lot.StartingPrice;

            var minBidPrice = actualBidPrice + _biddingOptions.MinStep;
            if (request.Price < minBidPrice)
            {
                throw new InsufficientBidPriceException($"Минимальный размер ставки: {minBidPrice}");
            }

            var bid = new Bid
            {
                LotId = lotId,
                UserId = userId,
                Price = request.Price,
                BidDateTime = timeProvider.GetUtcNow().UtcDateTime
            };

            await dbContext.Bids.AddAsync(bid, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Произошла ошибка во время размещения ставки: {Ex}", e.Message);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}