namespace SolarLab.BiddingService.Domain.Contexts.Lots.Exeptions;

/// <summary>
/// Недостаточный размер ставки.
/// </summary>
public class InsufficientBidPriceException : Exception
{
    /// <inheritdoc />
    public InsufficientBidPriceException()
    {
    }

    /// <inheritdoc />
    public InsufficientBidPriceException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public InsufficientBidPriceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
