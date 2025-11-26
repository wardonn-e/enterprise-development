using AutoMapper;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.ModelGenerations;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for managing Car business logic
/// </summary>
/// <param name="repository">The repository for Car entities</param>
/// <param name="mgRepository">The repository for ModelGeneration entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class CarService(
    IRepository<Car, Guid> repository,
    IRepository<ModelGeneration, Guid> mgRepository,
    IMapper mapper) : ICarService
{
    /// <summary>
    /// Creates a new Car entity
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created CarDto</returns>
    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        var entity = mapper.Map<Car>(dto);
        var createdEntity = await repository.Create(entity);
        return mapper.Map<CarDto>(createdEntity);
    }

    /// <summary>
    /// Retrieves a Car entity by its identifier
    /// </summary>
    /// <param name="dtoId">The Car identifier</param>
    /// <returns>The CarDto or null if the entity is not found</returns>
    public async Task<CarDto?> Get(Guid dtoId)
    {
        var entity = await repository.Get(dtoId);
        return mapper.Map<CarDto>(entity);
    }

    /// <summary>
    /// Retrieves the complete list of Car entities
    /// </summary>
    /// <returns>A list of CarDto</returns>
    public async Task<IList<CarDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IList<CarDto>>(entities);
    }

    /// <summary>
    /// Updates an existing Car entity
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the Car to be updated</param>
    /// <returns>The updated CarDto</returns>
    public async Task<CarDto> Update(CarCreateUpdateDto dto, Guid dtoId)
    {
        var existingCar = await repository.Get(dtoId) 
            ?? throw new KeyNotFoundException($"Car with ID {dtoId} not found");

        mapper.Map(dto, existingCar);

        var updatedEntity = await repository.Update(existingCar);
        return mapper.Map<CarDto>(updatedEntity);
    }

    /// <summary>
    /// Deletes a Car entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the Car to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public async Task<bool> Delete(Guid dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves the ModelGeneration details associated with a specific car
    /// </summary>
    /// <param name="carId">The unique identifier of the Car</param>
    /// <returns>The ModelGenerationDto of the car's model generation</returns>
    public async Task<ModelGenerationDto> GetModelGeneration(Guid carId)
    {
        var car = await repository.Get(carId)
            ?? throw new KeyNotFoundException($"Car with ID {carId} not found");

        var modelGeneration = car.ModelGeneration;

        modelGeneration ??= await mgRepository.Get(car.ModelGenerationId);

        if (modelGeneration == null)
        {
            throw new KeyNotFoundException($"ModelGeneration for Car {carId} not found");
        }

        return mapper.Map<ModelGenerationDto>(modelGeneration);
    }
}