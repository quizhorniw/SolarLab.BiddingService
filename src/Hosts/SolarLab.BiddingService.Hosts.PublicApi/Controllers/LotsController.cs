using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarLab.BiddingService.Application.Contexts.Lots.Services;
using SolarLab.BiddingService.Contracts.Contexts.Lots.Requests;

namespace SolarLab.BiddingService.Hosts.PublicApi.Controllers;

/// <summary>
/// Контроллер управления лотами.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class LotsController(ILotsService lotsService) : ControllerBase
{
    /// <summary>
    /// Получить лот по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор лота.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var lot = await lotsService.GetByIdAsync(id, cancellationToken);
        if (lot is null)
        {
            return NotFound();
        }

        return Ok(lot);
    }

    /// <summary>
    /// Получить лоты пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpGet("users/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var lots = await lotsService.GetByUserIdAsync(userId, cancellationToken);
        return Ok(lots);
    }

    /// <summary>
    /// Разместить лот.
    /// </summary>
    /// <param name="request">Запрос на размещение.</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateAsync(CreateLotRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || string.IsNullOrWhiteSpace(userIdClaim.Value))
        {
            return Unauthorized("Отсутствует авторизация.");
        }

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest("Неверный формат идентификатора.");
        }
        
        var lotId = await lotsService.CreateAsync(userId, request, cancellationToken);
        return CreatedAtAction("GetById", new { id = lotId }, new { id = lotId });
    }

    /// <summary>
    /// Разместить ставку на лот.
    /// </summary>
    /// <param name="lotId">Идентификатор лота.</param>
    /// <param name="request">Запрос на размещение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpPost("{lotId:guid}/bids")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PlaceBidAsync(Guid lotId, PlaceBidRequest request,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || string.IsNullOrWhiteSpace(userIdClaim.Value))
        {
            return Unauthorized("Отсутствует авторизация");
        }

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest("Неверный формат идентификатора");
        }

        await lotsService.PlaceBidAsync(lotId, userId, request, cancellationToken);
        return Ok();
    }
}