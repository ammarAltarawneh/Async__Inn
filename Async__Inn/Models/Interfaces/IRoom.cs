namespace Async__Inn.Models.Interfaces
{
    public interface IRoom
    {
        // Create
        Task<Room> Create(string name, int layout);

        // Get all
        Task<List<Room>> GetRooms();

        // Get Amenity by ID
        Task<Room> GetRoom(int RoomID);

        // Update
        Task<Room> UpdateRoom(int ID, string name, int layout);

        // Delete
        Task Delete(int ID);

        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
