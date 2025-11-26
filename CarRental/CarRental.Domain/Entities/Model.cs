using CarRental.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

/// <summary>
/// Catalog entity describing a car model
/// </summary>
[Table("models")]
public class Model
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Human-readable model name
    /// </summary>
    [Column("name")]
    public required string NameModel { get; set; }

    /// <summary>
    /// Drivetrain type
    /// </summary>
    [Column("type_drive")]
    public required TypeDrive TypeDrive { get; set; }

    /// <summary>
    /// Number of seats
    /// </summary>
    [Column("seat_number")]
    public required int SeatNumber { get; set; }

    /// <summary>
    /// Body type classification
    /// </summary>
    [Column("body_car_type")]
    public required BodyCarType BodyCarType { get; set; }

    /// <summary>
    /// Vehicle class
    /// </summary>
    [Column("class_car")]
    public required ClassCar ClassCar { get; set; }
}