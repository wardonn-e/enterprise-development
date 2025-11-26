using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

/// <summary>
/// Contract entity representing a car rental transaction
/// </summary>
[Table("rentals")]
public class Rental
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Primary key of client
    /// </summary>
    [Column("client_id")]
    public required Guid ClientId { get; set; }

    /// <summary>
    /// Client who rents the car
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Primary key of car
    /// </summary>
    [Column("car_id")]
    public required Guid CarId { get; set; }

    /// <summary>
    /// Car being rented
    /// </summary>
    public Car? Car { get; set; }

    /// <summary>
    /// Start date and time when the car was issued to the client
    /// </summary>
    [Column("rental_start_time")]
    public required DateTime RentalStartTime { get; set; }

    /// <summary>
    /// Total amount charged for this rental
    /// </summary>
    [Column("total_rental_amount")]
    public decimal TotalRentalAmount =>
        (decimal)RentalDurationHours * Car!.ModelGeneration!.RentalPricePerHour;

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    [Column("rental_duration_hours")]
    public required double RentalDurationHours { get; set; }
}