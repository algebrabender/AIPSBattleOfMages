using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class AddDamage : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public AddDamage(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int attackedUserID, int damageDone, int nextUserID, int cardID)
        {
            PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, turnUserID);
            CardDeck cardDeck = await unitOfWork.CardDeckRepository.GetByDeckIDAndCardID(user.DeckID, attackedUserID);

            cardDeck.DamageBooster = damageDone;

            unitOfWork.CardDeckRepository.Update(cardDeck);

            return user;
        }
    }
}
