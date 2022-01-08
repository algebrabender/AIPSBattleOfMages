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

                game.Users.Add(user);
                game.CreatedGameUserID = userID;

                unitOfWork.GameRepository.Create(game);

                var g = await unitOfWork.GameRepository.GetById(game.ID);
                
                UserMageGame umg = new UserMageGame();
                umg.GameID = g.ID;
                umg.MageID = mageID;
                umg.UserID = userID;
                umg.ManaPoints = manaPoints;
                umg.HealthPoints = healthPoints;
                
                unitOfWork.UserMageGameRepository.Create(umg);

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

                game.Users.Add(user);

                unitOfWork.GameRepository.Update(game);

                UserMageGame umg = new UserMageGame();
                umg.GameID = gameID;
                umg.MageID = mageID;
                umg.UserID = userID;
                umg.ManaPoints = manaPoints;
                umg.HealthPoints = healthPoints;
                
                unitOfWork.UserMageGameRepository.Create(umg);

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

                game.Users.Remove(user);

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
                UserMageGame umgTurnuser = await unitOfWork.UserMageGameRepository.GetByGameIDAndUserID(gameID, turnUserID);
                umgTurnuser.ManaPoints -= manaSpent;
                unitOfWork.UserMageGameRepository.Update(umgTurnuser);

                UserMageGame umgAttackedUser = await unitOfWork.UserMageGameRepository.GetByGameIDAndUserID(gameID, attackedUserID);
                umgAttackedUser.HealthPoints -= damageDone;                
                unitOfWork.UserMageGameRepository.Update(umgAttackedUser);

                Game game = await unitOfWork.GameRepository.GetById(gameID);
                game.WhoseTurnID = nextUserID;   
                unitOfWork.GameRepository.Update(game);

                await unitOfWork.CompleteAsync();

                return game;
            }
        }
    }
}