using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(Game entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<Game>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Game> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(Game entity)
        {
            base.Update(entity);
        }
        public async Task<Game> GetGameWithTerrain(int gameID)
        {
            var game = await this.dbSet.Include(game => game.Terrain).FirstOrDefaultAsync(game => game.ID == gameID);
            return game;
        }

        public async Task<Game> GetGameWithPlayerStates(int gameID)
        {
            var game = await this.dbSet.Include(game => game.PlayerStates).FirstOrDefaultAsync(game => game.ID == gameID);
            return game; 
        }

        public async Task<int> GetRandomGameID()
        {
            var game = await this.dbSet.FirstOrDefaultAsync(game => game.NumOfPlayers > game.PlayerStates.Count);
            if (game != null)
                return game.ID;

            return -1;
        }
    }
}