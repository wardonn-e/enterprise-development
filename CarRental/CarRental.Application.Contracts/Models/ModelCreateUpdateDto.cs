using CarRental.Domain.Shared.Enums;

namespace CarRental.Application.Contracts.Models;

/// <summary>
/// DTO for creating or updating a Model entity
/// </summary>
public record ModelCreateUpdateDto
{
    /// <summary> 
    /// Human-readable model name
    /// </summary>
    public required string NameModel { get; init; }

    /// <summary> 
    /// Drivetrain type
    /// </summary>
    public required TypeDrive TypeDrive { get; init; }

    /// <summary> 
    /// Number of seats
    /// </summary>
    public required int SeatNumber { get; init; }

    /// <summary> 
    /// Body type classification
    /// </summary>
    public required BodyCarType BodyCarType { get; init; }

    /// <summary> 
    /// Vehicle class
    /// </summary>
    public required ClassCar ClassCar { get; init; }
}