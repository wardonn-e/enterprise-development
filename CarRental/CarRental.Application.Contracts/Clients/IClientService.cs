using CarRental.Application.Contracts.Rentals;

namespace CarRental.Application.Contracts.Clients;

/// <summary>
/// Application service interface for managing Client entities
/// Inherits standard CRUD operations and adds specific client-related methods
/// </summary>
public interface IClientService : IApplicationService<ClientDto, ClientCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves all rental history records associated with a specific client
    /// </summary>
    /// <param name="clientId">The unique identifier of the Client</param>
    /// <returns>A list of RentalDto</returns>
    public Task<IList<RentalDto>> GetRentals(Guid clientId);
}