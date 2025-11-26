using CarRental.Domain.Shared.Enums;

namespace CarRental.Application.Contracts.ModelGenerations;

/// <summary>
/// DTO for creating or updating a ModelGeneration entity
/// </summary>
public record ModelGenerationCreateUpdateDto
{
    /// <summary> 
    /// The unique identifier of the parent Model
    /// </summary>
    public required Guid ModelId { get; init; }

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