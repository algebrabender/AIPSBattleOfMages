using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class BoostedHeal : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public BoostedHeal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
            user.HealthPoints += damageDone + 1;

            unitOfWork.PlayerStateRepository.Update(user);

            return user;
        }
    }
}
