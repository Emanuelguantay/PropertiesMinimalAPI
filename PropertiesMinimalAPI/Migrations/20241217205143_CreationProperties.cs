using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertiesMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8662), "Descripción test 1", true, "Test1", "Casa las palmas 1" },
                    { 2, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8666), "Descripción test 1", true, "Test2", "Casa las palmas 2" },
                    { 3, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8668), "Descripción test 1", true, "Test3", "Casa las palmas 3" },
                    { 4, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8669), "Descripción test 1", true, "Test4", "Casa las palmas 4" },
                    { 5, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8670), "Descripción test 1", true, "Test5", "Casa las palmas 5" },
                    { 6, new DateTime(2024, 12, 17, 20, 51, 43, 417, DateTimeKind.Utc).AddTicks(8671), "Descripción test 1", true, "Test6", "Casa las palmas 6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
