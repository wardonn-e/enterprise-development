using CarRental.Application.Contracts.Clients;

namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// DTO extending ClientDto with total rental amount information
/// </summary>
/// <param name="TotalRentalAmount">The total cumulative amount paid by the client across all rentals</param>
public record ClientTotalAmountDto(decimal TotalRentalAmount) : ClientDto;