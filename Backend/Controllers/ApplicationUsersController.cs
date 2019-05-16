using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationUsers
        [HttpGet]
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/ApplicationUsers/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.Users.FindAsync(id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // POST: api/ApplicationUsers
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Users.SingleOrDefault(k => k.Email == applicationUser.Email) == null)
            {
                applicationUser.Password = BCrypt.Net.BCrypt.HashPassword(applicationUser.Password, SaltRevision.Revision2);
                _context.Users.Add(applicationUser);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetApplicationUser", new { id = applicationUser.UserId }, applicationUser);
            }
            return StatusCode(500, "Account already exist");
        }

        // PUT: api/ApplicationUsers/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationUser([FromBody] ApplicationUser applicationUser, [FromRoute] int id)
        {
            if (ModelState.IsValid && ApplicationUserExists(id))
            {
                var user = await _context.Users.FindAsync(id);
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.BirthDate = applicationUser.BirthDate;
                user.Phone = applicationUser.Phone;
                user.Car = applicationUser.Car;
                user.Description = applicationUser.Description;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest(ModelState);

        }

        [HttpPost("addimg/{id}")]
        public async Task<IActionResult> PostImage([FromForm(Name = "file")] IFormFile file, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    user.Photo = memoryStream.ToArray();
                }
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet("getimg/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if(ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                return File(user.Photo, "image/jpg");
            }
            return BadRequest(ModelState);

        }

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return Ok(applicationUser);
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}