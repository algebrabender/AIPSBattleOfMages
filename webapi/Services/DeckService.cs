using webapi.Interfaces.ServiceInterfaces;
using webapi.Interfaces;
using webapi.DataLayer.Models.Cards;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Services
{
    public class DeckService : IDeckService
    {

        private readonly IUnitOfWork unitOfWork;

        public DeckService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Deck> CreateDeck(Deck deck)
        {
            using(unitOfWork)
            {
                //TODO: nesto za player state 
                // var u = await this.unitOfWork.UserRepository.GetById(deck.UserID);
                // if(u == null)
                //     return null;
                unitOfWork.DeckRepository.Create(deck);
                await unitOfWork.CompleteAsync();

                return deck;
            }
        }

        public async Task<Deck> GetDeckByID(int ID)
        {
            using(unitOfWork)
            {
                Deck deck = await unitOfWork.DeckRepository.GetById(ID);

                if(deck != null)
                {
                    //TODO: ako treba nesto
                }

                return deck;
            }
        }
        public async Task<Deck> GetDeckByUserID(int userID, int gameID)
        {
            using(unitOfWork)
            {
                Deck deck = await unitOfWork.PlayerStateRepository.GetUserDeck(userID, gameID);
            
                if(deck != null)
                {
                    //TODO: ako treba nesto
                }
                return deck;
            
            }

        }

        public async Task<Deck> AddCardToDeck(Card card, int deckID)
        {
            using (unitOfWork)
            {
                Deck deck = await unitOfWork.DeckRepository.GetDeckWithCards(deckID);

         
                unitOfWork.CardDeckRepository.AddCardToDeck(card, deck);

                //deck.Cards.Add(cd);
                //unitOfWork.DeckRepository.Update(deck);

                await unitOfWork.CompleteAsync();

                return deck;
            }
        }

        public async Task<Deck> AddCardToDeck(int cardID, int deckID)
        {
            using (unitOfWork)
            {
                Card card = await this.unitOfWork.CardRepository.GetById(cardID);
                if(card == null)
                    return null;
                return await this.AddCardToDeck(card, deckID);
            }
        }
    }
}