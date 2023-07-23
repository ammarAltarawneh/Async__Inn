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

        public async Task<Room> Create(Room room)
        {
            _context.Rooms.Add(room);

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
            var rooms= await _context.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoom(int RoomID)
        {
            Room room = await _context.Rooms.FindAsync(RoomID);
            return room;
        }
        
        public async Task<Room> UpdateRoom(int ID, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        
    }
}
