using Async__Inn.Models.DTO;

namespace Async__Inn.Models.Interfaces
{
    public interface IRoom
    {
        // CREATE
        Task<RoomDTO> Create(RoomDTO room);

        // GET ALL
        Task<List<RoomDTO>> GetRooms();

        // GET ONE BY ID
        Task<RoomDTO> GetRoom(int id);

        // UPDATE
        Task<RoomDTO> UpdateRoom(int id, RoomDTO room);

        // DELETE
        Task Delete(int id);

        // Add Amenity To Room
        Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId);

        // Remove Amentity From Room
        Task RemoveAmenityFromRoom(int roomId, int amenityId);

    }
}
