using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Data;

/// <summary>
/// Provides sample data for the car rental system.
/// Initializes Model, ModelGeneration, Car, Client, Rental
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Car models used in the system
    /// </summary>
    public List<Model> Models { get; private set; }

    /// <summary>
    /// Model generations with specs
    /// </summary>
    public List<ModelGeneration> ModelGenerations { get; private set; }

    /// <summary>
    /// Fleet of rental cars
    /// </summary>
    public List<Car> Cars { get; private set; }

    /// <summary>
    /// Registered rental clients
    /// </summary>
    public List<Client> Clients { get; private set; }

    /// <summary>
    /// Rental agreements data
    /// </summary>
    public List<Rental> Rentals { get; private set; }

    /// <summary>
    /// Builds all seed collections in memory.
    /// Order matters to satisfy references
    /// </summary>
    public DataSeeder()
    {
        Models = InitModels();
        ModelGenerations = InitModelGenerations(Models);
        Cars = InitCars(ModelGenerations);
        Clients = InitClients();
        Rentals = InitRentals(Clients, Cars);
    }

    /// <summary>
    /// Initializes 10 car models
    /// </summary>
    private static List<Model> InitModels() => 
    [
        new Model { NameModel = "Toyota Camry", TypeDrive = TypeDrive.FWD, SeatNumber = 5, BodyCarType = BodyCarType.Sedan, ClassCar = ClassCar.D },
        new Model { NameModel = "BMW 3 Series", TypeDrive = TypeDrive.RWD, SeatNumber = 5, BodyCarType = BodyCarType.Sedan, ClassCar = ClassCar.D },
        new Model { NameModel = "Audi Q5", TypeDrive = TypeDrive.AWD, SeatNumber = 5, BodyCarType = BodyCarType.Suv, ClassCar = ClassCar.E },
        new Model { NameModel = "Volkswagen Golf", TypeDrive = TypeDrive.FWD, SeatNumber = 5, BodyCarType = BodyCarType.Hatchback, ClassCar = ClassCar.C },
        new Model { NameModel = "Mercedes C-Class", TypeDrive = TypeDrive.RWD, SeatNumber = 5, BodyCarType = BodyCarType.Sedan, ClassCar = ClassCar.E },
        new Model { NameModel = "Hyundai Tucson", TypeDrive = TypeDrive.AWD, SeatNumber = 5, BodyCarType = BodyCarType.Suv, ClassCar = ClassCar.C },
        new Model { NameModel = "Kia Rio", TypeDrive = TypeDrive.FWD, SeatNumber = 5, BodyCarType = BodyCarType.Sedan, ClassCar = ClassCar.B },
        new Model { NameModel = "Ford Focus", TypeDrive = TypeDrive.FWD, SeatNumber = 5, BodyCarType = BodyCarType.Hatchback, ClassCar = ClassCar.C },
        new Model { NameModel = "Nissan X-Trail", TypeDrive = TypeDrive.AWD, SeatNumber = 5, BodyCarType = BodyCarType.Suv, ClassCar = ClassCar.D },
        new Model { NameModel = "Lada Vesta", TypeDrive = TypeDrive.FWD, SeatNumber = 5, BodyCarType = BodyCarType.Sedan, ClassCar = ClassCar.C }
    ];

    /// <summary>
    /// Initializes 10 model generations linked to Models
    /// </summary>
    private static List<ModelGeneration> InitModelGenerations(List<Model> models) =>
        [
        new ModelGeneration { ModelId = models[0].Id, YearManufacture = 2020, EngineCapacity = 2.5, GearBoxType = GearBoxType.Automatic, RentalPricePerHour = 10.0M },
        new ModelGeneration { ModelId = models[1].Id, YearManufacture = 2022, EngineCapacity = 2.0, GearBoxType = GearBoxType.Automatic, RentalPricePerHour = 13.0M },
        new ModelGeneration { ModelId = models[2].Id, YearManufacture = 2021, EngineCapacity = 2.0, GearBoxType = GearBoxType.DCT, RentalPricePerHour = 15.0M },
        new ModelGeneration { ModelId = models[3].Id, YearManufacture = 2019, EngineCapacity = 1.4, GearBoxType = GearBoxType.Manual, RentalPricePerHour = 7.0M },
        new ModelGeneration { ModelId = models[4].Id, YearManufacture = 2023, EngineCapacity = 2.0, GearBoxType = GearBoxType.Automatic, RentalPricePerHour = 16.5M },
        new ModelGeneration { ModelId = models[5].Id, YearManufacture = 2022, EngineCapacity = 2.0, GearBoxType = GearBoxType.CVT, RentalPricePerHour = 12.0M },
        new ModelGeneration { ModelId = models[6].Id, YearManufacture = 2020, EngineCapacity = 1.6, GearBoxType = GearBoxType.Automatic, RentalPricePerHour = 6.5M },
        new ModelGeneration { ModelId = models[7].Id, YearManufacture = 2021, EngineCapacity = 1.5, GearBoxType = GearBoxType.Manual, RentalPricePerHour = 8.0M },
        new ModelGeneration { ModelId = models[8].Id, YearManufacture = 2022, EngineCapacity = 2.5, GearBoxType = GearBoxType.Automatic, RentalPricePerHour = 14.0M },
        new ModelGeneration { ModelId = models[9].Id, YearManufacture = 2021, EngineCapacity = 1.6, GearBoxType = GearBoxType.Manual, RentalPricePerHour = 5.5M }
    ];

    /// <summary>
    /// Initializes 10 cars linked to ModelGenerations
    /// </summary>
    private static List<Car> InitCars(List<ModelGeneration> gens) =>
    [
        new Car { ModelGenerationId = gens[0].Id, LicensePlate = "A123BC163", Color = "White" },
        new Car { ModelGenerationId = gens[1].Id, LicensePlate = "B456AK163", Color = "Black" },
        new Car { ModelGenerationId = gens[2].Id, LicensePlate = "E789MH163", Color = "Gray" },
        new Car { ModelGenerationId = gens[3].Id, LicensePlate = "K234OP163", Color = "Red" },
        new Car { ModelGenerationId = gens[4].Id, LicensePlate = "M567TX163", Color = "Silver" },
        new Car { ModelGenerationId = gens[5].Id, LicensePlate = "P890CH163", Color = "Blue" },
        new Car { ModelGenerationId = gens[6].Id, LicensePlate = "C345EA163", Color = "White" },
        new Car { ModelGenerationId = gens[7].Id, LicensePlate = "T678HX163", Color = "Black" },
        new Car { ModelGenerationId = gens[8].Id, LicensePlate = "O901KP163", Color = "Gray" },
        new Car { ModelGenerationId = gens[9].Id, LicensePlate = "H112ME163", Color = "Red" }
    ];

    /// <summary>
    /// Initializes 10 clients
    /// </summary>
    private static List<Client> InitClients() =>
    [
        new Client { DriverLicenseNumber = "6314123456", FullName = "Roman Ivanov", BirthDate = new DateOnly(2003, 4, 21) },
        new Client { DriverLicenseNumber = "7707654321", FullName = "Marina Smirnova", BirthDate = new DateOnly(2004, 8, 15) },
        new Client { DriverLicenseNumber = "7809234567", FullName = "Alex Petrov", BirthDate = new DateOnly(1998, 3, 3) },
        new Client { DriverLicenseNumber = "5011345678", FullName = "Sergey Karpov", BirthDate = new DateOnly(1990, 1, 5) },
        new Client { DriverLicenseNumber = "1610987654", FullName = "Olga Kuznetsova", BirthDate = new DateOnly(1988, 11, 28) },
        new Client { DriverLicenseNumber = "6412112233", FullName = "Nikita Sidorov", BirthDate = new DateOnly(1999, 9, 12) },
        new Client { DriverLicenseNumber = "5413445566", FullName = "Pavel Orlov", BirthDate = new DateOnly(1985, 5, 2) },
        new Client { DriverLicenseNumber = "3608778899", FullName = "Anna Volkova", BirthDate = new DateOnly(2001, 6, 22) },
        new Client { DriverLicenseNumber = "7704556677", FullName = "Ivan Belov", BirthDate = new DateOnly(1994, 7, 9) },
        new Client { DriverLicenseNumber = "7815990011", FullName = "Elena Sergeeva", BirthDate = new DateOnly(1992, 12, 31) }
    ];

    /// <summary>
    /// Initializes 10 rentals linking Clients and Cars
    /// </summary>
    private static List<Rental> InitRentals(List<Client> clients, List<Car> cars) =>
    [
        new Rental { ClientId = clients[0].Id, CarId = cars[0].Id, RentalStartTime = DateTime.Now.AddHours(-10), RentalDurationHours = 4 },
        new Rental { ClientId = clients[1].Id, CarId = cars[1].Id, RentalStartTime = DateTime.Now.AddDays(-1),  RentalDurationHours = 3 },
        new Rental { ClientId = clients[2].Id, CarId = cars[2].Id, RentalStartTime = DateTime.Now.AddDays(-2),  RentalDurationHours = 6 },
        new Rental { ClientId = clients[3].Id, CarId = cars[3].Id, RentalStartTime = DateTime.Now.AddDays(-3),  RentalDurationHours = 2 },
        new Rental { ClientId = clients[4].Id, CarId = cars[4].Id, RentalStartTime = DateTime.Now.AddDays(-4),  RentalDurationHours = 8 },
        new Rental { ClientId = clients[5].Id, CarId = cars[5].Id, RentalStartTime = DateTime.Now.AddDays(-5),  RentalDurationHours = 5 },
        new Rental { ClientId = clients[6].Id, CarId = cars[6].Id, RentalStartTime = DateTime.Now.AddDays(-6),  RentalDurationHours = 3 },
        new Rental { ClientId = clients[7].Id, CarId = cars[7].Id, RentalStartTime = DateTime.Now.AddDays(-7),  RentalDurationHours = 2 },
        new Rental { ClientId = clients[8].Id, CarId = cars[8].Id, RentalStartTime = DateTime.Now.AddDays(-8),  RentalDurationHours = 4 },
        new Rental { ClientId = clients[9].Id, CarId = cars[9].Id, RentalStartTime = DateTime.Now.AddDays(-9),  RentalDurationHours = 7 }
    ];
} 
