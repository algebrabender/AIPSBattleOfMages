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
            return Ok();
        }

        [Route("GetCardsByType/{type}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByType(string type)
        {
            return Ok();
        }

        [Route("GetCardsByManaCost/{manacost}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByManaCost(int manaCost)
        {
            return Ok();
        }

        [Route("GetCardsByDamage/{damage}")]
        [HttpGet]
        public async Task<ActionResult> GetCardsByDamage(int damage)
        {
            return Ok();
        }

        [Route("GetCardByID/{cardID}")]
        [HttpGet]
        public async Task<ActionResult> GetCardByID(int cardID)
        {
            return Ok();
        }

        [Route("GetCardsByTitle/{title}")]
        [HttpGet]
        public async Task<ActionResult> GetCardByTitle(string title)
        {
            return Ok();
        }
    }
}