using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DataLayer;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class DeckRepository : BaseRepository<Deck>, IDeckRepository
    {
        public DeckRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(Deck entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<Deck>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Deck> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(Deck entity)
        {
            base.Update(entity);
        }
    }
}