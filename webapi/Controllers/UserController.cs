using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataLayer.Models;
using Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region HelpAttributes
        private const string alphaUppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string alphaLowercase = "abcdefghijklmnopqrstuvwxyz";
        private Random random;
        #endregion

        #region HelpMethodes
        private char GetRandomUppercase()
        {
            return alphaUppercase[random.Next(0, 26)];
        }
        private char GetRandomLowercase()
        {
            return alphaLowercase[random.Next(0, 26)];
        }
        private string SALTGenerator()
        {
            string salt = "";

            for (int i =0; i < 12; i++) //neka za sad bude po 11 random karaktera
            {
                if(i%3 == 0)
                    salt += GetRandomUppercase();
                else if (i%3 == 1)
                    salt += GetRandomLowercase();
                else
                    salt += random.Next(0, 10).ToString();
            }
            
            return salt;
        }
        private string PasswordHashing(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                var hashedPassword = new System.Text.StringBuilder();
                var bytePassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                foreach (var b in bytePassword)
                {
                    hashedPassword.Append(b.ToString("x2"));
                }

                return hashedPassword.ToString();
            }
        }
        #endregion
        private UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserController user)
        {
            user.SALTGenerator = this.SALTGenerator();
            string passAndSalt = user.Password + user.Salt;
            user.Password = this.PasswordHashing(passAndSalt);

            int minNum = 0;
            int maxNum = 9999;

            String generatedTag = random.Next(minNum, maxNum).ToString("D4");
            
            while (this.usedTags.Contains(generatedTag))
            {
                //TODO: CONTEXT -> SERVICE
                var sameTagUser = (User)Context.Users.Where(u => u.Tag == generatedTag);
                
                if(sameTagUser.Username == user.Username)
                    generatedTag = random.Next(minNum, maxNum).ToString("D4");
            }

            usedTags.Add(generatedTag);
            
            user.Tag = generatedTag;

            var result = await userService.CreateUser(user);
            
            user.Password = null;
            user.Salt = null;
            return Ok(result);
        }

        [Route("UserValidating")]
        [HttpPost]
        public async Task<ActionResult> UserValidating([FromBody] User user)
        {
            //TODO: CONTEXT -> SERVICE
            string un = user.Username.Substring(0, user.Username.Length-5); //samo username
            string tag = user.Username.Substring(user.Username.Length-4); //samo tag
            User u = (User)Context.Users.Where(u => u.Username.Equals(un) && u.Tag == tag).FirstOrDefault();
            if(u == null)
            {
                return BadRequest(); //ERROR             
            }

            string passAndSalt = user.Password + u.Salt;
            string hashedPassword = this.PasswordHashing(passAndSalt);

            if (hashedPassword == u.Password)
            {
                u.Password = null;
                u.Salt = null;
                return Ok(u);
            }
            else
                return BadRequest();//ERROR
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();

            foreach (var u in users)
            {
                u.Password = null;
                u.Salt = null;
            }

            return Ok(users);
        }

        [Route("GetUserByID/{userID}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByID(int userID)
        {
            var user = await userService.GetUserByID(userID);

            if (user != null)
            {
                user.Password = null;
                user.Salt = null;

                return Ok(user);
            }
            else
                return BadRequest(); //ERROR
        }

        [Route("GetUserByUsername/{username}")]
        [HttpGet]
        public async Task<ActionResult> GetuserByUsername(string username)
        {
            var user = await userService.GetuserByUsername(username);

            if (user != null)
            {
                user.Password = null;
                user.Salt = null;

                return Ok(user);
            }
            else
                return BadRequest(); //ERROR
        }
    }
}