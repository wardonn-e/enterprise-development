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
        var modelName = seed.Rentals.Select(r => r.Car.ModelGeneration.Model.NameModel).First();

        var clients = seed.Rentals
            .Where(r => r.Car.ModelGeneration.Model.NameModel == modelName)
            .Select(r => r.Client)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
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
            .Select(r => r.Car)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
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
            .GroupBy(r => r.Car.Id)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .First();

        var top5 = seed.Rentals
            .GroupBy(r => r.Car.Id)
            .Select(g => new { Car = g.First().Car, Count = g.Count() })
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
                Count = seed.Rentals.Count(r => r.Car.Id == c.Id)
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
            .GroupBy(r => r.Client.Id)
            .Select(g => new { client = g.First().Client, Sum = g.Sum(r => r.TotalRentalAmount) })
            .OrderByDescending(x => x.Sum)
            .ThenBy(x => x.client.FullName)
            .Take(5)
            .ToList();

        Assert.True(top5.SequenceEqual(top5.OrderByDescending(x => x.Sum).ThenBy(x => x.client.FullName)));
    }
}
