using CarRental.Application.Contracts.Cars;

namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// DTO extending CarDto with rental count information
/// </summary>
/// <param name="RentalCount">The total number of times this car has been rented</param>
public record CarRentalCountDto(int RentalCount) : CarDto;