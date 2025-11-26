using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.ModelGenerations;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Car entities
/// Inherits standard CRUD operations and provides an endpoint to retrieve the associated Model Generation
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarController(
    ICarService carService,
    ILogger<CarController> logger
) : CrudControllerBase<CarDto, CarCreateUpdateDto, Guid>(carService, logger)
{
    /// <summary>
    /// Retrieves the ModelGeneration details associated with a specific car
    /// </summary>
    /// <param name="carId">The unique identifier of the Car</param>
    /// <returns>The ModelGenerationDto of the car's model generation</returns>
    [HttpGet("{carId}/ModelGeneration")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ModelGenerationDto>> GetModelGeneration(Guid carId)
    {
        try
        {
            var modelGenerationDto = await carService.GetModelGeneration(carId);

            logger.LogInformation("Retrieved ModelGeneration for Car {Id} in {Controller}", carId, GetType().Name);
            return Ok(modelGenerationDto);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Car or its associated ModelGeneration not found for ID {Id} in {Controller}", carId, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetModelGeneration), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}