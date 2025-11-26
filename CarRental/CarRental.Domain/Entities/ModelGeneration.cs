using CarRental.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

/// <summary>
/// Catalog entity describing a specific generation of a model,
/// including year, engine capacity, gearbox, and hourly rental price
/// </summary>
[Table("model_generations")]
public class ModelGeneration
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Primary key of model
    /// </summary>
    [Column("model_id")]
    public required Guid ModelId { get; set; }

    /// <summary>
    /// Reference to the parent model
    /// </summary>
    public Model? Model { get; set; }

    /// <summary>
    /// Manufacturing year of this generation
    /// </summary>
    [Column("year_manufacture")]
    public required int YearManufacture { get; set; }

    /// <summary>
    /// Engine capacity in liters
    /// </summary>
    [Column("engine_capacity")]
    public required double EngineCapacity { get; set; }

    /// <summary>
    /// Hourly rental price applicable to cars of this generation
    /// </summary>
    [Column("rental_price_per_hour")]
    public required decimal RentalPricePerHour { get; set; }

    /// <summary>
    /// Transmission type for this generation
    /// </summary>
    [Column("gear_box_type")]
    public required GearBoxType GearBoxType { get; set; }
}