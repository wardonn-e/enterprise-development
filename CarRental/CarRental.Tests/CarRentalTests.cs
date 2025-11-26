using System;
using System.Linq;
using Xunit;
using CarRental.Domain.Data;

namespace CarRental.Tests;

/// <summary>
/// Contains unit tests for car rental system.
/// Uses test data provided by <see cref="DataSeeder"/>.
/// </summary>
public class CarRentalTests(DataSeeder seed) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Returns all distinct clients who rented cars of a selected model,
    /// sorted alphabetically by full name
    /// </summary>
    [Fact]
    public void GetClientsByModelName_ShouldReturnDistinctClientsOrderedByFullName()
    {
        var firstModelId = seed.ModelGenerations.First().ModelId;
        var modelName = seed.Models.First(m => m.Id == firstModelId).NameModel;

        var clients = seed.Rentals
            .Join(seed.Cars,
                rental => rental.CarId,
                car => car.Id,
                (rental, car) => new { rental, car })
            .Join(seed.ModelGenerations,
                rc => rc.car.ModelGenerationId,
                mg => mg.Id,
                (rc, mg) => new { rc.rental, mg })
            .Join(seed.Clients,
                rmg => rmg.rental.ClientId,
                client => client.Id,
                (rmg, client) => new { rmg.mg, client })
            .Where(x => x.mg.ModelId == firstModelId)
            .Select(x => x.client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Equal(clients.Count, clients.Select(c => c.Id).Distinct().Count());
        Assert.True(clients.SequenceEqual(clients.OrderBy(c => c.FullName)));
    }

    /// <summary>
    /// Returns cars currently in active rentals
    /// and checks alphabetical order by license plate
    /// </summary>
    [Fact]
    public void GetCarsCurrentlyRented_ShouldReturnCarsThatAreActiveNow()
    {
        var now = DateTime.Now;

        var carsInRent = seed.Rentals
            .Where(r => r.RentalStartTime <= now &&
                        r.RentalStartTime.AddHours(r.RentalDurationHours) > now)
            .Join(seed.Cars,
                rental => rental.CarId,
                car => car.Id,
                (rental, car) => car)
            .Distinct()
            .OrderBy(c => c.LicensePlate)
            .ToList();

        Assert.True(carsInRent.SequenceEqual(carsInRent.OrderBy(c => c.LicensePlate)));
    }

    /// <summary>
    /// Returns top 5 most rented cars,
    /// ordered by rental count in descending order
    /// </summary>
    [Fact]
    public void GetTop5MostRentedCars_ShouldReturnCarsOrderedByRentalCountDesc()
    {
        var expectedCarId = seed.Rentals
            .GroupBy(r => r.CarId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .First();

        var top5 = seed.Rentals
            .GroupBy(r => r.CarId)
            .Select(g => new
            {
                CarId = g.Key,
                Count = g.Count()
            })
            .Join(seed.Cars,
                anon => anon.CarId,
                car => car.Id,
                (anon, car) => new { Car = car, anon.Count })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        Assert.Contains(top5, x => x.Car.Id == expectedCarId);
        Assert.True(top5.SequenceEqual(
            top5.OrderByDescending(x => x.Count)));
    }


    /// <summary>
    /// Returns number of rentals per each car
    /// and checks alphabetical order by license plate
    /// </summary>
    [Fact]
    public void GetRentalCountPerCar_ShouldReturnCountForEveryCar()
    {
        var perCar = seed.Cars
            .Select(c => new
            {
                Car = c,
                Count = seed.Rentals.Count(r => r.CarId == c.Id)
            })
            .OrderBy(x => x.Car.LicensePlate)
            .ToList();

        Assert.Equal(seed.Cars.Count, perCar.Count);
        Assert.True(perCar.SequenceEqual(perCar.OrderBy(x => x.Car.LicensePlate)));
    }

    /// <summary>
    /// Returns top 5 clients with the highest total rental sum,
    /// sorted by amount descending and name ascending
    /// </summary>
    [Fact]
    public void GetTop5ClientsByTotalAmount_ShouldReturnClientsOrderedBySumDesc()
    {
        var top5 = seed.Rentals
            .Join(seed.Cars,
                r => r.CarId,
                c => c.Id,
                (r, c) => new { r, c })
            .Join(seed.ModelGenerations,
                rc => rc.c.ModelGenerationId,
                mg => mg.Id,
                (rc, mg) => new { rc.r, mg })
            .GroupBy(x => x.r.ClientId)
            .Select(g =>
            {
                var clientId = g.Key;
                var client = seed.Clients.First(cl => cl.Id == clientId);
                var sum = g.Sum(x =>
                    (decimal)x.r.RentalDurationHours * x.mg.RentalPricePerHour);

                return new { Client = client, Sum = sum };
            })
            .OrderByDescending(x => x.Sum)
            .ThenBy(x => x.Client.FullName)
            .Take(5)
            .ToList();

        Assert.True(top5.SequenceEqual(top5.OrderByDescending(x => x.Sum).ThenBy(x => x.Client.FullName)));
    }
}
