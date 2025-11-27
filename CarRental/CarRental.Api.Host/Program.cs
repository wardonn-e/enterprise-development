using CarRental.Application;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Cars;
using CarRental.Application.Contracts.Clients;
using CarRental.Application.Contracts.ModelGenerations;
using CarRental.Application.Contracts.Models;
using CarRental.Application.Contracts.Rentals;
using CarRental.Application.Services;
using CarRental.Domain;
using CarRental.Domain.Data;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.EfCore;
using CarRental.Infrastructure.EfCore.Repositories;
using CarRental.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new CarRentalProfile());
});

builder.Services.AddSingleton<DataSeeder>();

builder.Services.AddTransient<IRepository<Car, Guid>, CarRepository>();
builder.Services.AddTransient<IRepository<Client, Guid>, ClientRepository>();
builder.Services.AddTransient<IRepository<Model, Guid>, ModelRepository>();
builder.Services.AddTransient<IRepository<ModelGeneration, Guid>, ModelGenerationRepository>();
builder.Services.AddTransient<IRepository<Rental, Guid>, RentalRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IModelGenerationService, ModelGenerationService>();
builder.Services.AddScoped<IApplicationService<ModelDto, ModelCreateUpdateDto, Guid>, ModelService>();
builder.Services.AddScoped<IApplicationService<RentalDto, RentalCreateUpdateDto, Guid>, RentalService>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("CarRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddMySqlDbContext<CarRentalDbContext>(connectionName: "DatabaseConnection");

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();
db.Database.Migrate();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
