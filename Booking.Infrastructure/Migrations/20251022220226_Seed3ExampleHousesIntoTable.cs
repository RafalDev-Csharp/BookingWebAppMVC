using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed3ExampleHousesIntoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "SqMeters", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, "Description about Big House...", "https://placehold.co/600x400", "Big House", 4, 350.0, 600, null },
                    { 2, null, "Description about Uber House...", "https://placehold.co/600x401", "Uber House", 3, 250.0, 500, null },
                    { 3, null, "Description about Boss House...", "https://placehold.co/600x402", "Boss House", 6, 150.0, 750, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
