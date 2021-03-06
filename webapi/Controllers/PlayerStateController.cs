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
    public class PlayerStateController : ControllerBase
    {
        private readonly IPlayerStateService playerStateService;

        public PlayerStateController(IPlayerStateService playerStateService)
        {
            this.playerStateService = playerStateService;
        }

        [Route("GetPlayerStateForGame/{gameID}/{userID}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerStateForGame(int gameID, int userID)
        {
            var ps = await playerStateService.GetPlayerStateByGameID(gameID, userID);

            return Ok(ps);
        }

        [Route("GetPlayerStateWithUserData/{gameID}/{userID}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerStateWithUserData(int gameID, int userID)
        {
            var ps = await playerStateService.GetPlayerStateWithUserData(gameID, userID);

            return Ok(ps);
        }


        [Route("GetPlayersInGame/{gameID}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayersInGame(int gameID)
        {
            var ps = await playerStateService.GetPlayersInGame(gameID);

            return Ok(ps);
        }
    }
}