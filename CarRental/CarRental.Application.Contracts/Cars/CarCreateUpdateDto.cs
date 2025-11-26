namespace CarRental.Application.Contracts.Cars;

/// <summary>
/// DTO for creating or updating a Car entity
/// </summary>
public record CarCreateUpdateDto
{
    /// <summary> 
    /// Primary key of model generation
    /// </summary>
    public required Guid ModelGenerationId { get; init; }

    /// <summary> 
    /// License plate
    /// </summary>
    public required string LicensePlate { get; init; }

    /// <summary> 
    /// Exterior color
    /// </summary>
    public string? Color { get; init; }
}