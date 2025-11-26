using CarRental.Domain;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository implementation for managing Rental entities using Entity Framework Core
/// </summary>
public class RentalRepository(CarRentalDbContext context) : IRepository<Rental, Guid>
{
    /// <summary>
    /// Adds a new Rental entity to the database
    /// </summary>
    /// <param name="entity">The Rental entity to be created</param>
    /// <returns>The created Rental entity with updated state</returns>
    public async Task<Rental> Create(Rental entity)
    {
        var result = await context.Rentals.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a Rental entity from the database by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Rental to delete</param>
    /// <returns>True if the Rental was successfully deleted otherwise false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var rental = await context.Rentals.FindAsync(entityId);
        if (rental == null)
            return false;

        context.Rentals.Remove(rental);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a Rental entity by its Id
    /// Includes all necessary data (Client Car and ModelGeneration) for calculation
    /// </summary>
    /// <param name="entityId">The unique identifier of the Rental to retrieve</param>
    /// <returns>The Rental entity or null if not found</returns>
    public async Task<Rental?> Get(Guid entityId)
    {
        return await context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Car)
                .ThenInclude(c => c!.ModelGeneration)
            .FirstOrDefaultAsync(r => r.Id == entityId);
    }

    /// <summary>
    /// Retrieves all Rental entities from the database
    /// Includes all necessary data (Client Car and ModelGeneration) for calculation
    /// </summary>
    /// <returns>A list of all Rental entities</returns>
    public async Task<IList<Rental>> GetAll()
    {
        return await context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Car)
                .ThenInclude(c => c!.ModelGeneration)
            .ToListAsync();
    }

    /// <summary>
    /// Updates an existing Rental entity in the database
    /// </summary>
    /// <param name="entity">The Rental entity with updated values</param>
    /// <returns>The updated Rental entity</returns>
    public async Task<Rental> Update(Rental entity)
    {
        context.Rentals.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}