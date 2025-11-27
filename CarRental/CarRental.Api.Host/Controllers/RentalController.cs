using CarRental.Application.Contracts.Rentals;
using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Contracts;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Rental entities
/// Inherits standard CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalController(
    IApplicationService<RentalDto, RentalCreateUpdateDto, Guid> rentalService,
    ILogger<RentalController> logger) : CrudControllerBase<RentalDto, RentalCreateUpdateDto, Guid>(rentalService, logger);