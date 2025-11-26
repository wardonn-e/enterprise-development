namespace CarRental.Application.Contracts;

/// <summary>
/// Application service interface for basic CRUD operations
/// </summary>
/// <typeparam name="TDto">DTO for Get requests</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO for Post/Put requests</typeparam>
/// <typeparam name="TKey">The type of the DTO identifier</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity based on the DTO
    /// </summary>
    /// <param name="dto">The DTO containing the creation data</param>
    /// <returns>The created DTO</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="dtoId">The entity identifier</param>
    /// <returns>The DTO or null if the entity is not found</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Retrieves the complete list of entities
    /// </summary>
    /// <returns>A list of DTOs</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Updates an entity by its identifier
    /// </summary>
    /// <param name="dto">The DTO containing the updated values</param>
    /// <param name="dtoId">The identifier of the entity to be updated</param>
    /// <returns>The updated DTO</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="dtoId">The identifier of the entity to be deleted</param>
    /// <returns>true if the deletion was successful otherwise false</returns>
    public Task<bool> Delete(TKey dtoId);
}