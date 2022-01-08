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

        public override void Create(CardDeck entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
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