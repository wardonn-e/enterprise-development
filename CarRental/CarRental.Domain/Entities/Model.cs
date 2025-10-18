using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Catalog entity describing a car model
/// </summary>
public class Model
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Human-readable model name
    /// </summary>
    public required string NameModel { get; set; }

    /// <summary>
    /// Drivetrain type
    /// </summary>
    public required TypeDrive TypeDrive { get; set; }

    /// <summary>
    /// Number of seats
    /// </summary>
    public required int SeatNumber { get; set; }

    /// <summary>
    /// Body type classification
    /// </summary>
    public required BodyCarType BodyCarType { get; set; }

    /// <summary>
    /// Vehicle class
    /// </summary>
    public required ClassCar ClassCar { get; set; }
}