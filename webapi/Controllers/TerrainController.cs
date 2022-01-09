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

        [Route("CreateTerrain")]
        [HttpPost]
        public async Task<ActionResult> CreateTerrain([FromBody] Terrain terrain)
        {
            var result = await terrainService.CreateTerrain(terrain);
            return Ok(result);
        }

        [Route("GetTerrainByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetTerrainByType(string type)
        {
            var terrain = await terrainService.GetTerrainByType(type);

            if (terrain == null)
                return BadRequest("No terrain with selected type"); //ERROR

            return Ok(terrain);
        }

        [Route("GetTerrainByID/{terrainID}")]
        [HttpGet]
        public async Task<ActionResult> GetTerrainByID(int terrainID)
        {
            var terrain = await terrainService.GetTerrainByID(terrainID);

            if (terrain == null)
                return BadRequest("No terrain with this ID"); //ERROR

            return Ok(terrain);
        }
    }
}