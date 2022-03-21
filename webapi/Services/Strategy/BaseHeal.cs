
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class BaseHeal : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public BaseHeal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            using (unitOfWork)
            {
                PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
                user.HealthPoints += damageDone;

                unitOfWork.PlayerStateRepository.Update(user);
                await unitOfWork.CompleteAsync();

                return user;
            }
        }
    }
}
