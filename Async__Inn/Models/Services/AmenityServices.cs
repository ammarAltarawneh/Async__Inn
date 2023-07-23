﻿using Async__Inn.Data;
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


        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Amenities.Add(amenity);

            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int ID)
        {
            Amenity amenity = await GetAmenity(ID);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetAmenity(int AmenityID)
        {
            Amenity amenity = await _context.Amenities.FindAsync(AmenityID);
            return amenity;
        }

        public async Task<Amenity> UpdateAmenity(int ID, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
