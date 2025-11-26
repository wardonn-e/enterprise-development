namespace CarRental.Application.Contracts.Rentals;

/// <summary>
/// DTO representing a Rental entity for retrieval operations
/// </summary>
public record RentalDto
{
    /// <summary> 
    /// Primary key
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary> 
    /// Start date and time when the car was issued to the client
    /// </summary>
    public required DateTime RentalStartTime { get; init; }

    /// <summary> 
    /// Rental duration in hours
    /// </summary>
    public required double RentalDurationHours { get; init; }

    /// <summary> 
    /// Total amount charged for this rental
    /// </summary>
    public required decimal TotalRentalAmount { get; init; }
}