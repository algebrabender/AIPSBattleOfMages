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
using webapi.DataLayer.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MageController : ControllerBase
    {
        private readonly MageService mageService;

        public MageController(MageService mageService)
        {
            this.mageService = mageService;
        }

        [Route("CreateMage/{userID}")]
        [HttpPost]
        public async Task<ActionResult> CreateMage([FromBody] Mage mage, int userID)
        {
            var result = await mageService.CreateMage(mage, userID);
            return Ok(result);
        }

        [Route("GetMageByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetMageByType(string type)
        {
            var Mage = await mageService.GetMageByType(type);

            if (Mage == null)
                return BadRequest(); //ERROR

            return Ok(Mage);
        }

        [Route("GetMageByID/{MageID}")]
        [HttpGet]
        public async Task<ActionResult> GetMageByID(int MageID)
        {
            var Mage = await mageService.GetMageByID(MageID);

            if (Mage == null)
                return BadRequest(); //ERROR

            return Ok(Mage);
        }
    }
}