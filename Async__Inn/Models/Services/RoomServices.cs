using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Async__Inn.Models.Services
{
    /// <summary>
    /// Implementation of the IRoom interface to manage room-related operations.
    /// </summary>
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _context;

        /// <summary>
        /// Initializes a new instance of the RoomServices class.
        /// </summary>
        /// <param name="context">The database context for AsyncInn.</param>
        public RoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an amenity to a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to which the amenity will be added.</param>
        /// <param name="amenityId">The ID of the amenity to add to the room.</param>
        /// <returns>The RoomDTO object representing the updated room.</returns>
        public async Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId)
        {
            var room = await GetRoom(roomId);
            var amenity = await _context.Amenities.FindAsync(amenityId);
            if (room == null || amenity == null)
            {
                return null;
            }

            await _context.RoomAmenities.AddAsync(new RoomAmenities { RoomID = roomId, AmenitiesID = amenityId });
            await _context.SaveChangesAsync();

            var roomDTO = new RoomDTO
            {
                ID = roomId,
                Name = room.Name,
                Layout = room.Layout
            };
            return roomDTO;
        }

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The RoomDTO object containing room information.</param>
        /// <returns>The created RoomDTO object.</returns>
        public async Task<RoomDTO> Create(RoomDTO room)
        {
            var roomToAdd = new Room
            {
                Name = room.Name,
                Layout = room.Layout
            };
            await _context.AddAsync(roomToAdd);
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Deletes an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        public async Task Delete(int id)
        {
            RoomDTO room = await GetRoom(id);
            _context.Entry<RoomDTO>(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>The RoomDTO object representing the retrieved room.</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.Select(r => new RoomDTO
            {
                ID = r.ID,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    ID = a.Amenity.ID,
                    Name = a.Amenity.Name
                }).ToList()
            })
            .FirstOrDefaultAsync(r => r.ID == id);

            return room;
        }

        /// <summary>
        /// Gets a list of all rooms.
        /// </summary>
        /// <returns>A list of RoomDTO objects representing all rooms.</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.Select(r => new RoomDTO
            {
                ID = r.ID,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    ID = a.Amenity.ID,
                    Name = a.Amenity.Name
                }).ToList()
            }).ToListAsync();

            return rooms;
        }

        /// <summary>
        /// Removes an amenity from a room.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the amenity will be removed.</param>
        /// <param name="amenityId">The ID of the amenity to remove from the room.</param>
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var roomAmenity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
            if (roomAmenity != null)
            {
                _context.RoomAmenities.Remove(roomAmenity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="roomDTO">The updated RoomDTO object containing room information.</param>
        /// <returns>The updated RoomDTO object.</returns>
        public async Task<RoomDTO> UpdateRoom(int id, RoomDTO roomDTO)
        {
            var room = await _context.Rooms.FirstAsync(r => r.ID == id);
            if (room == null)
            {
                return null;
            }

            room.Name = roomDTO.Name;
            room.Layout = roomDTO.Layout;
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return roomDTO;
        }
    }
}
