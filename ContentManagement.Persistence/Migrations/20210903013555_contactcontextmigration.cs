using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContentManagement.Persistence.Migrations
{
    public partial class contactcontextmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    StreetInfo = table.Column<string>(nullable: false),
                    IsContactFilled = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbersTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    CountryCode = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbersTbl", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressTbl");

            migrationBuilder.DropTable(
                name: "PhoneNumbersTbl");
        }
    }
}
