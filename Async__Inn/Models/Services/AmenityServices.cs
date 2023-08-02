using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async__Inn.Models.Services
{
    public class AmenityServices : IAmenity
    {
        private readonly AsyncInnDbContext _context; 

        public AmenityServices(AsyncInnDbContext context)
        { 
            _context = context;
        }


        public async Task<AmenityDTO> Create(AmenityDTO amenityDTO)
        {
            var amenity = new Amenity()
            {
                Name = amenityDTO.Name,
            };
            await _context.Amenities.AddAsync(amenity);

            await _context.SaveChangesAsync();

            return amenityDTO;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                _context.SaveChanges();
            }
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            var amenites = await _context.Amenities.Select(A => new AmenityDTO
            {
                ID = A.ID,
                Name = A.Name,
            }).ToListAsync();
            return amenites;
        }

        public async Task<AmenityDTO> GetAmenity(int id)
        {
            var amenty = await _context.Amenities.Select(A => new AmenityDTO
            {
                ID = A.ID,
                Name = A.Name,
            }).FirstAsync(Am => Am.ID == id);
            return amenty;
        }

        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenityDTO)
        {
            var amenity = await _context.Amenities.FirstAsync(Am => Am.ID == id);
            if (amenity == null) { return null; }
            amenity.Name = amenityDTO.Name;
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenityDTO;
        }


    }
}
