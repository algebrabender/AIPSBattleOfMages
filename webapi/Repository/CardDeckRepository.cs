using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DataLayer;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class CardDeckRepository : BaseRepository<CardDeck>, ICardDeckRepository
    {
        public CardDeckRepository(BOMContext context) : base(context)
        {
        }

        public void AddCardToDeck(Card card, Deck deck)
        {

            CardDeck cd = new CardDeck();
            cd.CardID = card.ID;
            cd.Card = card;
            cd.DeckID = deck.ID;
            cd.Deck = deck;
            if (deck.Cards != null)
                cd.NumberInDeck = deck.Cards.Count;
            else
                cd.NumberInDeck = 0;

            this.Create(cd);

        }

        public override void Create(CardDeck entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public void DeleteCardFromDeck(Card card, Deck deck)
        {
            var cd = this.dbSet.FirstOrDefault(cd => cd.CardID == card.ID && cd.DeckID == deck.ID);
            if (cd != null)
            {
                this.dbSet.Remove(cd);
                deck.Cards.Remove(cd);
            }
        }

        public override Task<IEnumerable<CardDeck>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<CardDeck> GetById(int id)
        {
            return base.GetById(id);
        }

        public override void Update(CardDeck entity)
        {
            base.Update(entity);
        }
    }
}