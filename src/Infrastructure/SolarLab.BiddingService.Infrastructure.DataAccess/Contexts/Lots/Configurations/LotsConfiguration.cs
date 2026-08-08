using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

namespace SolarLab.BiddingService.Infrastructure.DataAccess.Contexts.Lots.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Lot" />
/// </summary>
public class LotsConfiguration : IEntityTypeConfiguration<Lot>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(Lot.NameMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(Lot.DescriptionMaxLength);
        
        builder.HasIndex(x => x.UserId);
    }
}