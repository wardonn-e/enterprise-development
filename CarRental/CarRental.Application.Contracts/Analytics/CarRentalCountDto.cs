using CarRental.Application.Contracts.Cars;

namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// DTO extending CarDto with rental count information
/// </summary>
/// <param name="CarDto">The base DTO containing car details</param>
/// <param name="RentalCount">The total number of times this car has been rented</param>
public record CarRentalCountDto(CarDto CarDto, int RentalCount);