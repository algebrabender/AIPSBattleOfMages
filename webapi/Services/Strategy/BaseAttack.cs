﻿
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;

namespace webapi.Services.Strategy
{
    public class BaseAttack : ICardStrategy
    {
        private IUnitOfWork unitOfWork;

        public BaseAttack(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PlayerState> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, Card card)
        {
            using(unitOfWork)
            {
                PlayerState attackedUser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
                attackedUser.HealthPoints -= card.Damage;

                unitOfWork.PlayerStateRepository.Update(attackedUser);
                await unitOfWork.CompleteAsync();

                return attackedUser;
            }
        }
    }
}