using CarRental.Domain;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository implementation for managing ModelGeneration entities using Entity Framework Core
/// </summary>
public class ModelGenerationRepository(CarRentalDbContext context) : IRepository<ModelGeneration, Guid>
{
    /// <summary>
    /// Adds a new ModelGeneration entity to the database
    /// </summary>
    /// <param name="entity">The ModelGeneration entity to be created</param>
    /// <returns>The created ModelGeneration entity with updated state</returns>
    public async Task<ModelGeneration> Create(ModelGeneration entity)
    {
        var result = await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a ModelGeneration entity from the database by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the ModelGeneration to delete</param>
    /// <returns>True if the ModelGeneration was successfully deleted otherwise false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var modelGen = await context.ModelGenerations.FindAsync(entityId);
        if (modelGen == null)
            return false;

        context.ModelGenerations.Remove(modelGen);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a ModelGeneration entity by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the ModelGeneration to retrieve</param>
    /// <returns>The ModelGeneration entity or null if not found</returns>
    public async Task<ModelGeneration?> Get(Guid entityId)
    {
        return await context.ModelGenerations
            .Include(mg => mg.Model)
            .FirstOrDefaultAsync(mg => mg.Id == entityId);
    }

    /// <summary>
    /// Retrieves all ModelGeneration entities from the database
    /// </summary>
    /// <returns>A list of all ModelGeneration entities</returns>
    public async Task<IList<ModelGeneration>> GetAll()
    {
        return await context.ModelGenerations
            .Include(mg => mg.Model)
            .ToListAsync();
    }

    /// <summary>
    /// Updates an existing ModelGeneration entity in the database
    /// </summary>
    /// <param name="entity">The ModelGeneration entity with updated values</param>
    /// <returns>The updated ModelGeneration entity</returns>
    public async Task<ModelGeneration> Update(ModelGeneration entity)
    {
        context.ModelGenerations.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}