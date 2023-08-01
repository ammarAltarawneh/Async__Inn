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
         


        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            var room = await _context.Rooms.FindAsync(hotelRoom.RoomID);
            var hotel = await _context.Hotels.FindAsync(hotelRoom.HotelID);

            hotelRoom.HotelID = hotelId;

            hotelRoom.Room = room;
            hotelRoom.Hotel = hotel;

            _context.HotelRoom.Add(hotelRoom);

            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task DeleteHotelRooms(int hotelId, int roomNumber)
        {
            var delete = await _context.HotelRoom
                .Where(r => r.HotelID == hotelId && r.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            if (delete != null)
            {
                _context.Entry<HotelRoom>(delete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRoom
                .Include(hotel => hotel.Hotel)
                .Include(room => room.Room)
                .ThenInclude(amenities => amenities.RoomAmenities)
                .ThenInclude(roomAmenities => roomAmenities.Amenity)
                .Where(x => x.HotelID == hotelId)
                .ToListAsync();

            var result = hotelRooms.Select(hr => new HotelRoom
            {
                HotelID = hr.HotelID,
                RoomID = hr.RoomID,
                RoomNumber = hr.RoomNumber,
                Rate = hr.Rate,
                PetFreindly = hr.PetFreindly,
                Room = new Room
                {
                    ID = hr.Room.ID,
                    Name = hr.Room.Name,
                    Layout = hr.Room.Layout,
                    RoomAmenities = hr.Room.RoomAmenities.Select(ra => new RoomAmenities
                    {
                        RoomID = ra.RoomID,
                        AmenitiesID = ra.AmenitiesID,
                        Amenity = new Amenity
                        {
                            ID = ra.Amenity.ID,
                            Name = ra.Amenity.Name,
                        }
                    }).ToList()
                },
                Hotel = new Hotel
                {
                    ID = hr.Hotel.ID,
                    Name = hr.Hotel.Name,
                    StreetAdress = hr.Hotel.StreetAdress,
                    City = hr.Hotel.City,
                    State = hr.Hotel.State,
                    Country = hr.Hotel.Country,
                    Phone = hr.Hotel.Phone
                }

            }).ToList();

            return result;
        }

        public async Task<List<HotelRoom>> GetHotelRoomsByName(string name)
        {
            var hotels = await _context.HotelRoom.Include(x => x.Hotel)
                .Where(hn => hn.Hotel.Name == name)
                .ToListAsync();



            return hotels;

        }

        public async Task<HotelRoom> GetHotelRoomsDetails(int hotelId, int roomNumber)
        {
            var hotelRoomDetails = await _context.HotelRoom
                .Include(hr => hr.Hotel)
                .Include(hr => hr.Room)
                .ThenInclude(roomAmenities => roomAmenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenity)
                .Where(hotel => hotel.HotelID == hotelId && hotel.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();


            return hotelRoomDetails;
        }

        public async Task<HotelRoom> UpdateHotelRooms(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            
             _context.Entry(hotelRoom).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}
