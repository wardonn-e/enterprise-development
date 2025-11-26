using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Clients;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for managing Client business logic
/// </summary>
/// <param name="repository">The repository for Client entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class ClientService(IRepository<Client, Guid> repository, IMapper mapper) : IApplicationService<ClientDto, ClientCreateUpdateDto, Guid>
{
    /// <summary>
    /// Creates a new Client entity
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created ClientDto</returns>
    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var entity = mapper.Map<Client>(dto);
        var createdEntity = await repository.Create(entity);
        return mapper.Map<ClientDto>(createdEntity);
    }

    /// <summary>
    /// Deletes a Client entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the Client to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public async Task<bool> Delete(Guid dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a Client entity by its identifier
    /// </summary>
    /// <param name="dtoId">The Client identifier</param>
    /// <returns>The ClientDto or null if the entity is not found</returns>
    public async Task<ClientDto?> Get(Guid dtoId)
    {
        var entity = await repository.Get(dtoId);
        return mapper.Map<ClientDto>(entity);
    }

    /// <summary>
    /// Retrieves the complete list of Client entities
    /// </summary>
    /// <returns>A list of ClientDto</returns>
    public async Task<IList<ClientDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IList<ClientDto>>(entities);
    }

    /// <summary>
    /// Updates an existing Client entity
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the Client to be updated</param>
    /// <returns>The updated ClientDto</returns>
    public async Task<ClientDto> Update(ClientCreateUpdateDto dto, Guid dtoId)
    {
        var existingClient = await repository.Get(dtoId)
             ?? throw new KeyNotFoundException($"Client with ID {dtoId} not found");

        mapper.Map(dto, existingClient);

        var updatedEntity = await repository.Update(existingClient);
        return mapper.Map<ClientDto>(updatedEntity);
    }
}