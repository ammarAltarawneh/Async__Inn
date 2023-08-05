using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Async__Inn.Models.Services
{
    /// <summary>
    /// Implementation of the IHotelRoom interface to manage hotel room-related operations.
    /// </summary>
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        /// <summary>
        /// Initializes a new instance of the HotelRoomRepository class.
        /// </summary>
        /// <param name="context">The database context for AsyncInn.</param>
        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new hotel room.
        /// </summary>
        /// <param name="hotelRoom">The HotelRoomDTO object containing hotel room information.</param>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <returns>The created HotelRoomDTO object.</returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel != null)
            {
                var hotelRoomEntity = new HotelRoom()
                {
                    HotelID = hotelId,
                    RoomID = hotelRoom.RoomID,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    PetFreindly = hotelRoom.PetFriendly
                };
                await _context.HotelRoom.AddAsync(hotelRoomEntity);
                await _context.SaveChangesAsync();
                return hotelRoom;
            }
            return null;
        }

        /// <summary>
        /// Deletes an existing hotel room.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="roomNumber">The room number of the hotel room to delete.</param>
        public async Task Delete(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRoom
                .FirstOrDefaultAsync(h => h.HotelID == hotelId && h.RoomNumber == roomNumber);

            if (hotelRoom != null)
            {
                _context.HotelRoom.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a specific hotel room by its hotel ID and room number.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="roomNumber">The room number of the hotel room to retrieve.</param>
        /// <returns>The HotelRoomDTO object representing the retrieved hotel room.</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            var room = await _context.HotelRoom
                .Select(h => new HotelRoomDTO
                {
                    HotelID = h.HotelID,
                    RoomNumber = h.RoomNumber,
                    Rate = h.Rate,
                    PetFriendly = h.PetFreindly,
                    RoomID = h.RoomID,
                    Room = new RoomDTO
                    {
                        ID = h.Room.ID,
                        Name = h.Room.Name,
                        Layout = h.Room.Layout,
                        Amenities = h.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenity.ID,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync(h => h.HotelID == hotelId && h.RoomNumber == roomNumber);

            return room;
        }

        /// <summary>
        /// Gets a list of hotel rooms belonging to a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel for which to retrieve the rooms.</param>
        /// <returns>A list of HotelRoomDTO objects representing the hotel rooms.</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            var rooms = await _context.HotelRoom
                .Select(h => new HotelRoomDTO
                {
                    HotelID = h.HotelID,
                    RoomNumber = h.RoomNumber,
                    Rate = h.Rate,
                    PetFriendly = h.PetFreindly,
                    RoomID = h.RoomID,
                    Room = new RoomDTO
                    {
                        ID = h.Room.ID,
                        Name = h.Room.Name,
                        Layout = h.Room.Layout,
                        Amenities = h.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenity.ID,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                })
                .Where(h => h.HotelID == hotelId)
                .ToListAsync();

            return rooms;
        }

        /// <summary>
        /// Updates an existing hotel room.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="roomNumber">The room number of the hotel room to update.</param>
        /// <param name="hotelRoom">The updated HotelRoomDTO object containing hotel room information.</param>
        /// <returns>The updated HotelRoomDTO object.</returns>
        public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoom)
        {
            var hotelRoomToUpdate = await _context.HotelRoom
                .FirstOrDefaultAsync(h => h.HotelID == hotelId && h.RoomNumber == roomNumber);

            if (hotelRoomToUpdate == null)
            {
                return null;
            }

            hotelRoomToUpdate.Rate = hotelRoom.Rate;
            hotelRoomToUpdate.PetFreindly = hotelRoom.PetFriendly;
            hotelRoomToUpdate.RoomID = hotelRoom.RoomID;

            _context.Entry(hotelRoomToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }
    }
}
