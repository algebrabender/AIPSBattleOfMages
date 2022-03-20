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
                    List<PlayerState> players = new List<PlayerState>(game.PlayerStates.Count);
                    foreach(var player in game.PlayerStates)
                    {
                        PlayerState ps = await unitOfWork.PlayerStateRepository.GetWithUserData(gameID, player.UserID);

                        players.Add(ps);
                       
                    }

                     return players;
                }

                return null;
            }
        }

        public async Task<PlayerState> GetPlayerStateByGameID(int gameID, int userID)
        {
            using (unitOfWork)
            {
                PlayerState ps = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, userID);

                return ps;
            }
        }

        public async Task<PlayerState> GetPlayerStateWithUserData(int gameID, int userID)
        {
            using (unitOfWork)
            {
                PlayerState ps = await unitOfWork.PlayerStateRepository.GetWithUserData(gameID, userID);

                ps.User.Password = null;
                ps.User.Salt = null;

                return ps;
                
            }
        }
    }
}