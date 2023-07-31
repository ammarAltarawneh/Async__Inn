using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Async__Inn.Data;
using Async__Inn.Models;
using Async__Inn.Models.Interfaces;

namespace Async__Inn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _amenity;

        public AmenitiesController(IAmenity amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenities()
        {
          
            return await _amenity.GetAmenities ();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
          
            var amenity = await _amenity.GetAmenity(id);

            

            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/Amenities/{name}")]
        public async Task<IActionResult> PutAmenity(int id, string name)
        {
            var updateAmenity = await _amenity.UpdateAmenity(id, name);

            return Ok(updateAmenity);
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{name}/Amenities")]
        public async Task<ActionResult<Amenity>> PostAmenity(string name)
        {
            var createdAmenity = await _amenity.Create(name);

            return CreatedAtAction("GetAmenity", new { id = createdAmenity.ID }, createdAmenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _amenity.Delete(id);

            return NoContent();
        }        
    }
}
