using Async__Inn.Models.DTO;

namespace Async__Inn.Models.Interfaces
{
    public interface IHotel
    {
        // CREATE
        Task<Hotel> Create(Hotel hotel);

        // GET ALL
        Task<List<HotelDTO>> GetHotels();

        // GET ONE BY ID
        Task<HotelDTO> GetHotel(int id);

        // UPDATE
        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO);

        // DELETE
        Task Delete(int id);

    }
}
