using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using webapi.DataLayer.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            var result = await userService.CreateUser(user);
            return Ok(result);
        }

        [Route("UserValidating")]
        [HttpPost]
        public async Task<ActionResult> UserValidating([FromBody] User user)
        {
            
            //User u = (User)Context.Users.Where(u => u.Username.Equals(un) && u.Tag == tag).FirstOrDefault();
            User u = await userService.UserValidating(user);

            if (u == null)
                return BadRequest(); //ERROR
            else
                return Ok(u);
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();

            return Ok(users);
        }

        [Route("GetUserByID/{userID}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByID(int userID)
        {
            var user = await userService.GetUserByID(userID);

            if (user == null)
                return BadRequest(); //ERROR
            else
                return Ok(user);
        }

        [Route("GetUserByUsername/{username}")]
        [HttpGet]
        public async Task<ActionResult> GetuserByUsername(string username)
        {
            var user = await userService.GetUserByUsername(username);

            if (user == null)
                return BadRequest(); //ERROR
            else
                return Ok(user);
        }
    }
}