using AutoMapper;
using CarRental.Application.Contracts;
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
    public async Task<IList<ClientDto>> GetClientsByModelName(string modelName)
    {
        var models = await modelRepository.GetAll();
        var modelGenerations = await modelGenRepository.GetAll();
        var rentals = await rentalRepository.GetAll();
        var clients = await clientRepository.GetAll();
        var cars = await carRepository.GetAll();

        var modelId = models.FirstOrDefault(m => m.NameModel.Equals(modelName, StringComparison.OrdinalIgnoreCase))?.Id;
        if (modelId == null) return [];

        var modelGenIds = modelGenerations.Where(mg => mg.ModelId == modelId).Select(mg => mg.Id).ToHashSet();

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
    public async Task<IList<CarDto>> GetTop5MostRentedCars()
    {
        var rentals = await rentalRepository.GetAll();
        var cars = await carRepository.GetAll();

        var top5Cars = rentals
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Join(cars, anon => anon.CarId, c => c.Id, (anon, c) => c)
            .ToList();

        return mapper.Map<IList<CarDto>>(top5Cars);
    }

    /// <summary>
    /// Retrieves the number of rentals for every car in the system
    /// </summary>
    public async Task<Dictionary<CarDto, int>> GetRentalCountPerCar()
    {
        var rentals = await rentalRepository.GetAll();
        var cars = await carRepository.GetAll();

        var rentalCounts = rentals
            .GroupBy(r => r.CarId)
            .ToDictionary(g => g.Key, g => g.Count());

        var result = cars
            .OrderBy(c => c.LicensePlate)
            .ToDictionary(
                c => mapper.Map<CarDto>(c),
                c => rentalCounts.GetValueOrDefault(c.Id, 0)
            );

        return result;
    }

    /// <summary>
    /// Retrieves the top 5 clients based on their total cumulative rental amount
    /// </summary>
    public async Task<IList<ClientDto>> GetTop5ClientsByTotalAmount()
    {
        var modelGenerations = await modelGenRepository.GetAll();
        var rentals = await rentalRepository.GetAll();
        var clients = await clientRepository.GetAll();
        var cars = await carRepository.GetAll();

        var carDict = cars.ToDictionary(c => c.Id, c => c);
        var mgDict = modelGenerations.ToDictionary(mg => mg.Id, mg => mg);

        var top5Clients = rentals
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
            .Select(x => x.Client)
            .ToList();

        return mapper.Map<IList<ClientDto>>(top5Clients);
    }
}