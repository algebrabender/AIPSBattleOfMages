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

        public async Task<PlayerState> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, Card card)
        {
            return await this.strategy.Turn(gameID, turnUserID, manaSpent, attackedUserID, damageDone, nextUserID, card);
        }
    }
}