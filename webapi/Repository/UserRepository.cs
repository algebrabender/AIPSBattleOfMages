using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DataLayer;
using webapi.DataLayer.Models;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BOMContext context) : base(context)
        {
        }

        public override void Create(User entity)
        {
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<IEnumerable<User>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<User> GetById(int id)
        {
            return base.GetById(id);
        }
        public override void Update(User entity)
        {
            base.Update(entity);
        }
    }
}