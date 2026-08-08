using Microsoft.AspNetCore.Diagnostics;
using SolarLab.BiddingService.Domain.Contexts.Lots.Exeptions;

namespace SolarLab.BiddingService.Hosts.PublicApi.ExceptionHandlers;

/// <summary>
/// Глобальный обработчик ошибок приложения.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message) = exception switch
        {
            ArgumentException => (StatusCodes.Status400BadRequest, "Передан неверный аргумент"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Сущность не найдена"),
            InsufficientBidPriceException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Произошла неизвестная ошибка")
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                StatusCode = statusCode,
                Error = message
            },
            cancellationToken);

        return true;
    }
}