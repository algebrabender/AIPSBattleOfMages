
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class DoubleBoostedAttack : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public DoubleBoostedAttack(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            PlayerState attackedUser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
            attackedUser.HealthPoints -= damageDone + 2;

            unitOfWork.PlayerStateRepository.Update(attackedUser);

            return attackedUser;
        }
    }
}
