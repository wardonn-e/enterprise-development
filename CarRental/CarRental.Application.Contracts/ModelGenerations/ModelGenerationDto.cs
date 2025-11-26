using CarRental.Domain.Shared.Enums;

namespace CarRental.Application.Contracts.ModelGenerations;

/// <summary>
/// DTO representing a ModelGeneration entity for retrieval operations
/// </summary>
public record ModelGenerationDto
{
    /// <summary> 
    /// Primary key
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary> 
    /// Manufacturing year of this generation
    /// </summary>
    public required int YearManufacture { get; init; }

    /// <summary> 
    /// Engine capacity in liters
    /// </summary>
    public required double EngineCapacity { get; init; }

    /// <summary> 
    /// Hourly rental price applicable to cars of this generation
    /// </summary>
    public required decimal RentalPricePerHour { get; init; }

    /// <summary> 
    /// Transmission type for this generation
    /// </summary>
    public required GearBoxType GearBoxType { get; init; }
}