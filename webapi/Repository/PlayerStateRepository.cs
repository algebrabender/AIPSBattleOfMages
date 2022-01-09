using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class PlayerStateRepository : BaseRepository<PlayerState>, IPlayerStateRepository
    {
        public PlayerStateRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(PlayerState entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public void Delete(int gameID, int userID)
        {
            PlayerState entity = this.dbSet.Find(gameID, userID);
            this.dbSet.Remove(entity);
        }

        public override Task<IEnumerable<PlayerState>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<PlayerState> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(PlayerState entity)
        {
            base.Update(entity);
        }

        public async Task<PlayerState> GetByGameIDAndUserID(int gameID, int userID)
        {
            return await this.dbSet.FirstOrDefaultAsync(umg => umg.GameID == gameID && umg.UserID == userID);
        }

        public async Task<string> GetUserMageType(int userID, int gameID)
        {
            var ps = await this.dbSet.Include(ps => ps.Mage)
                                    .FirstOrDefaultAsync(ps => ps.UserID == userID && ps.GameID == gameID);
            if(ps != null)
            {
                return ps.Mage.Type;
            }

            return null;
        }

        public async Task<Deck> GetUserDeck(int userID, int gameID)
        {
            var ps = await this.dbSet.Include(ps => ps.Deck)
                                .FirstOrDefaultAsync(ps => ps.UserID == userID && ps.GameID == gameID);
            if(ps != null)
            {
                return ps.Deck;
            }

            return null;
        }
    }
}