using CarRental.Domain;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository implementation for managing Car entities using Entity Framework Core
/// </summary>
public class CarRepository(CarRentalDbContext context) : IRepository<Car, Guid>
{
    /// <summary>
    /// Adds a new Car entity to the database
    /// </summary>
    /// <param name="entity">The Car entity to be created</param>
    /// <returns>The created Car entity with updated state</returns>
    public async Task<Car> Create(Car entity)
    {
        var result = await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a Car entity from the database by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Car to delete</param>
    /// <returns>True if the Car was successfully deleted otherwise false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var car = await context.Cars.FindAsync(entityId);
        if (car == null)
            return false;

        context.Cars.Remove(car);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a Car entity by its Id
    /// </summary>
    /// <param name="entityId">The unique identifier of the Car to retrieve</param>
    /// <returns>The Car entity or null if not found</returns>
    public async Task<Car?> Get(Guid entityId)
    {
        return await context.Cars
            .Include(c => c.ModelGeneration)
            .FirstOrDefaultAsync(c => c.Id == entityId);
    }

    /// <summary>
    /// Retrieves all Car entities from the database
    /// </summary>
    /// <returns>A list of all Car entities</returns>
    public async Task<IList<Car>> GetAll()
    {
        return await context.Cars
            .Include(c => c.ModelGeneration)
            .ToListAsync();
    }

    /// <summary>
    /// Updates an existing Car entity in the database
    /// </summary>
    /// <param name="entity">The Car entity with updated values</param>
    /// <returns>The updated Car entity</returns>
    public async Task<Car> Update(Car entity)
    {
        context.Cars.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}