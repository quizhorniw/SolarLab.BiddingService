using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

namespace SolarLab.BiddingService.Application.Abstractions.Data;

/// <summary>
/// Контекст базы данных приложения.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Лоты.
    /// </summary>
    DbSet<Lot> Lots { get; }

    /// <summary>
    /// Ставки на лот.
    /// </summary>
    DbSet<Bid> Bids { get; }
    
    /// <summary>
    /// Фасад доступа к БД.
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// Сохранить изменения в БД.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Число измененных строк.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}