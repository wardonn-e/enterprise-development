using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Models;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for managing Model business logic
/// </summary>
/// <param name="repository">The repository for Model entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class ModelService(IRepository<Model, Guid> repository, IMapper mapper) : IApplicationService<ModelDto, ModelCreateUpdateDto, Guid>
{
    /// <summary>
    /// Creates a new Model entity
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created ModelDto</returns>
    public async Task<ModelDto> Create(ModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<Model>(dto);
        var createdEntity = await repository.Create(entity);
        return mapper.Map<ModelDto>(createdEntity);
    }

    /// <summary>
    /// Deletes a Model entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the Model to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public async Task<bool> Delete(Guid dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a Model entity by its identifier
    /// </summary>
    /// <param name="dtoId">The Model identifier</param>
    /// <returns>The ModelDto or null if the entity is not found</returns>
    public async Task<ModelDto?> Get(Guid dtoId)
    {
        var entity = await repository.Get(dtoId);
        return mapper.Map<ModelDto>(entity);
    }

    /// <summary>
    /// Retrieves the complete list of Model entities
    /// </summary>
    /// <returns>A list of ModelDto</returns>
    public async Task<IList<ModelDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IList<ModelDto>>(entities);
    }

    /// <summary>
    /// Updates an existing Model entity
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the Model to be updated</param>
    /// <returns>The updated ModelDto</returns>
    public async Task<ModelDto> Update(ModelCreateUpdateDto dto, Guid dtoId)
    {
        var existingModel = await repository.Get(dtoId)
             ?? throw new KeyNotFoundException($"Model with ID {dtoId} not found");

        mapper.Map(dto, existingModel);

        var updatedEntity = await repository.Update(existingModel);
        return mapper.Map<ModelDto>(updatedEntity);
    }
}