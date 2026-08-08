namespace SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

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
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Максимальная длина поля <see cref="Name"/>
    /// </summary>
    public const int NameMaxLength = 100;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Максимальная длина поля <see cref="Description"/>
    /// </summary>
    public const int DescriptionMaxLength = 2000;

    /// <summary>
    /// Стартовая цена.
    /// </summary>
    public decimal StartingPrice { get; set; }
    
    /// <summary>
    /// Дата и время размещения лота.
    /// </summary>
    public DateTime PlacementDateTime { get; set; }
    
    /// <summary>
    /// Время торгов лота в минутах.
    /// </summary>
    public const int TradingTimeMinutes = 300;

    /// <summary>
    /// Ставки.
    /// </summary>
    public virtual List<Bid>? Bids { get; set; }
}