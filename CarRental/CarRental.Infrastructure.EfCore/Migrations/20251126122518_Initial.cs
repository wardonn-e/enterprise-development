using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "clients",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                driver_license_number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                full_name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                birth_date = table.Column<DateOnly>(type: "date", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_clients", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "models",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                type_drive = table.Column<int>(type: "int", nullable: false),
                seat_number = table.Column<int>(type: "int", nullable: false),
                body_car_type = table.Column<int>(type: "int", nullable: false),
                class_car = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_models", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "model_generations",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                model_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                year_manufacture = table.Column<int>(type: "int", nullable: false),
                engine_capacity = table.Column<double>(type: "double", nullable: false),
                rental_price_per_hour = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                gear_box_type = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_model_generations", x => x.id);
                table.ForeignKey(
                    name: "FK_model_generations_models_model_id",
                    column: x => x.model_id,
                    principalTable: "models",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "cars",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                model_generation_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                license_plate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_cars", x => x.id);
                table.ForeignKey(
                    name: "FK_cars_model_generations_model_generation_id",
                    column: x => x.model_generation_id,
                    principalTable: "model_generations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "rentals",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                client_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                car_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                rental_start_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                rental_duration_hours = table.Column<double>(type: "double", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_rentals", x => x.id);
                table.ForeignKey(
                    name: "FK_rentals_cars_car_id",
                    column: x => x.car_id,
                    principalTable: "cars",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_rentals_clients_client_id",
                    column: x => x.client_id,
                    principalTable: "clients",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "clients",
            columns: new[] { "id", "birth_date", "driver_license_number", "full_name" },
            values: new object[,]
            {
                { new Guid("00254aaa-193e-4eab-90f0-9d830866ce95"), new DateOnly(1990, 1, 5), "5011345678", "Sergey Karpov" },
                { new Guid("030774ea-647c-4039-8e68-21536e83a93d"), new DateOnly(1994, 7, 9), "7704556677", "Ivan Belov" },
                { new Guid("2c707871-8d93-4fc2-9bd0-a0691abe2419"), new DateOnly(1988, 11, 28), "1610987654", "Olga Kuznetsova" },
                { new Guid("38bef19b-0bdc-478c-b15c-81ccfc63d0a1"), new DateOnly(1985, 5, 2), "5413445566", "Pavel Orlov" },
                { new Guid("42927043-7438-4a6a-8706-a5fd251550ef"), new DateOnly(2004, 8, 15), "7707654321", "Marina Smirnova" },
                { new Guid("44c818d9-546b-45ed-ba31-bc8f11bc4555"), new DateOnly(2003, 4, 21), "6314123456", "Roman Ivanov" },
                { new Guid("4ef2250c-0d70-4c39-8b17-102773c1b153"), new DateOnly(1992, 12, 31), "7815990011", "Elena Sergeeva" },
                { new Guid("525c380f-ba84-4571-80ef-9aab2049f75c"), new DateOnly(1999, 9, 12), "6412112233", "Nikita Sidorov" },
                { new Guid("8402de35-2ea9-4f95-af14-eb99c2a14992"), new DateOnly(1998, 3, 3), "7809234567", "Alex Petrov" },
                { new Guid("d88a9c6a-2424-4865-b43b-fab9eb948c8f"), new DateOnly(2001, 6, 22), "3608778899", "Anna Volkova" }
            });

        migrationBuilder.InsertData(
            table: "models",
            columns: new[] { "id", "body_car_type", "class_car", "name", "seat_number", "type_drive" },
            values: new object[,]
            {
                { new Guid("27663e74-ffcb-4bb6-8615-6c728b223e1a"), 3, 2, "Hyundai Tucson", 5, 2 },
                { new Guid("453a6ecb-8a97-4e38-a025-d4eb1512a914"), 3, 4, "Audi Q5", 5, 2 },
                { new Guid("6e047d30-3427-49ba-b8fa-620b507fcf34"), 0, 3, "BMW 3 Series", 5, 1 },
                { new Guid("74190ed9-6fba-4225-9ba9-3c2037fbc5c0"), 3, 3, "Nissan X-Trail", 5, 2 },
                { new Guid("9a01902a-c45f-4899-a616-c7e859cae37f"), 0, 4, "Mercedes C-Class", 5, 1 },
                { new Guid("9f5c3f00-1fa3-444d-bdd3-b6aff14a6615"), 1, 2, "Ford Focus", 5, 0 },
                { new Guid("ba35b213-664d-48b4-8941-c8605bd597b5"), 0, 3, "Toyota Camry", 5, 0 },
                { new Guid("be2590ff-9565-4388-80b8-ce542f7a5c26"), 1, 2, "Volkswagen Golf", 5, 0 },
                { new Guid("c4c83b9d-9f1d-47bf-95e1-c762682b139a"), 0, 2, "Lada Vesta", 5, 0 },
                { new Guid("e4843b94-5913-4fe6-9067-908576262d28"), 0, 1, "Kia Rio", 5, 0 }
            });

        migrationBuilder.InsertData(
            table: "model_generations",
            columns: new[] { "id", "engine_capacity", "gear_box_type", "model_id", "rental_price_per_hour", "year_manufacture" },
            values: new object[,]
            {
                { new Guid("0c746704-f33f-404f-95b1-38f97182945d"), 2.5, 1, new Guid("ba35b213-664d-48b4-8941-c8605bd597b5"), 10.0m, 2020 },
                { new Guid("16148b60-652c-4a4f-bea5-08b04036db05"), 2.5, 1, new Guid("74190ed9-6fba-4225-9ba9-3c2037fbc5c0"), 14.0m, 2022 },
                { new Guid("5555399c-0d61-4282-a9a8-8ec62c779c0a"), 1.6000000000000001, 1, new Guid("e4843b94-5913-4fe6-9067-908576262d28"), 6.5m, 2020 },
                { new Guid("6a16941c-23c1-4855-834c-213c9d0c43e0"), 1.3999999999999999, 0, new Guid("be2590ff-9565-4388-80b8-ce542f7a5c26"), 7.0m, 2019 },
                { new Guid("6a2500b4-5c0d-475e-9091-57f1f5eebd5e"), 2.0, 2, new Guid("27663e74-ffcb-4bb6-8615-6c728b223e1a"), 12.0m, 2022 },
                { new Guid("6bbc94b7-ce3e-403c-acc2-7673fcb3b542"), 2.0, 1, new Guid("6e047d30-3427-49ba-b8fa-620b507fcf34"), 13.0m, 2022 },
                { new Guid("8d40cbfb-f7d1-4c37-a9a3-2a83cfe4b68e"), 2.0, 1, new Guid("9a01902a-c45f-4899-a616-c7e859cae37f"), 16.5m, 2023 },
                { new Guid("e71fad6a-7130-4ec8-9c54-4cd9377a9f3e"), 1.5, 0, new Guid("9f5c3f00-1fa3-444d-bdd3-b6aff14a6615"), 8.0m, 2021 },
                { new Guid("eca5d10e-5e1d-42cc-9ecf-51660ad4eab8"), 2.0, 3, new Guid("453a6ecb-8a97-4e38-a025-d4eb1512a914"), 15.0m, 2021 },
                { new Guid("f3e00c4c-9688-4aa4-860e-4f82c922c001"), 1.6000000000000001, 0, new Guid("c4c83b9d-9f1d-47bf-95e1-c762682b139a"), 5.5m, 2021 }
            });

        migrationBuilder.InsertData(
            table: "cars",
            columns: new[] { "id", "color", "license_plate", "model_generation_id" },
            values: new object[,]
            {
                { new Guid("07a51db0-5a9c-4d22-ac23-0d38d5058382"), "White", "C345EA163", new Guid("5555399c-0d61-4282-a9a8-8ec62c779c0a") },
                { new Guid("14072ffe-8228-458d-85a6-d0dacdac3ba2"), "White", "A123BC163", new Guid("0c746704-f33f-404f-95b1-38f97182945d") },
                { new Guid("28f97dc8-ac46-440f-914a-4e998dcc7fed"), "Silver", "M567TX163", new Guid("8d40cbfb-f7d1-4c37-a9a3-2a83cfe4b68e") },
                { new Guid("63e52545-fffe-4bc4-bc23-6b00f29b255b"), "Black", "T678HX163", new Guid("e71fad6a-7130-4ec8-9c54-4cd9377a9f3e") },
                { new Guid("68dea793-7fd1-4080-92f4-6099bb83f515"), "Black", "B456AK163", new Guid("6bbc94b7-ce3e-403c-acc2-7673fcb3b542") },
                { new Guid("84d93500-f09b-4e99-8568-cb95b49c5220"), "Gray", "E789MH163", new Guid("eca5d10e-5e1d-42cc-9ecf-51660ad4eab8") },
                { new Guid("ba69ea51-36be-44d5-9140-bba02b84270e"), "Gray", "O901KP163", new Guid("16148b60-652c-4a4f-bea5-08b04036db05") },
                { new Guid("c7723367-898d-4497-b0bd-a52e0c98cf7c"), "Red", "K234OP163", new Guid("6a16941c-23c1-4855-834c-213c9d0c43e0") },
                { new Guid("ca24e7de-2c58-4fb0-bd82-2d8954305329"), "Blue", "P890CH163", new Guid("6a2500b4-5c0d-475e-9091-57f1f5eebd5e") },
                { new Guid("e8432c42-312b-438b-b446-319670e7a5a8"), "Red", "H112ME163", new Guid("f3e00c4c-9688-4aa4-860e-4f82c922c001") }
            });

        migrationBuilder.InsertData(
            table: "rentals",
            columns: new[] { "id", "car_id", "client_id", "rental_duration_hours", "rental_start_time" },
            values: new object[,]
            {
                { new Guid("13bdb3bb-9483-405a-9658-e1276378a0b8"), new Guid("84d93500-f09b-4e99-8568-cb95b49c5220"), new Guid("8402de35-2ea9-4f95-af14-eb99c2a14992"), 6.0, new DateTime(2025, 11, 24, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4921) },
                { new Guid("1cdaafad-08af-424d-9697-1ba21a45e2b5"), new Guid("c7723367-898d-4497-b0bd-a52e0c98cf7c"), new Guid("00254aaa-193e-4eab-90f0-9d830866ce95"), 2.0, new DateTime(2025, 11, 23, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4922) },
                { new Guid("1d4bda3f-bc8a-405d-8e04-c65fa5bb41d8"), new Guid("14072ffe-8228-458d-85a6-d0dacdac3ba2"), new Guid("44c818d9-546b-45ed-ba31-bc8f11bc4555"), 4.0, new DateTime(2025, 11, 26, 6, 25, 18, 125, DateTimeKind.Local).AddTicks(4656) },
                { new Guid("311464f2-da86-40ea-8bb4-eee6079aec03"), new Guid("28f97dc8-ac46-440f-914a-4e998dcc7fed"), new Guid("2c707871-8d93-4fc2-9bd0-a0691abe2419"), 8.0, new DateTime(2025, 11, 22, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4924) },
                { new Guid("6ed229cd-e258-4d2a-8b9a-f2eeaf5d6f19"), new Guid("63e52545-fffe-4bc4-bc23-6b00f29b255b"), new Guid("d88a9c6a-2424-4865-b43b-fab9eb948c8f"), 2.0, new DateTime(2025, 11, 19, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4931) },
                { new Guid("7cf99400-ca41-4b1a-a2cc-a8e8ab0d0248"), new Guid("ba69ea51-36be-44d5-9140-bba02b84270e"), new Guid("030774ea-647c-4039-8e68-21536e83a93d"), 4.0, new DateTime(2025, 11, 18, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4933) },
                { new Guid("d5dae61f-e874-4fe4-a9c1-08556ead27a4"), new Guid("ca24e7de-2c58-4fb0-bd82-2d8954305329"), new Guid("525c380f-ba84-4571-80ef-9aab2049f75c"), 5.0, new DateTime(2025, 11, 21, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4925) },
                { new Guid("e6519f4a-7599-4ce8-bac9-c31c55244fbd"), new Guid("07a51db0-5a9c-4d22-ac23-0d38d5058382"), new Guid("38bef19b-0bdc-478c-b15c-81ccfc63d0a1"), 3.0, new DateTime(2025, 11, 20, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4926) },
                { new Guid("fe0145db-50f8-4a32-82ff-ea91859aa71b"), new Guid("e8432c42-312b-438b-b446-319670e7a5a8"), new Guid("4ef2250c-0d70-4c39-8b17-102773c1b153"), 7.0, new DateTime(2025, 11, 17, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4934) },
                { new Guid("fe1fc6bc-7c3f-4a84-9484-39c0a8d30c87"), new Guid("68dea793-7fd1-4080-92f4-6099bb83f515"), new Guid("42927043-7438-4a6a-8706-a5fd251550ef"), 3.0, new DateTime(2025, 11, 25, 16, 25, 18, 125, DateTimeKind.Local).AddTicks(4910) }
            });

        migrationBuilder.CreateIndex(
            name: "IX_cars_model_generation_id",
            table: "cars",
            column: "model_generation_id");

        migrationBuilder.CreateIndex(
            name: "IX_model_generations_model_id",
            table: "model_generations",
            column: "model_id");

        migrationBuilder.CreateIndex(
            name: "IX_rentals_car_id",
            table: "rentals",
            column: "car_id");

        migrationBuilder.CreateIndex(
            name: "IX_rentals_client_id",
            table: "rentals",
            column: "client_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "rentals");

        migrationBuilder.DropTable(
            name: "cars");

        migrationBuilder.DropTable(
            name: "clients");

        migrationBuilder.DropTable(
            name: "model_generations");

        migrationBuilder.DropTable(
            name: "models");
    }
}
