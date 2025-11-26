using CarRental.Domain;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository implementation for managing Model entities using Entity Framework Core
/// </summary>
public class ModelRepository(CarRentalDbContext context) : IRepository<Model, Guid>
{
    /// <summary>
    /// Adds a new Model entity to the database
    /// </summary>
    /// <param name="entity">The Model entity to be created</param>
    /// <returns>The created Model entity with updated state</returns>
    public async Task<Model> Create(Model entity)
    {
        var result = await context.Models.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a Model entity from the database by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Model to delete</param>
    /// <returns>True if the Model was successfully deleted otherwise false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var model = await context.Models.FindAsync(entityId);
        if (model == null)
            return false;

        context.Models.Remove(model);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a Model entity by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Model to retrieve</param>
    /// <returns>The Model entity or null if not found</returns>
    public async Task<Model?> Get(Guid entityId)
    {
        return await context.Models.FindAsync(entityId);
    }

    /// <summary>
    /// Retrieves all Model entities from the database
    /// </summary>
    /// <returns>A list of all Model entities</returns>
    public async Task<IList<Model>> GetAll()
    {
        return await context.Models.ToListAsync();
    }

    /// <summary>
    /// Updates an existing Model entity in the database
    /// </summary>
    /// <param name="entity">The Model entity with updated values</param>
    /// <returns>The updated Model entity</returns>
    public async Task<Model> Update(Model entity)
    {
        context.Models.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}