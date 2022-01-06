using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DataLayer;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(Card entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<Card>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Card> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(Card entity)
        {
            base.Update(entity);
        }
    }
}