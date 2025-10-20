namespace CarRental.Domain.Entities;

/// <summary>
/// Contract entity representing a car rental transaction
/// </summary>
public class Rental
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Client who rents the car
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Car being rented
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Start date and time when the car was issued to the client
    /// </summary>
    public required DateTime RentalStartTime { get; set; }

    /// <summary>
    /// Total amount charged for this rental
    /// </summary>
    public decimal TotalRentalAmount =>
        (decimal)RentalDurationHours * Car.ModelGeneration.RentalPricePerHour;

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public required double RentalDurationHours { get; set; }
}