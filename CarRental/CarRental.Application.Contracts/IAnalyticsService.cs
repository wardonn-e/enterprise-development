using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;

namespace CarRental.Application.Contracts;

/// <summary>
/// Application service interface for analytical queries
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Retrieves all distinct clients who rented cars of a specified model
    /// The results are ordered alphabetically by full name
    /// </summary>
    /// <param name="modelId">The unique identifier of the car model to filter by</param>
    /// <returns>A list of ClientDto</returns>
    public Task<IList<ClientDto>> GetClientsByModelId(Guid modelId);

    /// <summary>
    /// Retrieves all cars that are currently in an active rental period
    /// </summary>
    /// <returns>A list of CarDto</returns>
    public Task<IList<CarDto>> GetCarsCurrentlyRented();

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars
    /// The results include the rental count and are ordered by rental count in descending order
    /// </summary>
    /// <returns>A list of CarRentalCountDto</returns>
    public Task<IList<CarRentalCountDto>> GetTop5MostRentedCars();

    /// <summary>
    /// Retrieves the total number of rentals for every car in the system
    /// </summary>
    /// <returns>A dictionary where the key is the car's license plate and the value is its total rental count</returns>
    public Task<IDictionary<string, int>> GetRentalCountPerCar();

    /// <summary>
    /// Retrieves the top 5 clients based on their total cumulative rental amount
    /// Results include the total amount and are ordered by total amount descending and full name ascending
    /// </summary>
    /// <returns>A list of ClientTotalAmountDto</returns>
    public Task<IList<ClientTotalAmountDto>> GetTop5ClientsByTotalAmount();
}