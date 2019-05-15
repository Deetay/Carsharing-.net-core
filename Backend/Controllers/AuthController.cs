using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }

        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; }

        [HttpPost]
        public IActionResult Login([FromBody]ApplicationUser userRequest)
        {
            if(userRequest==null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                var user = _context.Users.SingleOrDefault(k => k.Email == userRequest.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(userRequest.Password, user.Password))
                {
                    return new UnauthorizedResult();
                }
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: Configuration["Auth:Jwt:Issuer"],
                    audience: Configuration["Auth:Jwt:Audience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                Dictionary<String, Object> map = new Dictionary<String, Object>();
                user.Password = ""; //temp workaround for viewmodel
                map.Add("token", tokenString);
                map.Add("user", user);
                return Ok(map);
            }
            catch(Exception ex)
            {
                return new UnauthorizedResult();
            }
        }

    }
}