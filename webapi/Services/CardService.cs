using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces;
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Card> CreateCard(Card card)
        {
            using(unitOfWork)
            {
                unitOfWork.CardRepository.Create(card);
                await unitOfWork.CompleteAsync();

                return card;               
            }    
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            using(unitOfWork)
            {
                return await this.unitOfWork.CardRepository.GetAll();
            }
        }

        public async Task<Card> GetCardByID(int ID)
        {
            using(unitOfWork)
            {
                Card card = await unitOfWork.CardRepository.GetById(ID);

                if(card != null)
                {
                    //TODO
                }
                return card;
            }
        }

        public async Task<List<Card>> GetCardsByDamage(int damage)
        {
            var cards = await this.unitOfWork.CardRepository.GetCardsByDamage(damage);
            return cards;
        }

        public async Task<IEnumerable<Card>> GetCardsByDeckID(int deckID)
        {
            using(unitOfWork)
            {
                List<CardDeck> cd = await unitOfWork.CardDeckRepository.GetCardDecksInDeck(deckID);

                if(cd != null)
                {
                    List<Card> cards = new List<Card>(cd.Count);
                    foreach(var c in cd)
                    {
                        cards.Add(c.Card);
                    }

                    return cards;                
                }

                return null;
            }
        }

        public async Task<List<Card>> GetCardsByManaCost(int manaCost)
        {
            return await this.unitOfWork.CardRepository.GetCardsByManaCost(manaCost);
        }

        public async Task<List<Card>> GetCardsByTitle(string title)
        {
            return await this.unitOfWork.CardRepository.GetCardsByTitle(title);
        }

        public async Task<List<Card>> GetCardsByType(string type)
        {
           return await this.unitOfWork.CardRepository.GetCardsByType(type);
        }

        public async Task<List<Card>> GetCardsByMagic(string magic)
        {
            return await this.unitOfWork.CardRepository.GetCardsByMagic(magic);
        }
    }
}