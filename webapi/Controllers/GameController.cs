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

        [Route("CreateGame/{terrainType}/{userID}/{mageType}/{numOfSpellCards}/{numOfAttackCards}/{numOfBuffCards}")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] Game game, string terrainType, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            var result = await gameService.CreateGame(game, terrainType, userID, mageType, numOfSpellCards, numOfAttackCards, numOfBuffCards);
            return Ok(result);
        }
        
        [Route("AddUserToGame/{gameID}/{userID}/{mageType}/{numOfSpellCards}/{numOfAttackCards}/{numOfBuffCards}")]
        [HttpPost]
        public async Task<ActionResult> AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            var result = await gameService.AddUserToGame(gameID, userID, mageType, numOfSpellCards, numOfAttackCards, numOfBuffCards);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        
        [Route("JoinRandomGame/{userID}/{mageType}/{numOfSpellCards}/{numOfAttackCards}/{numOfBuffCards}")]
        [HttpPost]
        public async Task<ActionResult> JoinRandomGame(int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            var result = await gameService.JoinRandomGame(userID, mageType, numOfSpellCards, numOfAttackCards, numOfBuffCards);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [Route("RemoveUserFromGame/{gameID}/{userID}")]
        [HttpPost]
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
        
        [Route("Turn/{gameID}/{turnUserID}/{attackedUserID}/{nextUserID}/{cardID}")]
        [HttpPost]
        public async Task<ActionResult> Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, int cardID)
        { 
            var game = await gameService.Turn(gameID, turnUserID, attackedUserID, nextUserID, cardID);

            return Ok(game);
        }

        [Route("SendInvite/{gameID}/{username}/{tag}/{userFrom}")]
        [HttpPost]
        public async Task<ActionResult> SendInvite(int gameID, string username, string tag, int userFrom)
        {
            bool invite = await gameService.SendInvite(gameID, username, tag, userFrom);

            if (invite == false)
                return BadRequest();

            return Ok(invite);
        }

        [Route("SkipTurn/{gameID}/{turnUserID}/{nextUserID}")]
        [HttpPost]
        public async Task<ActionResult> SkipTurn(int gameID, int turnUserID, int nextUserID)
        {
            var game = await gameService.SkipTurn(gameID, turnUserID, nextUserID);

            return Ok(game);
        }
    }
}