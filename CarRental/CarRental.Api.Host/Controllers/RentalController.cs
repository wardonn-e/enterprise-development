using CarRental.Application.Contracts.Rentals;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Rental entities
/// Inherits standard CRUD operations and provides endpoints to retrieve related Car and Client entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalController(
    IRentalService rentalService,
    ILogger<RentalController> logger
) : CrudControllerBase<RentalDto, RentalCreateUpdateDto, Guid>(rentalService, logger)
{
    /// <summary>
    /// Retrieves the Car details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The CarDto of the rented car</returns>
    [HttpGet("{rentalId}/Car")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarDto>> GetCar(Guid rentalId)
    {
        try
        {
            var carDto = await rentalService.GetCar(rentalId);

            logger.LogInformation("Retrieved Car for Rental {Id} in {Controller}", rentalId, GetType().Name);
            return Ok(carDto);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Rental or its associated Car not found for ID {Id} in {Controller}", rentalId, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetCar), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves the Client details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The ClientDto of the client</returns>
    [HttpGet("{rentalId}/Client")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> GetClient(Guid rentalId)
    {
        try
        {
            var clientDto = await rentalService.GetClient(rentalId);

            logger.LogInformation("Retrieved Client for Rental {Id} in {Controller}", rentalId, GetType().Name);
            return Ok(clientDto);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Rental or its associated Client not found for ID {Id} in {Controller}", rentalId, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetClient), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}