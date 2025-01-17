using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
           
           if (_context.Users == null)
        {
            return NotFound();  
        }
           return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var user = await _context.Users.FindAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (user == null)
            {
              return NotFound(); // Returns a 404 response
            }
            return user;
        }
    }
}