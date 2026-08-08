namespace SolarLab.BiddingService.Contracts.Contexts.Lots.Requests;

/// <summary>
/// Запрос на размещение лота.
/// </summary>
public class CreateLotRequest
{
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
}