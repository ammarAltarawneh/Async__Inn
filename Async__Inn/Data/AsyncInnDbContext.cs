using Async__Inn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Data
{
    public class AsyncInnDbContext: IdentityDbContext<ApplicationUser>
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options) 
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { ID = 1, Name="SahNoom", StreetAdress="st60", City="Irbid", Country="Jordan", Phone="0775555555", State="Alhimah"},
                new Hotel() { ID = 2, Name = "Castle", StreetAdress = "st30", City = "Irbid", Country = "Jordan", Phone = "0781111111", State = "Hakama" },
                new Hotel() { ID = 3, Name = "Diamond", StreetAdress = "st100", City = "Irbid", Country = "Jordan", Phone = "0792222222", State = "Howara" }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room() { ID = 1, Name = "Bedroom", Layout=20 },
                new Room() { ID = 2, Name = "livingroom", Layout = 30 },
                new Room() { ID = 3, Name = "Kitchen", Layout = 10 }
                );
            modelBuilder.Entity<Amenity>().HasData(
                new Room() { ID = 1, Name = "fitness center" },
                new Room() { ID = 2, Name = "swiming pool" },
                new Room() { ID = 3, Name = "buisiness center" }
                );

            modelBuilder.Entity<HotelRoom>().HasKey(
                hotelRoom => new { hotelRoom.HotelID, hotelRoom.RoomID, hotelRoom.RoomNumber, hotelRoom.Rate, hotelRoom.PetFreindly }
                );
            modelBuilder.Entity<RoomAmenities>().HasKey(
                roomAmenities => new { roomAmenities.AmenitiesID, roomAmenities.RoomID }
                );

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
    }
}
