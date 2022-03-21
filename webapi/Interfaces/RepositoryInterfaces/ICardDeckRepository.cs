using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface ICardDeckRepository : IBaseRepository<CardDeck>
    {
        public void AddCardToDeck(Card card, Deck deck);
        public void DeleteCardFromDeck(Card card, Deck deck);
        public Task<List<CardDeck>> GetCardDecksInDeck(int deckID);
        public Task<CardDeck> GetByDeckIDAndCardID(int deckID, int cardID);
    }
}