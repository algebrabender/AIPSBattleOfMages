using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class UserMageGameRepository : BaseRepository<UserMageGame>, IUserMageGameRepository
    {
        public UserMageGameRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(UserMageGame entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<UserMageGame>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<UserMageGame> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(UserMageGame entity)
        {
            base.Update(entity);
        }

        public async Task<UserMageGame> GetByGameIDAndUserID(int gameID, int userID)
        {
            return await this.dbSet.FirstOrDefaultAsync(umg => umg.GameID == gameID && umg.UserID == userID);
        }
    }
}