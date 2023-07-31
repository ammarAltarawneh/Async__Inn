using Async__Inn.Data;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Models.Services
{
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }


        public async Task<Hotel> Create(string name, string streatAdress, string city, string state, string country, string phone)
        {
            //_context.Hotels.Add(hotel);
            Hotel hotel = new Hotel {Name = name,StreetAdress = streatAdress, City=city, State = state,Country=country,Phone=phone };
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int ID)
        {
            Hotel hotel = await GetHotel(ID);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels
                .Include(h=>h.HotelRoom).ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotel(int HotelID)
        {
            Hotel hotel = await _context.Hotels.FindAsync(HotelID);
            return hotel;
        }

        public async Task<Hotel> UpdateHotel(int ID, string name, string streatAdress, string city, string state, string country, string phone)
        {
            //_context.Entry(hotel).State = EntityState.Modified;
            Hotel hotel = new Hotel { Name = name, StreetAdress = streatAdress, City = city, State = state, Country = country, Phone = phone };
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        
    }
}
