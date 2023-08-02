using Async__Inn.Models.DTO;

namespace Async__Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int HotelId);

		// GET ALL
		Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

		// GET ONE BY ID
		Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);

		// UPDATE
		Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

		// DELETE
		Task Delete(int hotelId, int roomNumber);
    }
}
