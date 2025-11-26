using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Client entities
/// Inherits standard CRUD from the generic base controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto, Guid> crudService, ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto, Guid>(crudService, logger);