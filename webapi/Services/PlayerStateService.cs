using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using webapi.DataLayer.Models;
using webapi.Interfaces;
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Services
{
    public class PlayerStateService : IPlayerStateService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public PlayerStateService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<PlayerState>> GetPlayersInGame(int gameID)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetGameWithPlayerStates(gameID);
                if(game != null)
                {
                    return game.PlayerStates;
                }

                return null;
            }
        }

        public async Task<PlayerState> GetPlayerStateByGameID(int gameID)
        {
            using (unitOfWork)
            {
                PlayerState ps = await unitOfWork.PlayerStateRepository.GetByGameID(gameID);

                return ps;
            }
        }
    }
}