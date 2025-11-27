using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using CarRental.Domain;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

/// <summary>
/// Service implementation for analytical queries
/// </summary>
/// <param name="rentalRepository">Repository for Rental entities</param>
/// <param name="carRepository">Repository for Car entities</param>
/// <param name="clientRepository">Repository for Client entities</param>
/// <param name="modelGenRepository">Repository for ModelGeneration entities</param>
/// <param name="modelRepository">Repository for Model entities</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping</param>
public class AnalyticsService(
    IRepository<Rental, Guid> rentalRepository,
    IRepository<Car, Guid> carRepository,
    IRepository<Client, Guid> clientRepository,
    IRepository<ModelGeneration, Guid> modelGenRepository,
    IRepository<Model, Guid> modelRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Retrieves all distinct clients who rented cars of a specified model
    /// </summary>
    /// <param name="modelId">The unique identifier of the car model to filter by</param>
    public async Task<IList<ClientDto>> GetClientsByModelId(Guid modelId)
    {
        var modelGenerations = await modelGenRepository.GetAll();
        var rentals = await rentalRepository.GetAll();
        var clients = await clientRepository.GetAll();
        var cars = await carRepository.GetAll();

        var model = await modelRepository.Get(modelId) ?? throw new KeyNotFoundException($"Model with ID {modelId} not found");

        var modelGenIds = modelGenerations
            .Where(mg => mg.ModelId == modelId)
            .Select(mg => mg.Id)
            .ToHashSet();

        if (modelGenIds.Count == 0) return [];

        var clientEntities = rentals
            .Join(cars, r => r.CarId, c => c.Id, (r, c) => new { r, c })
            .Where(x => modelGenIds.Contains(x.c.ModelGenerationId))
            .Join(clients, rc => rc.r.ClientId, cl => cl.Id, (rc, cl) => cl)
            .Distinct()
            .OrderBy(cl => cl.FullName)
            .ToList();

        return mapper.Map<IList<ClientDto>>(clientEntities);
    }

    /// <summary>
    /// Retrieves all cars that are currently in an active rental period
    /// </summary>
    public async Task<IList<CarDto>> GetCarsCurrentlyRented()
    {
        var rentals = await rentalRepository.GetAll();
        var cars = await carRepository.GetAll();
        var now = DateTime.Now;

        var carsInRent = rentals
            .Where(r => r.RentalStartTime <= now &&
                        r.RentalStartTime.AddHours(r.RentalDurationHours) > now)
            .Join(cars, r => r.CarId, c => c.Id, (r, c) => c)
            .Distinct()
            .OrderBy(c => c.LicensePlate)
            .ToList();

        return mapper.Map<IList<CarDto>>(carsInRent);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented cars
    /// </summary>
    public async Task<IList<CarRentalCountDto>> GetTop5MostRentedCars()
    {
        var rentals = await rentalRepository.GetAll();
        var cars = await carRepository.GetAll();

        var top5CarData = rentals
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Join(cars, anon => anon.CarId, c => c.Id, (anon, c) => new
            {
                Car = c,
                anon.Count
            })
            .ToList();

        var result = top5CarData.Select(x =>
        {
            var carDtoBase = mapper.Map<CarDto>(x.Car);

            return new CarRentalCountDto(x.Count)
            {
                Id = carDtoBase.Id,
                LicensePlate = carDtoBase.LicensePlate,
                Color = carDtoBase.Color,
            };
        }).ToList();

        return result;
    }

    /// <summary>
    /// Retrieves the total number of rentals for every car in the system
    /// </summary>
    /// <returns>A dictionary where the key is the car's license plate and the value is its total rental count</returns>
    public async Task<IDictionary<string, int>> GetRentalCountPerCar()
    {
        var rentals = await rentalRepository.GetAll();
        var cars = await carRepository.GetAll();

        var rentalCounts = rentals
            .GroupBy(r => r.CarId)
            .ToDictionary(g => g.Key, g => g.Count());

        var result = cars
            .OrderBy(c => c.LicensePlate)
            .ToDictionary(
                c => c.LicensePlate,
                c => rentalCounts.GetValueOrDefault(c.Id, 0)
            );

        return result;
    }

    /// <summary>
    /// Retrieves the top 5 clients based on their total cumulative rental amount
    /// </summary>
    public async Task<IList<ClientTotalAmountDto>> GetTop5ClientsByTotalAmount()
    {
        var modelGenerations = await modelGenRepository.GetAll();
        var rentals = await rentalRepository.GetAll();
        var clients = await clientRepository.GetAll();
        var cars = await carRepository.GetAll();

        var carDict = cars.ToDictionary(c => c.Id, c => c);
        var mgDict = modelGenerations.ToDictionary(mg => mg.Id, mg => mg);

        var top5ClientData = rentals
            .Select(r =>
            {
                var car = carDict.GetValueOrDefault(r.CarId);
                var mg = car != null ? mgDict.GetValueOrDefault(car.ModelGenerationId) : null;
                var price = mg?.RentalPricePerHour ?? 0;
                var amount = (decimal)r.RentalDurationHours * price;

                return new { r.ClientId, Amount = amount };
            })
            .GroupBy(x => x.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                TotalSum = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.TotalSum)
            .Take(5)
            .Join(clients, anon => anon.ClientId, cl => cl.Id, (anon, cl) => new
            {
                Client = cl,
                anon.TotalSum
            })
            .OrderByDescending(x => x.TotalSum)
            .ThenBy(x => x.Client.FullName)
            .ToList();

        var result = top5ClientData.Select(x =>
        {
            var clientDtoBase = mapper.Map<ClientDto>(x.Client);

            return new ClientTotalAmountDto(x.TotalSum)
            {
                Id = clientDtoBase.Id,
                FullName = clientDtoBase.FullName,
                DriverLicenseNumber = clientDtoBase.DriverLicenseNumber,
            };
        })
        .ToList();

        return result;
    }
}