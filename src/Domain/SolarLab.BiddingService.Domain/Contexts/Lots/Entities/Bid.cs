namespace SolarLab.BiddingService.Domain.Contexts.Lots.Entities;

/// <summary>
/// Ставка на лот.
/// </summary>
public class Bid
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор лота.
    /// </summary>
    public Guid LotId { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Время и дата ставки.
    /// </summary>
    public DateTime BidDateTime { get; set; }

    /// <summary>
    /// Лот.
    /// </summary>
    public virtual Lot Lot { get; set; } = null!;
}