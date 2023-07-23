namespace Async__Inn.Models.Interfaces
{
    public interface IRoom
    {
        // Create
        Task<Room> Create(Room room);

        // Get all
        Task<List<Room>> GetRooms();

        // Get Amenity by ID
        Task<Room> GetRoom(int RoomID);

        // Update
        Task<Room> UpdateRoom(int ID, Room room);

        // Delete
        Task Delete(int ID);
    }
}
