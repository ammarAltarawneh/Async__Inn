namespace Async__Inn.Models.Interfaces
{
    public interface IHotel
    {
        // Create
        Task<Hotel> Create(Hotel hotel);

        // Get all
        Task<List<Hotel>> GetHotels();

        // Get Amenity by ID
        Task<Hotel> GetHotel(int HotelID);

        // Update
        Task<Hotel> UpdateHotel(int ID, Hotel hotel);

        // Delete
        Task Delete(int ID);
    }
}
