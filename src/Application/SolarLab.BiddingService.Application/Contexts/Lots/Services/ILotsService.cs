using SolarLab.BiddingService.Contracts.Contexts.Lots.Models;
using SolarLab.BiddingService.Contracts.Contexts.Lots.Requests;

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
    Task<LotDtoWithBids?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить лоты пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Найденные лоты.</returns>
    Task<IReadOnlyCollection<LotDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Разместить лот.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="request">Запрос на размещение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор размещенного лота.</returns>
    Task<Guid> CreateAsync(Guid userId, CreateLotRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Разместить ставку на лот.
    /// </summary>
    /// <param name="lotId">Идентификатор лота.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="request">Запрос на размещение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task PlaceBidAsync(Guid lotId, Guid userId, PlaceBidRequest request, CancellationToken cancellationToken);
}