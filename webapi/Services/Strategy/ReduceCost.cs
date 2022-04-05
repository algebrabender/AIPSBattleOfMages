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
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, turnUserID);
            CardDeck cardDeck = await unitOfWork.CardDeckRepository.GetByDeckIDAndCardID(user.DeckID, attackedUserID);

            cardDeck.ManaReducer = damageDone;

            unitOfWork.CardDeckRepository.Update(cardDeck);

            return user;
        }
    }
}
