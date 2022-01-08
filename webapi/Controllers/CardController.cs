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

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly CardService cardService;

        public CardController(CardService cardService)
        {
            this.cardService = cardService;
        }

        [Route("GetAllCards")]
        [HttpGet]
        public async Task<ActionResult> GetAllCards()
        {
            var cards = await this.cardService.GetAllCards();
            return Ok(cards);
        }

        [Route("GetCardsByDeckID/{deckID}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByDeckID(int deckID)
        {
            var cards = await this.cardService.GetCardsByDeckID(deckID);
            if(cards != null)
            {
                return Ok(cards);
            }
            return BadRequest();
        }

        [Route("GetCardsByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByType(string type)
        {
            var cards = await this.cardService.GetCardsByType(type);
            return Ok(cards);
        }

        [Route("GetCardsByManaCost/{manaCost}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByManaCost(int manaCost)
        {
            var cards = await this.cardService.GetCardsByManaCost(manaCost);
            return Ok(cards);
        }

        [Route("GetCardsByDamage/{damage}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByDamage(int damage)
        {
            var cards = await this.cardService.GetCardsByDamage(damage);
            return Ok(cards);
        }

        [Route("GetCardByID/{cardID}")]
        [HttpGet]
        public async Task<ActionResult> GetCardByID(int cardID)
        {
            var card = await this.cardService.GetCardByID(cardID);
            if(card != null)
            {
                return Ok(card);
            }
            return BadRequest();
        }

        [Route("GetCardsByTitle/{title}")]
        [HttpGet]
        public async Task<ActionResult> GetCardByTitle(string title)
        {
            var card = await this.cardService.GetCardsByTitle(title);
            return Ok(card);
        }

        [Route("CreateCard")]
        [HttpPost]
        public async Task<ActionResult> CreateCard([FromBody] Card card)
        {
            var r = await cardService.CreateCard(card);
            if(r != null)
            {
                return Ok(r);
            }
            return BadRequest();
        }
    }
}