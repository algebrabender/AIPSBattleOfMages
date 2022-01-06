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
    public class TerrainController : ControllerBase
    {
        private readonly TerrainService terrainService;

        public TerrainController(TerrainService terrainService)
        {
            this.terrainService = terrainService;
        }

        [Route("GetTerrainByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetTerrainByType(string type)
        {
            return Ok();
        }

        [Route("GetTerrainByID/{terrainID}")]
        [HttpGet]
        public async Task<ActionResult> GetTerrainByID(int terrainID)
        {
            return Ok();
        }

        [Route("GetTerrainByGameID/{gameID}")]
        [HttpGet]
        public async Task<ActionResult> GetTerrainByGameID(int gameID)
        {
            return Ok();
        }
    }
}