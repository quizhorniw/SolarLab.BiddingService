using SolarLab.BiddingService.Domain.Contexts.Lots;

namespace SolarLab.BiddingService.Application.Contexts.Lots.Services;

/// <summary>
/// Сервис управления лотами.
/// </summary>
public interface ILotsService
{
    /// <summary>
    /// Получить лот по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор лота.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Найденный лот.</returns>
    Task<Lot?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}