using System.Threading.Tasks;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IDeckRepository : IBaseRepository<Deck>
    {
        Task<Deck> GetDeckWithCards(int deckID);
    }
}