namespace SolarLab.BiddingService.Application.Contexts.Auth.Options;

/// <summary>
/// Опции для JWT.
/// </summary>
public class JwtBearerOptions
{
    /// <summary>
    /// Секретный ключ.
    /// </summary>
    public string SecretKey { get; set; } = null!;

    /// <summary>
    /// Отправитель.
    /// </summary>
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// Получатель.
    /// </summary>
    public string Audience { get; set; } = null!;
}