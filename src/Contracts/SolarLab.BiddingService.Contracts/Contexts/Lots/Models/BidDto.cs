namespace SolarLab.BiddingService.Contracts.Contexts.Lots.Models;

/// <summary>
/// DTO ставки на лот.
/// </summary>
public class BidDto
{
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
}