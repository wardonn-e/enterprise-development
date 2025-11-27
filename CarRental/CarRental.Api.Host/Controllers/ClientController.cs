using CarRental.Application.Contracts.Clients;
using CarRental.Application.Contracts.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Client entities
/// Inherits standard CRUD from the generic base controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService, ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto, Guid>(clientService, logger)
{
    /// <summary>
    /// Retrieves all rental history records associated with a specific client
    /// </summary>
    /// <param name="clientId">The unique identifier of the Client</param>
    /// <returns>A list of RentalDto</returns>
    [HttpGet("{clientId}/Rentals")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<RentalDto>>> GetRentals(Guid clientId)
    {
        try
        {
            var rentalDtos = await clientService.GetRentals(clientId);

            logger.LogInformation("Retrieved Rentals for Client {Id} in {Controller}", clientId, GetType().Name);
            return Ok(rentalDtos);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Client not found for ID {Id} in {Controller}", clientId, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetRentals), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}