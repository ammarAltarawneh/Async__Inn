﻿using Async__Inn.Models.DTO;

namespace Async__Inn.Models.Interfaces
{
    public interface IAmenity
    {
        // CREATE
        Task<AmenityDTO> Create(AmenityDTO amenity);

        // GET ALL
        Task<List<AmenityDTO>> GetAmenities();

        // GET ONE BY ID
        Task<AmenityDTO> GetAmenity(int id);

        // UPDATE
        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);

        // DELETE
        Task Delete(int id);
    }
}
