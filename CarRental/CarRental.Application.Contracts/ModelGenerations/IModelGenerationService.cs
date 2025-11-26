using CarRental.Application.Contracts.Models;

namespace CarRental.Application.Contracts.ModelGenerations;

/// <summary>
/// Application service interface for managing ModelGeneration entities
/// Inherits standard CRUD operations and provides a method to retrieve the parent Model
/// </summary>
public interface IModelGenerationService : IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves the parent Model details associated with a specific model generation
    /// </summary>
    /// <param name="modelGenerationId">The unique identifier of the ModelGeneration</param>
    /// <returns>The ModelDto of the parent model</returns>
    public Task<ModelDto> GetModel(Guid modelGenerationId);
}