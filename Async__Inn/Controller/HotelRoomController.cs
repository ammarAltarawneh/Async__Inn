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
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoomService;

        public HotelRoomController(IHotelRoom hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
        }




        // GET all the rooms for a hotel: /api/Hotels/{hotelId}/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _hotelRoomService.GetAllHotelRooms(hotelId);
            return Ok(hotelRooms);
        }





        // POST to add a room to a hotel: /api/Hotels/{hotelId}/Rooms
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> AddHotelRoom(int hotelId, HotelRoom hotelRoom)
        {
            hotelRoom.HotelID = hotelId;
            var addedHotelRoom = await _hotelRoomService.AddHotelRoom(hotelRoom);
            return CreatedAtAction(nameof(GetHotelRoom), new { hotelId, roomId = addedHotelRoom.RoomID }, addedHotelRoom);
        }



        // GET all room details for a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int roomId, int hotelId)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoom(roomId, hotelId);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }




        // PUT update the details of a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpPut("{roomNumber}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, HotelRoom hotelRoom)
        {
            if (hotelId != hotelRoom.HotelID || roomId != hotelRoom.RoomID)
            {
                return BadRequest();
            }

            await _hotelRoomService.UpdateHotelRoom(hotelRoom);
            return NoContent();
        }

        // DELETE a specific room from a hotel: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpDelete("{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomId)
        {
            await _hotelRoomService.DeleteHotelRoom(roomId, hotelId);
            return NoContent();

        }
    }
}
