namespace SolarLab.BiddingService.Domain.Contexts.Lots;

/// <summary>
/// Сущность лота аукциона.
/// </summary>
public class Lot
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, разместившего лот.
    /// </summary>
    public Guid UserId { get; set; }
}