using Async__Inn.Data;
using Async__Inn.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInn_Test
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:"); 
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();
        }


        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity() { Name= "Test"};
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, amenity.ID);

            return amenity;
        }

        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room() { Name = "Test", Layout = 1 };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, room.ID);

            return room;
        }




        public void Dispose()
        {
            if( _db != null )
            {
                _db.Dispose();
            }
            _db?.Dispose();
            _connection?.Dispose();

        }
    }
}
