var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-carRental")
    .AddDatabase("CarRentalDb");

builder.AddProject<Projects.CarRental_Api_Host>("carrental-api-host")
    .WithReference(db, "DatabaseConnection")
    .WaitFor(db);

builder.Build().Run();
