using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface ICardService
    {
        Task<Card> GetCardByID(int ID);
        Task<IEnumerable<Card>> GetCardsByDeckID(int deckID);
        Task<IEnumerable<Card>> GetAllCards();
        Task<Card> CreateCard(Card card);
        Task<List<Card>> GetCardsByTitle(string title);
        Task<List<Card>> GetCardsByType(string type);
        Task<List<Card>> GetCardsByManaCost(int manaCost);
        Task<List<Card>> GetCardsByDamage(int damage);
        Task<List<Card>> GetCardsByMagic(string magic);



    }
}