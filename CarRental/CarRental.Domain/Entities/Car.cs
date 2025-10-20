namespace CarRental.Domain.Entities;

/// <summary>
/// Inventory entity representing a specific car in the fleet
/// </summary>
public class Car
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Reference model
    /// </summary>
    public required ModelGeneration ModelGeneration { get; set; }

    /// <summary>
    /// License plate
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Exterior color
    /// </summary>
    public string? Color { get; set; }
}