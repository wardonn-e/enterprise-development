namespace CarRental.Application.Contracts.Clients;

/// <summary>
/// DTO representing a Client entity for retrieval operations
/// </summary>
public record ClientDto
{
    /// <summary> 
    /// Primary key
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary> 
    /// Driver license number
    /// </summary>
    public required string DriverLicenseNumber { get; init; }

    /// <summary> 
    /// Full legal name of the client
    /// </summary>
    public required string FullName { get; init; }

    /// <summary> 
    /// Date of birth
    /// </summary>
    public DateOnly? BirthDate { get; init; }
}