namespace CarRental.Application.Contracts.Cars;

/// <summary>
/// DTO representing a Car entity for retrieval operations
/// </summary>
public record CarDto
{
    /// <summary> 
    /// Primary key
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary> 
    /// License plate
    /// </summary>
    public required string LicensePlate { get; init; }

    /// <summary> 
    /// Exterior color
    /// </summary>
    public string? Color { get; init; }
}