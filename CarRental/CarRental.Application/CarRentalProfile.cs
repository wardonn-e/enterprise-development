using AutoMapper;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using CarRental.Application.Contracts.ModelGenerations;
using CarRental.Application.Contracts.Models;
using CarRental.Application.Contracts.Rentals;
using CarRental.Domain.Entities;

namespace CarRental.Application;

/// <summary>
/// AutoMapper profile defining mappings between Domain Entities and DTOs
/// </summary>
public class CarRentalProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the CarRentalProfile class
    /// Configures bidirectional mappings for all entities
    /// between the domain model and corresponding DTOs
    /// </summary>
    public CarRentalProfile() 
    {
        CreateMap<Car, CarDto>();
        CreateMap<CarCreateUpdateDto, Car>();

        CreateMap<Client, ClientDto>();
        CreateMap<ClientCreateUpdateDto, Client>();

        CreateMap<ModelGeneration, ModelGenerationDto>();
        CreateMap<ModelGenerationCreateUpdateDto, ModelGeneration>();

        CreateMap<Model, ModelDto>();
        CreateMap<ModelCreateUpdateDto, Model>();

        CreateMap<Rental, RentalDto>();
        CreateMap<RentalCreateUpdateDto, Rental>();
    }
}