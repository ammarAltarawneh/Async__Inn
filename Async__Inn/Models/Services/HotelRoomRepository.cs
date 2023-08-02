using Async__Inn.Data;
using Async__Inn.Models.DTO;
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

        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int HotelId)
        {
            var Hotel = await _context.Hotels.FindAsync(HotelId);
            //var Room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
            if (Hotel != null)
            {
                HotelRoom HR = new HotelRoom()
                {
                    HotelID = HotelId,
                    RoomID = hotelRoom.RoomID,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    PetFreindly = hotelRoom.PetFriendly

                };
                await _context.HotelRoom.AddAsync(HR);
                await _context.SaveChangesAsync();
                return hotelRoom;
            }
            return null;
            //_context.Entry(hotelRoom).State = EntityState.Added;
            //await _context.SaveChangesAsync();
            //return hotelRoom;	

        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            var hotelroom = await _context.HotelRoom
                .FirstOrDefaultAsync(HR => HR.HotelID == hotelId && HR.RoomNumber == roomNumber);
            //var hotelroom = await _context.HotelRooms.GetById(hotelId, roomNumber);
            if (hotelroom != null)
            {
                _context.HotelRoom.Remove(hotelroom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            var Room = await _context.HotelRoom.Select(HR => new HotelRoomDTO
            {
                HotelID = HR.HotelID,
                RoomNumber = HR.RoomNumber,
                Rate = HR.Rate,
                PetFriendly = HR.PetFreindly,
                RoomID = HR.RoomID,
                Room = new RoomDTO
                {
                    ID = HR.Room.ID,
                    Name = HR.Room.Name,
                    Layout = HR.Room.Layout,
                    Amenities = HR.Room.RoomAmenities.Select(RA => new AmenityDTO
                    {
                        ID = RA.Amenity.ID,
                        Name = RA.Amenity.Name
                    }).ToList()
                }
            })

        .FirstOrDefaultAsync(HR => HR.HotelID == hotelId && HR.RoomNumber == roomNumber);
            return Room;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            var Rooms = await _context.HotelRoom.Select(HR => new HotelRoomDTO
            {
                HotelID = HR.HotelID,
                RoomNumber = HR.RoomNumber,
                Rate = HR.Rate,
                PetFriendly = HR.PetFreindly,
                RoomID = HR.RoomID,
                Room = new RoomDTO
                {
                    ID = HR.Room.ID,
                    Name = HR.Room.Name,
                    Layout = HR.Room.Layout,
                    Amenities = HR.Room.RoomAmenities.Select(RA => new AmenityDTO
                    {
                        ID = RA.Amenity.ID,
                        Name = RA.Amenity.Name
                    }).ToList()
                }
            }).Where(HR => HR.HotelID == hotelId).ToListAsync();
            return Rooms;
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoom)
        {

            var HotelRoomToUpdate = await _context.HotelRoom.FirstAsync(HR => HR.HotelID == hotelId && HR.RoomNumber == roomNumber);

            if (HotelRoomToUpdate == null) { return null; }
            HotelRoomToUpdate.Rate = hotelRoom.Rate;
            HotelRoomToUpdate.PetFreindly = hotelRoom.PetFriendly;
            HotelRoomToUpdate.RoomID = hotelRoom.RoomID;
            _context.Entry(HotelRoomToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}
