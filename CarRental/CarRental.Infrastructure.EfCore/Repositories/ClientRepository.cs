using CarRental.Domain;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository implementation for managing Client entities using Entity Framework Core
/// </summary>
public class ClientRepository(CarRentalDbContext context) : IRepository<Client, Guid>
{
    /// <summary>
    /// Adds a new Client entity to the database
    /// </summary>
    /// <param name="entity">The Client entity to be created</param>
    /// <returns>The created Client entity with updated state</returns>
    public async Task<Client> Create(Client entity)
    {
        var result = await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a Client entity from the database by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Client to delete</param>
    /// <returns>True if the Client was successfully deleted otherwise false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var client = await context.Clients.FindAsync(entityId);
        if (client == null)
            return false;

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a Client entity by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Client to retrieve</param>
    /// <returns>The Client entity or null if not found</returns>
    public async Task<Client?> Get(Guid entityId)
    {
        return await context.Clients.FindAsync(entityId);
    }

    /// <summary>
    /// Retrieves all Client entities from the database
    /// </summary>
    /// <returns>A list of all Client entities</returns>
    public async Task<IList<Client>> GetAll()
    {
        return await context.Clients.ToListAsync();
    }

    /// <summary>
    /// Updates an existing Client entity in the database
    /// </summary>
    /// <param name="entity">The Client entity with updated values</param>
    /// <returns>The updated Client entity</returns>
    public async Task<Client> Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}