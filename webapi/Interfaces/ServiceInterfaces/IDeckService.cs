using System.Threading.Tasks;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IDeckService
    {
        Task<Deck> CreateDeck(Deck deck);
        Task<Deck> GetDeckByID(int ID);
        Task<Deck> GetDeckByUserID(int userID);
        Task<Deck> AddCardToDeck(Card card, int deckID);
    }
}