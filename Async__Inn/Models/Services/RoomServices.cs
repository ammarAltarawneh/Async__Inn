using Async__Inn.Data;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(string name, int layout)
        {
            //_context.Rooms.Add(room);
            Room room = new Room { Layout= layout ,Name = name};
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int ID)
        {
            Room room = await GetRoom(ID);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms= await _context.Rooms
                .Include(x => x.HotelRoom)
                .Include(x => x.RoomAmenities)
                .ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoom(int RoomID)
        {
            Room room = await _context.Rooms.FindAsync(RoomID);
            return room;
        }
        
        public async Task<Room> UpdateRoom(int ID, string name, int layout)
        {
            //_context.Entry(room).State = EntityState.Modified;
            Room room = new Room {ID=ID, Layout = layout, Name = name };
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                RoomID = roomId,
                AmenitiesID = amenityId
            };

            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(r => r.AmenitiesID == amenityId && r.RoomID == roomId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
