using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Async__Inn.Models.Services
{
    /// <summary>
    /// Implementation of the IHotel interface to manage hotel-related operations.
    /// </summary>
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _context;

        /// <summary>
        /// Initializes a new instance of the HotelServices class.
        /// </summary>
        /// <param name="context">The database context for AsyncInn.</param>
        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">The Hotel object containing hotel information.</param>
        /// <returns>The created Hotel object.</returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            await _context.AddAsync(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Deletes an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        public async Task Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a list of all hotels.
        /// </summary>
        /// <returns>A list of HotelDTO objects representing all hotels.</returns>
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
                    Room = new RoomDTO
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

        /// <summary>
        /// Gets a specific hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        /// <returns>The HotelDTO object representing the retrieved hotel.</returns>
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
                    Room = new RoomDTO
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
            .FirstOrDefaultAsync(h => h.ID == id);

            return hotel;
        }

        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotelDTO">The updated HotelDTO object containing hotel information.</param>
        /// <returns>The updated HotelDTO object.</returns>
        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO)
        {
            Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.ID == hotelDTO.ID);
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
