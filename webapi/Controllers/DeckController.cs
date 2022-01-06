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
    public class DeckController : ControllerBase
    {
        private readonly DeckService deckService;

        public DeckController(DeckService deckService)
        {
            this.deckService = deckService;
        }

        [Route("CreateDeck/{numOfCards}")]
        [HttpPost]
        public async Task<ActionResult> CreateDeck(int numOfCards)
        {
            return Ok();
        }

        [Route("GetDeckByID/{deckID}")]
        [HttpGet]
        public async Task<ActionResult> GetDeckByID(int cardID)
        {
            return Ok();
        }

        [Route("GetDeckByUserID/{userID}")]
        [HttpGet]
        public async Task<ActionResult> GetDeckByUserID(int userID)
        {
            return Ok();
        }
    }
}