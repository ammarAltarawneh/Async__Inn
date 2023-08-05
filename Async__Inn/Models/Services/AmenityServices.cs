using Async__Inn.Data;
using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Async__Inn.Models.Services
{
    /// <summary>
    /// Implementation of the IAmenity interface to manage amenity-related operations.
    /// </summary>
    public class AmenityServices : IAmenity
    {
        private readonly AsyncInnDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AmenityServices class.
        /// </summary>
        /// <param name="context">The database context for AsyncInn.</param>
        public AmenityServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="amenityDTO">The AmenityDTO object containing amenity information.</param>
        /// <returns>The created AmenityDTO object.</returns>
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

        /// <summary>
        /// Deletes an existing amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>
        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a list of all amenities.
        /// </summary>
        /// <returns>A list of AmenityDTO objects representing all amenities.</returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            var amenities = await _context.Amenities.Select(A => new AmenityDTO
            {
                ID = A.ID,
                Name = A.Name,
            }).ToListAsync();
            return amenities;
        }

        /// <summary>
        /// Gets a specific amenity by its ID.
        /// </summary>
        /// <param name="id">The ID of the amenity to retrieve.</param>
        /// <returns>The AmenityDTO object representing the retrieved amenity.</returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.Select(A => new AmenityDTO
            {
                ID = A.ID,
                Name = A.Name,
            }).FirstOrDefaultAsync(Am => Am.ID == id);
            return amenity;
        }

        /// <summary>
        /// Updates an existing amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="amenityDTO">The updated AmenityDTO object containing amenity information.</param>
        /// <returns>The updated AmenityDTO object.</returns>
        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenityDTO)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(Am => Am.ID == id);
            if (amenity == null) { return null; }
            amenity.Name = amenityDTO.Name;
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenityDTO;
        }
    }
}
