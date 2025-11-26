using CarRental.Application.Contracts.ModelGenerations;
using CarRental.Application.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Model Generation entities
/// Inherits standard CRUD operations and provides endpoints to retrieve related entities
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationController(
    IModelGenerationService modelGenerationService,
    ILogger<ModelGenerationController> logger
) : CrudControllerBase<ModelGenerationDto, ModelGenerationCreateUpdateDto, Guid>(modelGenerationService, logger)
{
    /// <summary>
    /// Retrieves the parent Model details associated with a specific model generation
    /// </summary>
    /// <param name="modelGenerationId">The unique identifier of the ModelGeneration</param>
    /// <returns>The ModelDto of the parent model</returns>
    [HttpGet("{modelGenerationId}/Model")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ModelDto>> GetModel(Guid modelGenerationId)
    {
        try
        {
            var modelDto = await modelGenerationService.GetModel(modelGenerationId);

            logger.LogInformation("Retrieved Model for ModelGeneration {Id} in {Controller}", modelGenerationId, GetType().Name);
            return Ok(modelDto);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "ModelGeneration or its parent Model not found for ID {Id} in {Controller}", modelGenerationId, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetModel), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}