using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class TerrainRepository : BaseRepository<Terrain>, ITerrainRepository
    {
        public TerrainRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(Terrain entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<Terrain>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Terrain> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(Terrain entity)
        {
            base.Update(entity);
        }

        public async Task<Terrain> GetTerrainByType(string type)
        {
            return await this.dbSet.FirstOrDefaultAsync(terrain => terrain.Type == type); 
        }
    }
}