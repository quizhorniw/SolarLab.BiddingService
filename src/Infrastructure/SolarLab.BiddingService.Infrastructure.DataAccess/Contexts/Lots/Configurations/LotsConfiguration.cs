using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.BiddingService.Domain.Contexts.Lots;

namespace SolarLab.BiddingService.Infrastructure.DataAccess.Contexts.Lots.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Lot" />
/// </summary>
public class LotsConfiguration : IEntityTypeConfiguration<Lot>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
    }
}