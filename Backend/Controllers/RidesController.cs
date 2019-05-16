using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RidesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rides
        [HttpGet]
        public IEnumerable<Ride> GetRides()
        {
            return _context.Rides;
        }

        // GET: api/Rides/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRide([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ride = await _context.Rides.Include(r => r.From).ThenInclude(place => place.City)
                .Include(r => r.To).ThenInclude(place => place.City).FirstOrDefaultAsync(r => r.RideId == id);

            if (ride == null)
            {
                return NotFound();
            }

            return Ok(ride);
        }

        // Get: api/Rides
        [HttpGet("search")]
        public IActionResult FindRide([FromQuery] int from, [FromQuery] int to, [FromQuery] DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rides = _context.Rides.Where(k => k.From.City.CityId == from && k.To.City.CityId == to && k.Date > date)
                .Include(ride => ride.From).ThenInclude(place => place.City)
                .Include(ride => ride.To).ThenInclude(place => place.City)
                .ToList();
            return Ok(rides);
        }


        // POST: api/Rides
        [HttpPost]
        public async Task<IActionResult> PostRide([FromBody] Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fromCity = await _context.Cities.FindAsync(ride.From.City.CityId);
            var toCity = await _context.Cities.FindAsync(ride.To.City.CityId);
            if(toCity != null && fromCity != null)
            {
                ride.From.City = fromCity;
                ride.To.City = toCity;
                _context.Rides.Add(ride);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRide", new { id = ride.RideId }, ride);
            }
            return new StatusCodeResult(500);
            
        }

        // DELETE: api/Rides/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRide([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ride = await _context.Rides.FindAsync(id);
            if (ride == null)
            {
                return NotFound();
            }

            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync();

            return Ok(ride);
        }

        private bool RideExists(int id)
        {
            return _context.Rides.Any(e => e.RideId == id);
        }
    }
}