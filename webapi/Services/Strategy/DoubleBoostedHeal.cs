using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class DoubleBoostedHeal : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public DoubleBoostedHeal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, Card card)
        {
            using (unitOfWork)
            {
                PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
                user.HealthPoints += card.Damage + 2;

                unitOfWork.PlayerStateRepository.Update(user);
                await unitOfWork.CompleteAsync();

                return user;
            }
        }
    }
}
