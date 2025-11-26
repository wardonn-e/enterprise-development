namespace CarRental.Application.Contracts.Rentals;

/// <summary>
/// DTO for creating or updating a Rental entity
/// </summary>
public record RentalCreateUpdateDto
{
    /// <summary> 
    /// Primary key of client
    /// </summary>
    public required Guid ClientId { get; init; }

    /// <summary> 
    /// Primary key of car
    /// </summary>
    public required Guid CarId { get; init; }

    /// <summary> 
    /// Start date and time when the car was issued to the client
    /// </summary>
    public required DateTime RentalStartTime { get; init; }

    /// <summary> 
    /// Rental duration in hours
    /// </summary>
    public required double RentalDurationHours { get; init; }
}