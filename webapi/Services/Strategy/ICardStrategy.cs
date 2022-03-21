using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;

namespace webapi.Services.Strategy
{
    public interface ICardStrategy
    {
        public Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID);
    }
}
