namespace Async__Inn.Models
{
    public class Room 
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }

        public List<HotelRoom>? HotelRoom { get; set; }
        public List<RoomAmenities>? RoomAmenities { get; set; }

       
    }
}
