using CarRental.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// Base controller implementing CRUD operations for entities
/// </summary>
/// <typeparam name="TDto">Entity DTO</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO for creating/updating the entity</typeparam>
/// <typeparam name="TKey">Entity key type</typeparam>
[Route("api/[controller]")]
[ApiController]
public class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Retrieves all records
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var items = await appService.GetAll();
            logger.LogInformation("Retrieved {Count} items in {Controller}", items.Count, GetType().Name);
            return Ok(items);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves a record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the record</param>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            var item = await appService.Get(id);
            logger.LogInformation("Retrieved item {Id} in {Controller}", id, GetType().Name);
            return item != null ? Ok(item) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Creates a new record
    /// </summary>
    /// <param name="dto">The DTO containing the data for the new record</param>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto dto)
    {
        try
        {
            var created = await appService.Create(dto);
            logger.LogInformation("Created new item with id in {Controller}", GetType().Name);
            return CreatedAtAction(nameof(Create), created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(Create), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Updates a record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the record to update</param>
    /// <param name="dto">The DTO containing the updated data</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update(TKey id, TCreateUpdateDto dto)
    {
        try
        {
            var updated = await appService.Update(dto, id);
            logger.LogInformation("Updated item {Id} in {Controller}", id, GetType().Name);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Updating item with ID {Id} not found in {Controller}", id, GetType().Name);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(Update), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes a record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the record to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(TKey id)
    {
        try
        {
            await appService.Delete(id);
            logger.LogInformation("Deleted item {Id} in {Controller}", id, GetType().Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {Method} method of {Controller}", nameof(Delete), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}