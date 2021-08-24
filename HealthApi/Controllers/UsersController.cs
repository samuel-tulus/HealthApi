using HealthApi.Data;
using HealthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        HealthDbContext _healthDbContext;

        public UsersController(HealthDbContext healthDbContext)
        {
            _healthDbContext = healthDbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUsersList()
        {
            var users = _healthDbContext.Users;
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _healthDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _healthDbContext.Users.Add(user);
            _healthDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
