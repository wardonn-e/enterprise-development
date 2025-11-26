using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Model entities
/// Inherits standard CRUD from the generic base controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelController(IApplicationService<ModelDto, ModelCreateUpdateDto, Guid> crudService, ILogger<ModelController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto, Guid>(crudService, logger);