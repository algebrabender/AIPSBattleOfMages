using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;
using webapi.Interfaces.ServiceInterfaces;
using webapi.Communication;
using Microsoft.AspNetCore.SignalR;

namespace webapi.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;
        private HubService hubService;

        private struct TurnStruct
        {
            public int PlayedByUserID { set; get; }
            public Card Card { set; get; }
            public PlayerState AttackedUser { set; get; }
            public int DamageDone { set; get; }
            public int NextPlayerID { set; get; }
        }

        public GameService(IUnitOfWork unitOfWork, IHubContext<MessageHub> hub) 
        {
            this.unitOfWork = unitOfWork;
            this.hubService = new HubService(hub);
        }

        public async Task<Game> CreateGame(Game game, string terrainType, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards)
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
                deck.NumberOfCards = 30;

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();

                //TODO: izmeniti hardkodiranje
                Card card1 = await this.unitOfWork.CardRepository.GetById(1);
                Deck deck1 = await this.unitOfWork.DeckRepository.GetDeckWithCards(deck.ID);
                this.unitOfWork.CardDeckRepository.AddCardToDeck(card1, deck1);

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

                playerState.ManaPoints = 5;
                playerState.HealthPoints = 10;
                
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
        public async Task<Game> AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards)
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
                deck.NumberOfCards = 30; //ovo ce trebati da se salje kao parametar

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();

                //TODO: izmeniti hardkodiranje
                Card card1 = await this.unitOfWork.CardRepository.GetById(1005);
                Deck deck1 = await this.unitOfWork.DeckRepository.GetDeckWithCards(deck.ID);
                this.unitOfWork.CardDeckRepository.AddCardToDeck(card1, deck1);

                Card card2 = await this.unitOfWork.CardRepository.GetById(1006);
                this.unitOfWork.CardDeckRepository.AddCardToDeck(card2, deck1);

                Mage mage = await this.unitOfWork.MageRepository.GetMageByType(mageType);
                PlayerState playerState = new PlayerState();
                playerState.Mage = mage;
                playerState.MageID = mage.ID;
                playerState.GameID = gameID;
                playerState.UserID = userID;
                playerState.User = user;
                playerState.DeckID = deck.ID;
                playerState.Deck = deck;

                playerState.ManaPoints = 5;
                playerState.HealthPoints = 10;
                
                unitOfWork.PlayerStateRepository.Create(playerState);

                game.PlayerStates.Add(playerState);
                mage.PlayerStates.Add(playerState);
                deck.PlayerState = playerState;
                unitOfWork.GameRepository.Update(game);
                unitOfWork.MageRepository.Update(mage);
                unitOfWork.DeckRepository.Update(deck);

                await unitOfWork.CompleteAsync();

                await hubService.NotifyOnGameChanges(gameID, "AddUserToGame", user);

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

                await hubService.NotifyOnGameChanges(gameID, "RemoveUserFromGame", user);

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

                await hubService.NotifyOnGameChanges(gameID, "SetWinner", game);

                return game;
            }
        }
        public async Task<Game> Turn(int gameID, int turnUserID, int manaSpent, int attackedUserID, int damageDone, int nextUserID, int cardID)
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

                Card card = await unitOfWork.CardRepository.GetById(cardID);
                Deck deck = await unitOfWork.DeckRepository.GetDeckWithCards(umgTurnuser.DeckID);
                unitOfWork.CardDeckRepository.DeleteCardFromDeck(card, deck);               

                await unitOfWork.CompleteAsync();

                TurnStruct turn = new TurnStruct();
                turn.PlayedByUserID = turnUserID;
                turn.AttackedUser = umgAttackedUser;
                turn.DamageDone = damageDone;
                turn.NextPlayerID = game.WhoseTurnID;
                turn.Card = card;
                string turnMessage = "Played By UserID: " + turnUserID 
                                    + "\nAttacked UserID:" + umgAttackedUser.ID
                                    + ", Damage Done:" + damageDone
                                    + ", New Health Points: " + umgAttackedUser.HealthPoints
                                    + "\nNext Player UserID: " + nextUserID;
                
                //await hubService.NotifyOnGameChanges(gameID, "Turn", turn);
                await hubService.NotifyOnGameChanges(gameID, "Turn", turnMessage);
                
                return game;
            }
        }
        public async Task<bool> SendInvite(int gameID, string username, string tag, int userFrom)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetUserByUsernameAndTag(username, tag);
                User uFrom = await unitOfWork.UserRepository.GetById(userFrom);

                if (user == null)
                    return false;

                await hubService.NotifyUser(user.ID, "ReceivedInvite", new { UserFrom = uFrom.Username + "#" + uFrom.Tag, GameID = gameID});

                return true;
            }
        }
    }
}