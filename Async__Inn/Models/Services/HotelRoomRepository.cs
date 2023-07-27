using Async__Inn.Data;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }



        public async Task<HotelRoom> AddHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRoom.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }





        public async Task DeleteHotelRoom(int roomId, int hotelId)
        {

            HotelRoom hotel = await GetHotelRoom(hotelId, roomId);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();



        }




        public async Task<List<HotelRoom>> GetAllHotelRooms(int hotelId)
        {
            var hotels = await _context.HotelRoom.ToListAsync();
            return hotels;
        }




        public async Task<HotelRoom> GetHotelRoom(int roomId, int hotelId)
        {
            var hotelRoom = await _context.HotelRoom
                        .FirstOrDefaultAsync(hr => hr.RoomID == roomId && hr.HotelID == hotelId);

            return hotelRoom;
        }




        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}
