using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Card>> GetCardsByDamage(int damage)
        {
            return await this.dbSet.Where(card => card.Damage == damage).ToListAsync();
        }

        public async Task<List<Card>> GetCardsByManaCost(int manaCost)
        {
            return await this.dbSet.Where(card => card.ManaCost == manaCost).ToListAsync();
        }

        public async Task<List<Card>> GetCardsByTitle(string title)
        {
            return await this.dbSet.Where(card => card.Title == title).ToListAsync();
        }

        public async Task<List<Card>> GetCardsByType(string type)
        {
            return await this.dbSet.Where(card => card.Type == type).ToListAsync();
        }

        public override void Update(Card entity)
        {
            base.Update(entity);
        }
    }
}