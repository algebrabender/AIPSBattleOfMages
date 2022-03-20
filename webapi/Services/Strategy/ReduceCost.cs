using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class ReduceCost : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public ReduceCost(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<PlayerState> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, Card card)
        {
            throw new System.NotImplementedException();
        }
    }
}
