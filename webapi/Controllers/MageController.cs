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
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MageController : ControllerBase
    {
        private readonly IMageService mageService;

        public MageController(IMageService mageService)
        {
            this.mageService = mageService;
        }

        [Route("CreateMage")]
        [HttpPost]
        public async Task<ActionResult> CreateMage([FromBody] Mage mage)
        {
            var result = await mageService.CreateMage(mage);
            return Ok(result);
        }

        [Route("GetMageByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetMageByType(string type)
        {
            var Mage = await mageService.GetMageByType(type);

            if (Mage == null)
                return BadRequest("No mage with selected type"); //ERROR

            return Ok(Mage);
        }

        [Route("GetMageByID/{MageID}")]
        [HttpGet]
        public async Task<ActionResult> GetMageByID(int MageID)
        {
            var Mage = await mageService.GetMageByID(MageID);

            if (Mage == null)
                return BadRequest("No mage with this ID"); //ERROR

            return Ok(Mage);
        }
    }
}