using webapi.Interfaces.ServiceInterfaces;
using webapi.Interfaces;
using webapi.DataLayer.Models.Cards;
using System.Threading.Tasks;

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
                //TODO: neka provera
                unitOfWork.DeckRepository.Create(deck);
                await unitOfWork.CompleteAsync();

                var d = await unitOfWork.DeckRepository.GetById(deck.ID);

                // CardDeck cardDeck = new CardDeck();
                // cardDeck.DeckID = d.ID;
                // cardDeck.Deck = d;

                //karte ne znam da li se ovde dodaju

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

        public async Task<Deck> GetDeckByUserID(int userID)
        {
            using(unitOfWork)
            {
                Deck deck = await unitOfWork.DeckRepository.GetDeckByUserID(userID);
            
                if(deck != null)
                {
                    //TODO: ako treba nesto
                }
                return deck;
            
            }

        }
    }
}