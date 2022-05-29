using BMS.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BMSContext _context;

        public UserController(BMSContext BMSDbContext, IConfiguration config)
        {
            _context = BMSDbContext;
        }
        [HttpGet("users")]
        public IActionResult GetUsers()
        { 
            var userdatails = _context.tblUser.AsQueryable();
            return Ok(userdatails);
        }
        [HttpPost("signup")]
        public IActionResult SiginUp([FromBody] tblUser userobj)
        {
             if(userobj == null)
            {
                return BadRequest();
            }
            else 
            {
                _context.tblUser.Add(userobj);
                _context.SaveChanges();
                return Ok(new
                    {
                    StatusCode = 200,
                    Message = "User Added Sucessfully",
                    
                });
            }

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] tblUser userobj)
        {
            if(userobj == null)
            {
                return BadRequest();
            }
            else
            {
                var User = _context.tblUser.Where(x =>
                x.Email == userobj.Email
                && x.Password == userobj.Password).FirstOrDefault();
                if (User != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message="Login SUccessfull",
                        UserData= userobj.Email
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode=404,
                        Message = "User Not Found"
                    });
                }
            }
        }
        //private string GeneratToken(string username)
        //{
        //    var tokenhandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
        //    return "";
        //}
    }
}
