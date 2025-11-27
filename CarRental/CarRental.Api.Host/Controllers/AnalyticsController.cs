using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for retrieving analytical data about rentals, cars, and clients
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all distinct clients who rented cars of a specified model, ordered by full name
    /// </summary>
    /// <param name="modelId">The unique identifier of the car model to filter by</param>
    /// <returns>A list of ClientDto</returns>
    [HttpGet("clients-by-model-id")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientDto>>> GetClientsByModelId([FromQuery] Guid modelId)
    {
        try
        {
            var clients = await analyticsService.GetClientsByModelId(modelId);
            logger.LogInformation("Retrieved {Count} clients who rented model with ID '{ModelId}'", clients.Count, modelId);
            return Ok(clients);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Model not found for ID {ModelId}", modelId);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving clients by model ID.");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves all cars that are currently in an active rental period
    /// </summary>
    /// <returns>A list of CarDto</returns>
    [HttpGet("cars-currently-rented")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CarDto>>> GetCarsCurrentlyRented()
    {
        try
        {
            var cars = await analyticsService.GetCarsCurrentlyRented();
            logger.LogInformation("Retrieved {Count} cars currently rented.", cars.Count);
            return Ok(cars);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving currently rented cars.");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars, ordered by rental count descending
    /// </summary>
    /// <returns>A list of CarRentalCountDto</returns>
    [HttpGet("top-5-most-rented-cars")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CarRentalCountDto>>> GetTop5MostRentedCars()
    {
        try
        {
            var cars = await analyticsService.GetTop5MostRentedCars();
            logger.LogInformation("Retrieved top 5 most rented cars.");
            return Ok(cars);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving top 5 most rented cars.");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the number of rentals for every car in the system
    /// </summary>
    /// <returns>A dictionary mapping License plate to its total rental count</returns>
    [HttpGet("rental-count-per-car")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IDictionary<string, int>>> GetRentalCountPerCar()
    {
        try
        {
            var counts = await analyticsService.GetRentalCountPerCar();
            logger.LogInformation("Retrieved rental counts for {Count} cars.", counts.Count);
            return Ok(counts);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving rental count per car.");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the top 5 clients based on their total cumulative rental amount
    /// </summary>
    /// <returns>A list of ClientTotalAmountDto</returns>
    [HttpGet("top-5-clients-by-total-amount")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientTotalAmountDto>>> GetTop5ClientsByTotalAmount()
    {
        try
        {
            var clients = await analyticsService.GetTop5ClientsByTotalAmount();
            logger.LogInformation("Retrieved top 5 clients by total rental amount.");
            return Ok(clients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving top 5 clients by total amount.");
            return StatusCode(500, ex.Message);
        }
    }
}