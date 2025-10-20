namespace CarRental.Domain.Entities;

/// <summary>
/// Entity representing a rental service client
/// </summary>
public class Client
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Driver license number
    /// </summary>
    public required string DriverLicenseNumber { get; set; }

    /// <summary>
    /// Full legal name of the client
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateOnly? BirthDate { get; set; }
}
