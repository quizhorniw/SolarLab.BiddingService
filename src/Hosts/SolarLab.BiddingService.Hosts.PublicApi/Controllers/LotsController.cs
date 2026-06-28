using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SolarLab.BiddingService.Application.Contexts.Lots.Services;

namespace SolarLab.BiddingService.Hosts.PublicApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public partial class LotsController(ILotsService lotsService, ILogger<LotsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить лот по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор лота.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var lot = await lotsService.GetByIdAsync(id, cancellationToken);
        if (lot is null)
        {
            return NotFound();
        }

        LogFoundLotById(logger, id, JsonSerializer.Serialize(lot));
        return Ok(lot);
    }

    [LoggerMessage(LogLevel.Debug, "Получен лот с ID {Id}: {Lot}")]
    static partial void LogFoundLotById(ILogger<LotsController> logger, Guid id, string lot);
}