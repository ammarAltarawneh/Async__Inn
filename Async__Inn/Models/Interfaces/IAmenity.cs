namespace Async__Inn.Models.Interfaces
{
    public interface IAmenity
    {
        // Create
        Task<Amenity> Create(Amenity amenity);

        // Get all
        Task<List<Amenity>> GetAmenities();

        // Get Amenity by ID
        Task<Amenity> GetAmenity(int AmenityID);

        // Update
        Task<Amenity> UpdateAmenity(int ID, Amenity amenity);

        // Delete
        Task Delete(int ID);
    }
}
