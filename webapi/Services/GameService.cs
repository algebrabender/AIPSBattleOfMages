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
using webapi.Services.Strategy;

namespace webapi.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;
        private HubService hubService;
        private ICardContext cardContext;

        private struct TurnStruct
        {
            public int PlayedByUserID { set; get; }
            public Card Card { set; get; }
            public PlayerState AttackedUser { set; get; }
            public int DamageDone { set; get; }
            public int NextPlayerID { set; get; }
        }

        private static void Shuffle (List<Card> cards)
        {
            Random rand = new Random();
            int count = cards.Count;
            while (count > 1)
            {
                count--;
                int j = rand.Next(count + 1);
                Card c = cards[j];
                cards[j] = cards[count];
                cards[count] = c;
            }
        }

        private async Task DeckCreating(Deck deck, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            //TODO: Proveriti sa svim kartama da li je shuffle ok
            //List<Card> deckCards = new List<Card>(30);
            List<Card> attackCards = await this.unitOfWork.CardRepository.GetCardsByType("attack");
            Shuffle(attackCards);
            List<Card> spellCards = await this.unitOfWork.CardRepository.GetCardsByType("heal");
            Shuffle(spellCards);
            List<Card> buffCards = await this.unitOfWork.CardRepository.GetCardsByType("add damage");
            buffCards.AddRange(await this.unitOfWork.CardRepository.GetCardsByType("reduce cost"));
                Shuffle(buffCards);

                int i = 0;
                int max = Math.Max(numOfAttackCards, numOfSpellCards);
                max = Math.Max(max, numOfBuffCards);
                while (i < max)
                {
                    if (i < numOfAttackCards)
                        this.unitOfWork.CardDeckRepository.AddCardToDeck(attackCards[i], deck);
                        //deckCards.Add(attackCards[i]);
                    if (i < numOfSpellCards)
                        this.unitOfWork.CardDeckRepository.AddCardToDeck(spellCards[i], deck);
                        //deckCards.Add(spellCards[i]);
                    if (i < numOfBuffCards)
                        this.unitOfWork.CardDeckRepository.AddCardToDeck(buffCards[i], deck);
                        //deckCards.Add(buffCards[i]);
                    i++;
                }
        }

        public GameService(IUnitOfWork unitOfWork, IHubContext<MessageHub> hub, CardContext cardContext) 
        {
            this.unitOfWork = unitOfWork;
            this.hubService = new HubService(hub);
            this.cardContext = cardContext;
        }

        public async Task<Game> CreateGame(Game game, string terrainType, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
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

                Deck deck = new Deck();
                deck.NumberOfCards = 30;

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();

                await this.DeckCreating(deck, numOfSpellCards, numOfAttackCards, numOfBuffCards);

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
                playerState.TurnOrder = 0;

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
        public async Task<Game> AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            using (unitOfWork)
            {
                Game game = await unitOfWork.GameRepository.GetById(gameID);
                
                if (game == null)
                    return null;

                User user = await unitOfWork.UserRepository.GetById(userID);

                if (user == null)
                    return null;

                Deck deck = new Deck();
                deck.NumberOfCards = 30; 

                unitOfWork.DeckRepository.Create(deck);

                await unitOfWork.CompleteAsync();

                await this.DeckCreating(deck, numOfSpellCards, numOfAttackCards, numOfBuffCards);

                Mage mage = await this.unitOfWork.MageRepository.GetMageByType(mageType);
                PlayerState playerState = new PlayerState();
                playerState.Mage = mage;
                playerState.MageID = mage.ID;
                playerState.GameID = gameID;
                playerState.UserID = userID;
                playerState.User = user;
                playerState.DeckID = deck.ID;
                playerState.Deck = deck;
                playerState.TurnOrder = await this.unitOfWork.PlayerStateRepository.GetCountByGameID(gameID);
                
                playerState.ManaPoints = 5;
                playerState.HealthPoints = 2;
                
                unitOfWork.PlayerStateRepository.Create(playerState);

                game.PlayerStates.Add(playerState);
                mage.PlayerStates.Add(playerState);
                deck.PlayerState = playerState;
                unitOfWork.GameRepository.Update(game);
                unitOfWork.MageRepository.Update(mage);
                unitOfWork.DeckRepository.Update(deck);

                await unitOfWork.CompleteAsync();

                await hubService.NotifyOnPlayersChanges(gameID, "AddUserToGame", user, playerState);

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
                unitOfWork.DeckRepository.Delete(ps.DeckID);

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
        public async Task<Game> Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, int cardID)
        {
            using (unitOfWork)
            {
                Card card = await unitOfWork.CardRepository.GetById(cardID);
                Game game = await unitOfWork.GameRepository.GetGameWithTerrain(gameID);
                PlayerState user = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, turnUserID);
                CardDeck cardDeck = await unitOfWork.CardDeckRepository.GetByDeckIDAndCardID(user.DeckID, cardID);
                var mageType = await unitOfWork.PlayerStateRepository.GetUserMageType(turnUserID, gameID);
                switch (card.Type)
                {
                    case "attack":
                        {
                            if (game.Terrain.Type == "")
                            {
                                if (game.Terrain.Type == mageType)
                                    this.cardContext.SetStrategy(new DoubleBoostedAttack(unitOfWork));
                                else
                                    this.cardContext.SetStrategy(new BoostedAttack(unitOfWork));
                            }
                            else if (mageType == "")
                            {
                                this.cardContext.SetStrategy(new BoostedAttack(unitOfWork));

                            }
                            else
                                this.cardContext.SetStrategy(new BaseAttack(unitOfWork));
                        }
                        break;
                    case "heal":
                        {
                            if (game.Terrain.Type == "")
                            {
                                if (game.Terrain.Type == mageType)
                                    this.cardContext.SetStrategy(new DoubleBoostedHeal(unitOfWork));
                                else
                                    this.cardContext.SetStrategy(new BoostedHeal(unitOfWork));
                            }
                            else if (mageType == "")
                            {
                                this.cardContext.SetStrategy(new BoostedHeal(unitOfWork));

                            }
                            else
                                this.cardContext.SetStrategy(new BaseHeal(unitOfWork));
                        }
                        break;
                    case "add damage":
                        this.cardContext.SetStrategy(new AddDamage(unitOfWork));
                        break;
                    case "reduce cost":
                        this.cardContext.SetStrategy(new ReduceCost(unitOfWork));
                        break;
                }

                PlayerState attackedUser = await this.cardContext.Turn(gameID, turnUserID, attackedUserID, card.Damage + cardDeck.DamageBooster, nextUserID, cardID);

                user.ManaPoints -= (card.ManaCost - cardDeck.ManaReducer);
                unitOfWork.PlayerStateRepository.Update(user);

                game.WhoseTurnID = nextUserID;
                unitOfWork.GameRepository.Update(game);

                Deck deck = await unitOfWork.DeckRepository.GetDeckWithCards(user.DeckID);
                unitOfWork.CardDeckRepository.DeleteCardFromDeck(card, deck);

                if (card.Type == "reduce cost" || card.Type == "add damage")
                {
                    card = new Card { ID = card.ID, Damage = card.Damage, ManaCost = card.ManaCost, Type = card.Type, Description = attackedUserID.ToString() };
                }

                TurnStruct turn = new TurnStruct()
                {
                    PlayedByUserID = turnUserID,
                    DamageDone = card.Damage,
                    NextPlayerID = game.WhoseTurnID,
                    Card = card,
                    AttackedUser = attackedUser
                };

                await hubService.NotifyOnGameChanges(gameID, "Turn", turn);

                if(attackedUser.HealthPoints <= 0)
                {
                    Game gameWithPlayerStates = await unitOfWork.GameRepository.GetGameWithPlayerStates(gameID);
                    for(int i = attackedUser.TurnOrder + 1; i < game.NumOfPlayers; i++)
                    {
                        PlayerState ps = await unitOfWork.PlayerStateRepository.GetById(gameWithPlayerStates.PlayerStates[i].ID);
                        ps.TurnOrder -= 1;
                        unitOfWork.PlayerStateRepository.Update(ps);
                    }

                    game.NumOfPlayers -= 1;
                    

                    unitOfWork.PlayerStateRepository.Delete(gameID, attackedUserID);
                    unitOfWork.DeckRepository.Delete(attackedUser.DeckID);

                    if(game.NumOfPlayers == 1)
                    {
                        unitOfWork.PlayerStateRepository.Delete(gameID, turnUserID);
                        unitOfWork.DeckRepository.Delete(user.DeckID);
                        game.WinnerUserID = turnUserID;
                        await hubService.NotifyUser(turnUserID, "EndGame", "You won!");
                    }
                    unitOfWork.GameRepository.Update(game);

                    await hubService.NotifyUser(attackedUserID, "EndGame", "You lost!");
                    await hubService.NotifyOnPlayersChanges(gameID, "RemoveUserFromGame", user, game);
                }

                await unitOfWork.CompleteAsync();

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

        public async Task<Game> JoinRandomGame(int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
        {
            using (unitOfWork)
            {
                int gameID = await unitOfWork.GameRepository.GetRandomGameID();
                if(gameID > -1)
                    return await this.AddUserToGame(gameID, userID, mageType, numOfSpellCards, numOfAttackCards, numOfBuffCards);
                return null;
                
            }
        }

        public async Task<Game> SkipTurn(int gameID, int turnUserID, int nextUserID)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetById(turnUserID);
                PlayerState psdUser = await unitOfWork.PlayerStateRepository.GetByGameIDAndUserID(gameID, turnUserID);
                psdUser.ManaPoints += 1;                
                unitOfWork.PlayerStateRepository.Update(psdUser);

                Game game = await unitOfWork.GameRepository.GetById(gameID);
                game.WhoseTurnID = nextUserID;   
                unitOfWork.GameRepository.Update(game);

                await unitOfWork.CompleteAsync();

                TurnStruct turn = new TurnStruct();
                turn.PlayedByUserID = turnUserID;
                turn.AttackedUser = null;
                turn.DamageDone = -1;
                turn.NextPlayerID = nextUserID;
                turn.Card = null;
                
                await hubService.NotifyOnGameChanges(gameID, "Turn", turn);

                return game;
            }
        }
    }
}