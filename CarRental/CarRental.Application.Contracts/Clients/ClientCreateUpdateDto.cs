namespace CarRental.Application.Contracts.Clients;

/// <summary>
/// DTO for creating or updating a Client entity
/// </summary>
public record ClientCreateUpdateDto
{
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