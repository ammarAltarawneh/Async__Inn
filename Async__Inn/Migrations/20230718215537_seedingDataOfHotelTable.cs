using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Async__Inn.Migrations
{
    /// <inheritdoc />
    public partial class seedingDataOfHotelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "ID", "City", "Country", "Name", "Phone", "State", "StreetAdress" },
                values: new object[,]
                {
                    { 1, "Irbid", "Jordan", "SahNoom", "0775555555", "Alhimah", "Al60" },
                    { 2, "Irbid", "Jordan", "Castle", "0781111111", "Hakama", "Al30" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
