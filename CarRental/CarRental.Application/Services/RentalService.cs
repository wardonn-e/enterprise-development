using AutoMapper;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using CarRental.Application.Contracts.Rentals;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for managing Rental business logic
/// </summary>
/// <param name="repository">The repository for Rental entities</param>
/// <param name="carRepository">The repository for Car entities</param>
/// <param name="clientRepository">The repository for Client entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class RentalService(
    IRepository<Rental, Guid> repository,
    IRepository<Car, Guid> carRepository,
    IRepository<Client, Guid> clientRepository,
    IMapper mapper) : IRentalService
{
    /// <summary>
    /// Creates a new Rental entity
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created RentalDto</returns>
    public async Task<RentalDto> Create(RentalCreateUpdateDto dto)
    {
        var entity = mapper.Map<Rental>(dto);
        var createdEntity = await repository.Create(entity);

        var loadedEntity = await repository.Get(createdEntity.Id);

        return mapper.Map<RentalDto>(loadedEntity);
    }

    /// <summary>
    /// Deletes a Rental entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the Rental to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public async Task<bool> Delete(Guid dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a Rental entity by its identifier
    /// </summary>
    /// <param name="dtoId">The Rental identifier</param>
    /// <returns>The RentalDto or null if the entity is not found</returns>
    public async Task<RentalDto?> Get(Guid dtoId)
    {
        var entity = await repository.Get(dtoId);
        return mapper.Map<RentalDto>(entity);
    }

    /// <summary>
    /// Retrieves the complete list of Rental entities
    /// </summary>
    /// <returns>A list of RentalDto</returns>
    public async Task<IList<RentalDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IList<RentalDto>>(entities);
    }

    /// <summary>
    /// Updates an existing Rental entity
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the Rental to be updated</param>
    /// <returns>The updated RentalDto</returns>
    public async Task<RentalDto> Update(RentalCreateUpdateDto dto, Guid dtoId)
    {
        var existingRental = await repository.Get(dtoId)
             ?? throw new KeyNotFoundException($"Rental with ID {dtoId} not found");

        mapper.Map(dto, existingRental);

        var updatedEntity = await repository.Update(existingRental);
        return mapper.Map<RentalDto>(updatedEntity);
    }

    /// <summary>
    /// Retrieves the Car details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The CarDto of the rented car</returns>
    public async Task<CarDto> GetCar(Guid rentalId)
    {
        var rental = await repository.Get(rentalId)
            ?? throw new KeyNotFoundException($"Rental with ID {rentalId} not found");

        var car = rental.Car;

        car ??= await carRepository.Get(rental.CarId);

        if (car == null)
        {
            throw new KeyNotFoundException($"Car for Rental {rentalId} not found");
        }

        return mapper.Map<CarDto>(car);
    }

    /// <summary>
    /// Retrieves the Client details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The ClientDto of the client</returns>
    public async Task<ClientDto> GetClient(Guid rentalId)
    {
        var rental = await repository.Get(rentalId)
            ?? throw new KeyNotFoundException($"Rental with ID {rentalId} not found");

        var client = rental.Client;

        client ??= await clientRepository.Get(rental.ClientId);

        if (client == null)
        {
            throw new KeyNotFoundException($"Client for Rental {rentalId} not found");
        }

        return mapper.Map<ClientDto>(client);
    }
}