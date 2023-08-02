using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Models.Services
{ 
    public class HotelServices : IHotel 
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        { 
            _context = context;
        }


        public async Task<Hotel> Create(Hotel hotel)
        {
            await _context.AddAsync(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.Select(hotel => new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAdress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotel.HotelRoom.Select(HR => new HotelRoomDTO
                {
                    HotelID = HR.HotelID,
                    RoomNumber = HR.RoomNumber,
                    Rate = HR.Rate,
                    PetFriendly = HR.PetFreindly,
                    RoomID = HR.RoomID,
                    Room = /*HR.room.Select(Room =>*/ new RoomDTO
                    {
                        ID = HR.Room.ID,
                        Name = HR.Room.Name,
                        Layout = HR.Room.Layout,
                        Amenities = HR.Room.RoomAmenities.Select(a => new AmenityDTO
                        {

                            ID = a.Amenity.ID,
                            Name = a.Amenity.Name,
                        }).ToList()
                    }
                }).ToList()
            })
                .ToListAsync();
            return hotels;
        }

        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Select(hotel => new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAdress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotel.HotelRoom.Select(HR => new HotelRoomDTO
                {
                    HotelID = HR.HotelID,
                    RoomNumber = HR.RoomNumber,
                    Rate = HR.Rate,
                    PetFriendly = HR.PetFreindly,
                    RoomID = HR.RoomID,
                    Room = /*HR.room.Select(Room =>*/ new RoomDTO
                    {
                        ID = HR.Room.ID,
                        Name = HR.Room.Name,
                        Layout = HR.Room.Layout,
                        Amenities = HR.Room.RoomAmenities.Select(a => new AmenityDTO
                        {

                            ID = a.Amenity.ID,
                            Name = a.Amenity.Name,
                        }).ToList()
                    }
                }).ToList()
            })
                .FirstOrDefaultAsync(H => H.ID == id);

            return hotel;

        }

        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO)
        {
            Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(H => H.ID == hotelDTO.ID);
            if (hotel == null)
            {
                return null;
            }
            hotel.Name = hotelDTO.Name;
            hotel.State = hotelDTO.State;
            hotel.StreetAdress = hotelDTO.StreetAddress;
            hotel.Phone = hotelDTO.Phone;
            hotel.City = hotelDTO.City;
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelDTO;
        }


    }
}
