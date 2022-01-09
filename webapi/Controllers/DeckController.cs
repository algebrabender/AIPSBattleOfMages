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
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService deckService;

        public DeckController(IDeckService deckService)
        {
            this.deckService = deckService;
        }

        [Route("CreateDeck")]
        [HttpPost]
        public async Task<ActionResult> CreateDeck([FromBody] Deck deck)
        {
            var result = await deckService.CreateDeck(deck);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("GetDeckByID/{deckID}")]
        [HttpGet]
        public async Task<ActionResult> GetDeckByID(int deckID)
        {
            var deck = await deckService.GetDeckByID(deckID);
            if (deck == null)
                return BadRequest(); //ERROR
            else
                return Ok(deck);
        }

        [Route("GetDeckByUserID/{userID}/{gameID}")]
        [HttpGet]
        public async Task<ActionResult> GetDeckByUserID(int userID, int gameID)
        {
            var deck = await deckService.GetDeckByUserID(userID, gameID);

            if(deck != null)
            {
                return Ok(deck);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("AddCard/{deckID}/{cardID}")]
        [HttpPut]
        public async Task<ActionResult> AddCardToDeck(int deckID, int cardID)
        {
            var deck = await this.deckService.AddCardToDeck(cardID, deckID);

            if(deck != null)
            {
                return Ok(deck);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}