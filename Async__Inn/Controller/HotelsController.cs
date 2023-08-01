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
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _hotel.GetHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _hotel.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        //PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int ID, Hotel hotel)
        {


            var updateHotel = await _hotel.UpdateHotel(ID, hotel);
            return Ok(updateHotel);


        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{name}/Hotels/{streatAdress}/{city}/{state}/{country}/{phone}")]
        public async Task<ActionResult<Hotel>> PostHotel(string name, string streatAdress, string city, string state, string country, string phone)
        {
            var createdHotel = await _hotel.Create(name, streatAdress,city,state,country,phone);

            return CreatedAtAction("GetHotel", new { id = createdHotel.ID }, createdHotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotel.Delete(id);

            return NoContent();
        }

        
    }
}
