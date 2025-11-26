namespace CarRental.Domain.Shared.Enums;

/// <summary>
/// Specifies the transmission type of a vehicle
/// </summary>
public enum GearBoxType
{
    /// <summary>
    /// Manual transmission
    /// </summary>
    Manual,

    /// <summary>
    /// Automatic torque-converter transmission
    /// </summary>
    Automatic,

    /// <summary>
    /// Continuously variable transmission
    /// </summary>
    CVT,

    /// <summary>
    /// Dual-clutch transmission
    /// </summary>
    DCT
}