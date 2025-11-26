using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

/// <summary>
/// Entity representing a rental service client
/// </summary>
[Table("clients")]
public class Client
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Driver license number
    /// </summary>
    [Column("driver_license_number")]
    public required string DriverLicenseNumber { get; set; }

    /// <summary>
    /// Full legal name of the client
    /// </summary>
    [Column("full_name")]
    public required string FullName { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    [Column("birth_date")]
    public DateOnly? BirthDate { get; set; }
}
