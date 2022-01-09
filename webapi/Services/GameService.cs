using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
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

        public async Task<Game> CreateGame(Game game, string terrainType, int userID, string mageType, int manaPoints, int healthPoints)
        {
            using(unitOfWork)
            {
                Terrain terrain = await unitOfWork.TerrainRepository.GetTerrainByType(terrainType);
                game.Terrain = terrain;

                User user = await unitOfWork.UserRepository.GetById(userID);
                game.CreatedGameUserID = userID;

                game.PlayerStates = new List<PlayerState>();

                unitOfWork.GameRepository.Create(game);

                terrain.Games.Add(game);
                //TODO: deck
                Deck deck = new Deck();
                deck.NumberOfCards = 5; //ovo ce trebati da se salje kao parametar

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();
                
                //TODO: treba da se nadje nacin da se dodaju karte
                //await this.deckService.AddCardToDeck(2, deck.ID);

                int gameID = game.ID;
                Mage mage = await this.unitOfWork.MageRepository.GetMageByType(mageType);
                PlayerState playerState = new PlayerState();
                playerState.GameID = gameID;
                playerState.Mage = mage;
                playerState.MageID = mage.ID;
                playerState.UserID = userID;
                playerState.User = user;
                playerState.DeckID = deck.ID;
                playerState.Deck = deck;

                playerState.ManaPoints = manaPoints;
                playerState.HealthPoints = healthPoints;
                
                unitOfWork.PlayerStateRepository.Create(playerState);
                                
                game.PlayerStates.Add(playerState);
                mage.PlayerStates.Add(playerState);
                deck.PlayerState = playerState;
                unitOfWork.GameRepository.Update(game);
                unitOfWork.MageRepository.Update(mage);
                unitOfWork.DeckRepository.Update(deck);

                await unitOfWork.CompleteAsync();

                return game;
            }
        }
        public async Task<Game> AddUserToGame(int gameID, int userID, string mageType, int manaPoints, int healthPoints)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);
                
                if (game == null)
                    return null;

                User user = await unitOfWork.UserRepository.GetById(userID);

                if (user == null)
                    return null;

                //TODO: deck
                Deck deck = new Deck();
                deck.NumberOfCards = 5; //ovo ce trebati da se salje kao parametar

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();

                //TODO: treba da se nadje nacin da se dodaju karte
                //await this.deckService.AddCardToDeck(2, deck.ID);
                Mage mage = await this.unitOfWork.MageRepository.GetMageByType(mageType);
                PlayerState playerState = new PlayerState();
                playerState.Mage = mage;
                playerState.MageID = mage.ID;
                playerState.GameID = gameID;
                playerState.UserID = userID;
                playerState.User = user;
                playerState.DeckID = deck.ID;
                playerState.Deck = deck;

                playerState.ManaPoints = manaPoints;
                playerState.HealthPoints = healthPoints;
                
                unitOfWork.PlayerStateRepository.Create(playerState);

                game.PlayerStates.Add(playerState);
                mage.PlayerStates.Add(playerState);
                deck.PlayerState = playerState;
                unitOfWork.GameRepository.Update(game);
                unitOfWork.MageRepository.Update(mage);
                unitOfWork.DeckRepository.Update(deck);

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

                unitOfWork.PlayerStateRepository.Delete(ps.GameID, ps.UserID); 

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
                Game game = await unitOfWork.GameRepository.GetGameWithTerrain(gameID);

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