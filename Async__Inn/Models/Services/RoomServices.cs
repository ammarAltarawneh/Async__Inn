using Async__Inn.Data;
using Async__Inn.Models.DTO;
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
         
        public async Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId)
		{
			var room = await GetRoom(roomId);
			var amenity = await _context.Amenities.FindAsync(amenityId);
			if (room == null || amenity == null) { return null; }

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

		public async Task<RoomDTO> Create(RoomDTO room)
		{
			var roomToadd = new Room
			{
				Name = room.Name,
				Layout = room.Layout
			};
			await _context.AddAsync(roomToadd);
			await _context.SaveChangesAsync();
			return room;
		}

		public async Task Delete(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room != null)
			{
				_context.Rooms.Remove(room);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<RoomDTO> GetRoom(int id)
		{
			var room = await _context.Rooms.Select(R => new RoomDTO
			{
				ID = R.ID,
				Name = R.Name,
				Layout = R.Layout,
				Amenities = R.RoomAmenities.Select(a => new AmenityDTO
				{
					ID = a.Amenity.ID,
					Name = a.Amenity.Name
				}).ToList()
			})
				.FirstOrDefaultAsync(r => r.ID == id);

			return room;
		}

		public async Task<List<RoomDTO>> GetRooms()
		{
			var rooms = await _context.Rooms.Select(R => new RoomDTO
			{
				ID = R.ID,
				Name = R.Name,
				Layout = R.Layout,
				Amenities = R.RoomAmenities.Select(a => new AmenityDTO
				{
					ID = a.Amenity.ID,
					Name = a.Amenity.Name
				}).ToList()

			}).ToListAsync();
			return rooms;
		}

		public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
		{
			var RoomAminity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
			if (RoomAminity != null)
			{
				_context.RoomAmenities.Remove(RoomAminity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<RoomDTO> UpdateRoom(int id, RoomDTO roomTDO)
		{
			var room = await _context.Rooms.FirstAsync(R => R.ID == id);
			if (room == null) { return null; }

			room.Name = roomTDO.Name;
			room.Layout = roomTDO.Layout;
			_context.Entry(room).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return roomTDO;
		}
    }
}
