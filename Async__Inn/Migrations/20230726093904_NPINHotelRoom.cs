using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async__Inn.Migrations
{
    /// <inheritdoc />
    public partial class NPINHotelRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmenityID",
                table: "RoomAmenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenityID",
                table: "RoomAmenities",
                column: "AmenityID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_RoomID",
                table: "RoomAmenities",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_Hotels_HotelID",
                table: "HotelRoom",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_Rooms_RoomID",
                table: "HotelRoom",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities",
                column: "AmenityID",
                principalTable: "Amenities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomID",
                table: "RoomAmenities",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_Hotels_HotelID",
                table: "HotelRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_Rooms_RoomID",
                table: "HotelRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenityID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_RoomID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom");

            migrationBuilder.DropColumn(
                name: "AmenityID",
                table: "RoomAmenities");
        }
    }
}
