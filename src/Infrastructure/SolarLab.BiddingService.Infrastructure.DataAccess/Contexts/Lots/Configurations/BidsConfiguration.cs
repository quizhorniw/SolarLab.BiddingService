using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

namespace SolarLab.BiddingService.Infrastructure.DataAccess.Contexts.Lots.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Bid"/>
/// </summary>
public class BidsConfiguration : IEntityTypeConfiguration<Bid>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasOne(x => x.Lot)
            .WithMany(x => x.Bids)
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}