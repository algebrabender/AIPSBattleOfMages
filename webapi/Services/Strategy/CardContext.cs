using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;

namespace webapi.Services.Strategy
{
    public class CardContext : ICardContext
    {
        private ICardStrategy strategy;

        public CardContext() { }

        public void SetStrategy(ICardStrategy cardStrategy)
        {
            this.strategy = cardStrategy;
        }

        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            return await this.strategy.Turn(gameID, turnUserID, attackedUserID, damageDone, nextUserID, cardID);
        }
    }
}