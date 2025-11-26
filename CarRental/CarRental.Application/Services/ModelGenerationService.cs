using AutoMapper;
using CarRental.Application.Contracts.ModelGenerations;
using CarRental.Application.Contracts.Models;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for managing ModelGeneration business logic
/// </summary>
/// <param name="repository">The repository for ModelGeneration entities</param>
/// <param name="modelRepository">The repository for Model entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class ModelGenerationService(
    IRepository<ModelGeneration, Guid> repository,
    IRepository<Model, Guid> modelRepository,
    IMapper mapper) : IModelGenerationService
{
    /// <summary>
    /// Creates a new ModelGeneration entity
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created ModelGenerationDto</returns>
    public async Task<ModelGenerationDto> Create(ModelGenerationCreateUpdateDto dto)
    {
        var entity = mapper.Map<ModelGeneration>(dto);
        var createdEntity = await repository.Create(entity);
        return mapper.Map<ModelGenerationDto>(createdEntity);
    }

    /// <summary>
    /// Deletes a ModelGeneration entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the ModelGeneration to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public async Task<bool> Delete(Guid dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Retrieves a ModelGeneration entity by its identifier
    /// </summary>
    /// <param name="dtoId">The ModelGeneration identifier</param>
    /// <returns>The ModelGenerationDto or null if the entity is not found</returns>
    public async Task<ModelGenerationDto?> Get(Guid dtoId)
    {
        var entity = await repository.Get(dtoId);
        return mapper.Map<ModelGenerationDto>(entity);
    }

    /// <summary>
    /// Retrieves the complete list of ModelGeneration entities
    /// </summary>
    /// <returns>A list of ModelGenerationDto</returns>
    public async Task<IList<ModelGenerationDto>> GetAll()
    {
        var entities = await repository.GetAll();
        return mapper.Map<IList<ModelGenerationDto>>(entities);
    }

    /// <summary>
    /// Updates an existing ModelGeneration entity
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the ModelGeneration to be updated</param>
    /// <returns>The updated ModelGenerationDto</returns>
    public async Task<ModelGenerationDto> Update(ModelGenerationCreateUpdateDto dto, Guid dtoId)
    {
        var existingModelGen = await repository.Get(dtoId)
             ?? throw new KeyNotFoundException($"ModelGeneration with ID {dtoId} not found");

        mapper.Map(dto, existingModelGen);

        var updatedEntity = await repository.Update(existingModelGen);
        return mapper.Map<ModelGenerationDto>(updatedEntity);
    }

    /// <summary>
    /// Retrieves the parent Model details associated with a specific model generation
    /// </summary>
    /// <param name="modelGenerationId">The unique identifier of the ModelGeneration</param>
    /// <returns>The ModelDto of the parent model</returns>
    public async Task<ModelDto> GetModel(Guid modelGenerationId)
    {
        var modelGeneration = await repository.Get(modelGenerationId)
            ?? throw new KeyNotFoundException($"ModelGeneration with ID {modelGenerationId} not found");

        var model = modelGeneration.Model;

        model ??= await modelRepository.Get(modelGeneration.ModelId);

        if (model == null)
        {
            throw new KeyNotFoundException($"Model for ModelGeneration {modelGenerationId} not found");
        }

        return mapper.Map<ModelDto>(model);
    }
}