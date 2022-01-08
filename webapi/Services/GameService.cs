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
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Game> CreateGame(Game game, int userID, int mageID, int manaPoints, int healthPoints)
        {
            using(unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetById(userID);

                game.PlayerStates = new List<PlayerState>();

                game.CreatedGameUserID = userID;

                unitOfWork.GameRepository.Create(game);

                int gameID = await unitOfWork.GameRepository.GetGameID(game);

                PlayerState playerState = new PlayerState();
                playerState.GameID = gameID;
                playerState.Mage = await this.unitOfWork.MageRepository.GetById(mageID);
                playerState.MageID = mageID;
                playerState.UserID = userID;
                playerState.User = user;

                //TODO: mozda pravi loop
                game.PlayerStates.Add(playerState);

                playerState.ManaPoints = manaPoints;
                playerState.HealthPoints = healthPoints;
                
                unitOfWork.PlayerStateRepository.Create(playerState);

                await unitOfWork.CompleteAsync();

                return game;
            }
        }
        public async Task<Game> AddUserToGame(int gameID, int userID, int mageID, int manaPoints, int healthPoints)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);
                
                if (game == null)
                    return null;

                User user = await unitOfWork.UserRepository.GetById(userID);

                if (user == null)
                    return null;

                unitOfWork.GameRepository.Update(game);

                //TODO: deck gde se dodaje
                PlayerState playerState = new PlayerState();
                playerState.GameID = gameID;
                playerState.MageID = mageID;
                
                playerState.Mage = await this.unitOfWork.MageRepository.GetById(mageID);
                playerState.UserID = userID;
                playerState.User = user;

                playerState.ManaPoints = manaPoints;
                playerState.HealthPoints = healthPoints;
                
                unitOfWork.PlayerStateRepository.Create(playerState);

                game.PlayerStates.Add(playerState);

                await unitOfWork.CompleteAsync();

                return game;
            }
        }
        public async Task<Game> RemoveUserFromGame(int gameID, int userID)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);
                
                if (game == null)
                    return null;

                User user = await unitOfWork.UserRepository.GetById(userID);

                if (user == null)
                    return null;

                var ps = await this.unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, userID);

                if(ps == null)
                    return null;

                game.PlayerStates.Remove(ps);

                unitOfWork.PlayerStateRepository.Delete(ps.ID); 

                unitOfWork.GameRepository.Update(game);
                
                await unitOfWork.CompleteAsync();

                return game;
            }
        }
        public async Task<IEnumerable<Game>> GetAllGames()
        {
            using (unitOfWork)
            {
                IEnumerable<Game> games = await unitOfWork.GameRepository.GetAll();

                return games;
            }
        }
        public async Task<Game> GetGameByID(int gameID)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);

                return game;
            }
            
        }
        public async Task<string> GetGameTerrainType(int gameID)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);

                return game.Terrain.Type;
            }
        }
        public async Task<Game> SetWinnerUserID(int gameID, int userID)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);
            
                game.WinnerUserID = userID;

                unitOfWork.GameRepository.Update(game);
                await unitOfWork.CompleteAsync();

                return game;
            }
        }
        public async Task<Game> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID)
        {
            using (unitOfWork)
            {
                PlayerState umgTurnuser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, turnUserID);
                umgTurnuser.ManaPoints -= manaSpent;
                unitOfWork.PlayerStateRepository.Update(umgTurnuser);

                PlayerState umgAttackedUser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, attackedUserID);
                umgAttackedUser.HealthPoints -= damageDone;                
                unitOfWork.PlayerStateRepository.Update(umgAttackedUser);

                Game game = await unitOfWork.GameRepository.GetById(gameID);
                game.WhoseTurnID = nextUserID;   
                unitOfWork.GameRepository.Update(game);

                await unitOfWork.CompleteAsync();

                return game;
            }
        }
    }
}