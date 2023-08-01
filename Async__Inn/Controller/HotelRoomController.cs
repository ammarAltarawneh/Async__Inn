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
    [Route("api/Hotels")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoomService;

        public HotelRoomController(IHotelRoom hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
        }




        // GET: api/HotelRooms
        [HttpGet]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms([FromRoute] int hotelId)
        {
            if (_hotelRoomService == null)
            {
                return NotFound();
            }

            var hotelRooms = await _hotelRoomService.GetHotelRooms(hotelId);

            return Ok(hotelRooms);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoomsDetails(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom( int hotelId, int roomNumber, HotelRoom hotelRoom)
        {

            var updateHotelRoom = await _hotelRoomService.UpdateHotelRooms(hotelId, roomNumber, hotelRoom);



            return Ok(updateHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom, int hotelId)
        {
            var addedHotelRoom = await _hotelRoomService.Create(hotelRoom, hotelId);
            return Ok(addedHotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            if (_hotelRoomService == null)
            {
                return NotFound();
            }
            await _hotelRoomService.DeleteHotelRooms(hotelId, roomNumber);

            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("byName/{name}")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> getHotelRoomsByName([FromRoute] string name)
        {
            if (_hotelRoomService == null)
            {
                return NotFound();
            }

            var hotelRooms = await _hotelRoomService.GetHotelRoomsByName(name);

            return Ok(hotelRooms);
        }

    }
}
