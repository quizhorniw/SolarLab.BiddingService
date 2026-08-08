namespace SolarLab.BiddingService.Contracts.Contexts.Lots.Models;

/// <summary>
/// DTO лота.
/// </summary>
public class LotDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, разместившего лот.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Стартовая цена.
    /// </summary>
    public decimal StartingPrice { get; set; }

    /// <summary>
    /// Дата и время размещения лота.
    /// </summary>
    public DateTime PlacementDateTime { get; set; }
}