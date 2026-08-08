using Riok.Mapperly.Abstractions;
using SolarLab.BiddingService.Contracts.Contexts.Lots.Models;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

namespace SolarLab.BiddingService.Infrastructure.Contexts.Lots.Mappers;

/// <summary>
/// Маппер для лотов.
/// </summary>
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target,
    RequiredEnumMappingStrategy = RequiredMappingStrategy.Source)]
public static partial class LotsMapper
{
    /// <summary>
    /// Проекция <see cref="Lot"/> в <see cref="LotDto"/>.
    /// </summary>
    /// <param name="entities">Нематериализованный список <see cref="Lot"/>.</param>
    /// <returns>Нематериализованный список <see cref="LotDto"/>.</returns>
    public static partial IQueryable<LotDto> ProjectToDto(this IQueryable<Lot> entities);

    /// <summary>
    /// Проекция <see cref="Lot"/> в <see cref="LotDtoWithBids"/>.
    /// </summary>
    /// <param name="entities">Нематериализованный список <see cref="Lot"/>.</param>
    /// <returns>Нематериализованный список <see cref="LotDtoWithBids"/>.</returns>
    public static partial IQueryable<LotDtoWithBids> ProjectToDtoWithBids(this IQueryable<Lot> entities);
}