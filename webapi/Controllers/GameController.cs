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
    public class GameController : ControllerBase
    {
        private readonly GameService gameService;

        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [Route("CreateGame/{userID}")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] Game game, int userID)
        {
            var result = await gameService.CreateGame(game, userID);
            return Ok(result);
        }
        
        [Route("AddUserToGame/{gameID}/{userID}")]
        [HttpPut]
        public async Task<ActionResult> AddUserToGame(int gameID, int userID)
        {
            var result = await gameService.AddUserToGame(gameID, userID);
            return Ok(result);
        }

        [Route("RemoveUserFromGame/{gameID}/{userID}")]
        [HttpPut]
        public async Task<ActionResult> RemoveUserFromGame(int gameID, int userID)
        {
            var result = await gameService.RemoveUserFromGame(gameID, userID);
            return Ok(result);
        }

        [Route("GetAllGames")]
        [HttpGet]
        public async Task<ActionResult> GetAllGames()
        {
            var games = await gameService.GetAllGames();

            return Ok(games);
        }

        [Route("GetGameByID/{gameID}")]
        [HttpGet]
        public async Task<ActionResult> GetGameByID(int gameID)
        {
            var game = await gameService.GetGameByID(gameID);

            if (game == null)
                return BadRequest(); //ERROR

            return Ok(game);
        }

        [Route("GetGameTerrainType/{gameID}")]
        [HttpGet]
        public async Task<ActionResult> GetGameTerrainType(int gameID)
        {
            string terrainType = await gameService.GetGameTerrainType(gameID);

            return Ok(terrainType);
        }
    }
}