using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;

namespace CarRental.Application.Contracts.Rentals;

/// <summary>
/// Application service interface for managing Rental entities
/// Inherits standard CRUD operations and provides methods to retrieve related entities
/// </summary>
public interface IRentalService : IApplicationService<RentalDto, RentalCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves the Client details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The ClientDto of the client who made the rental</returns>
    public Task<ClientDto> GetClient(Guid rentalId);

    /// <summary>
    /// Retrieves the Car details associated with a specific rental
    /// </summary>
    /// <param name="rentalId">The unique identifier of the Rental</param>
    /// <returns>The CarDto of the car included in the rental</returns>
    public Task<CarDto> GetCar(Guid rentalId);
}