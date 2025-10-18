using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Catalog entity describing a specific generation of a model,
/// including year, engine capacity, gearbox, and hourly rental price
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Reference to the parent model
    /// </summary>
    public required Model Model { get; set; }

    /// <summary>
    /// Manufacturing year of this generation
    /// </summary>
    public required int YearManufacture { get; set; }

    /// <summary>
    /// Engine capacity in liters
    /// </summary>
    public required double EngineCapacity { get; set; }

    /// <summary>
    /// Hourly rental price applicable to cars of this generation
    /// </summary>
    public required double RentalPricePerHour { get; set; }

    /// <summary>
    /// Transmission type for this generation
    /// </summary>
    public required GearBoxType GearBoxType { get; set; }
}