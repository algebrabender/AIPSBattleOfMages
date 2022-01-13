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
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [Route("CreateGame/{terrainType}/{userID}/{mageType}")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] Game game, string terrainType, int userID, string mageType)
        {
            var result = await gameService.CreateGame(game, terrainType, userID, mageType, 5, 10); //predefinded values
            return Ok(result);
        }
        
        [Route("AddUserToGame/{gameID}/{userID}/{mageType}")]
        [HttpPut]
        public async Task<ActionResult> AddUserToGame(int gameID, int userID, string mageType)
        {
            var result = await gameService.AddUserToGame(gameID, userID, mageType, 5, 10);
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
    
        [Route("SetWinner/{gameID}/{userID}")]
        [HttpPut]
        public async Task<ActionResult> SetWinner(int gameID, int userID)
        {
            var game = await gameService.SetWinnerUserID(gameID, userID);

            return Ok(game);
        }
        
        [Route("Turn/{gameID}/{turnUserID}/{manaSpent}/{attackedUserID}/{damageDone}/{nextUserID}/{cardID}")]
        [HttpPut]
        public async Task<ActionResult> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, int cardID)
        { 
            var game = await gameService.Turn(gameID, turnUserID, manaSpent, attackedUserID, damageDone, nextUserID, cardID);

            return Ok(game);
        }
    }
}