using CarRental.Application.Contracts.ModelGenerations;

namespace CarRental.Application.Contracts.Cars;

/// <summary>
/// Application service interface for managing Car entities
/// Inherits standard CRUD operations and adds specific car-related methods
/// </summary>
public interface ICarService : IApplicationService<CarDto, CarCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves the ModelGeneration details associated with a specific car
    /// </summary>
    /// <param name="carId">The unique identifier of the Car</param>
    /// <returns>The ModelGenerationDto of the car's model generation</returns>
    public Task<ModelGenerationDto> GetModelGeneration(Guid carId);
}