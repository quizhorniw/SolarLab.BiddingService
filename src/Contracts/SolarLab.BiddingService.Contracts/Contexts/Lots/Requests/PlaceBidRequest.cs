namespace SolarLab.BiddingService.Contracts.Contexts.Lots.Requests;

/// <summary>
/// Запрос на размещение ставки на лот.
/// </summary>
public class PlaceBidRequest
{
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
}