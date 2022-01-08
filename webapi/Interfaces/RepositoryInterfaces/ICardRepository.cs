using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<List<Card>> GetCardsByTitle(string title);
        Task<List<Card>> GetCardsByDamage(int damage);
        Task<List<Card>> GetCardsByManaCost(int manaCost);
        Task<List<Card>> GetCardsByType(string type);



    }
}