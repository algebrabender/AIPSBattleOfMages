
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class BoostedAttack : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public BoostedAttack(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            PlayerState attackedUser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
            attackedUser.HealthPoints -= damageDone + 1;

            unitOfWork.PlayerStateRepository.Update(attackedUser);

            return attackedUser;
        }
    }
}
