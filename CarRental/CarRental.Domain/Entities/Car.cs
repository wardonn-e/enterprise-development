using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

/// <summary>
/// Inventory entity representing a specific car in the fleet
/// </summary>
[Table("cars")]
public class Car
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Primary key of model generation
    /// </summary>
    [Column("model_generation_id")]
    public required Guid ModelGenerationId { get; set; }

    /// <summary>
    /// Reference model
    /// </summary>
    public ModelGeneration? ModelGeneration { get; set; }

    /// <summary>
    /// License plate
    /// </summary>
    [Column("license_plate")]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Exterior color
    /// </summary>
    [Column("color")]
    public string? Color { get; set; }
}