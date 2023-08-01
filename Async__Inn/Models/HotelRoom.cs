using System.ComponentModel.DataAnnotations.Schema;

namespace Async__Inn.Models
{
    public class HotelRoom
    {
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public int RoomID { get; set; }
        public decimal Rate { get; set; }
        public bool PetFreindly { get; set; }

        // Navigation Propabilities
        [ForeignKey("RoomID")]
        public Room? Room { get; set; }

        [ForeignKey("HotelID")]
        public Hotel? Hotel { get; set; }

    }
}
