using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class MageRepository : BaseRepository<Mage>, IMageRepository
    {
        public MageRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(Mage entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<Mage>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Mage> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(Mage entity)
        {
            base.Update(entity);
        }

        public async Task<Mage> GetMageByType(string type)
        {
            return await this.dbSet.Include(mage => mage.PlayerStates).FirstOrDefaultAsync(mage => mage.Type == type); 
        }
    }
}