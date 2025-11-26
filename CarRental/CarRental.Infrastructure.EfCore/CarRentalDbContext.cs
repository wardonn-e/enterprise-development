using CarRental.Domain.Data;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore;

/// <summary>
/// DbContext for the Car Rental system configuring all entity relationships and constraints
/// </summary>
public class CarRentalDbContext(DbContextOptions options, DataSeeder seeder) : DbContext(options)
{
    /// <summary>
    /// DbSet for Car entities
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// DbSet for Client entities
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// DbSet for Model entities
    /// </summary>
    public DbSet<Model> Models { get; set; }

    /// <summary>
    /// DbSet for ModelGeneration entities
    /// </summary>
    public DbSet<ModelGeneration> ModelGenerations { get; set; }

    /// <summary>
    /// DbSet for Rental entities
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    /// <summary>
    /// Configures the model schema, constraints, relationships and data types
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.NameModel)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(m => m.TypeDrive).IsRequired().HasConversion<int>();
            entity.Property(m => m.BodyCarType).IsRequired().HasConversion<int>();
            entity.Property(m => m.ClassCar).IsRequired().HasConversion<int>();

            entity.Property(m => m.SeatNumber).IsRequired();

            entity.HasData(seeder.Models);
        });

        modelBuilder.Entity<ModelGeneration>(entity =>
        {
            entity.HasKey(mg => mg.Id);

            entity.HasOne(mg => mg.Model)
                .WithMany()
                .HasForeignKey(mg => mg.ModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(mg => mg.RentalPricePerHour)
                .IsRequired()
                .HasColumnType("decimal(12, 2)");

            entity.Property(mg => mg.GearBoxType).IsRequired().HasConversion<int>();

            entity.HasData(seeder.ModelGenerations);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.HasOne(c => c.ModelGeneration)
                .WithMany()
                .HasForeignKey(c => c.ModelGenerationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(c => c.Color)
                .HasMaxLength(50)
                .IsRequired(false);

            entity.HasData(seeder.Cars);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(cl => cl.Id);

            entity.Property(cl => cl.DriverLicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(cl => cl.FullName)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(cl => cl.BirthDate)
                .HasColumnType("date")
                .IsRequired(false);

            entity.HasData(seeder.Clients);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.Ignore(r => r.TotalRentalAmount);

            entity.HasData(seeder.Rentals);
        });
    }
}