using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DataLayer;
using webapi.Interfaces;

namespace webapi.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected BOMContext context;
        protected DbSet<T> dbSet;

        public BaseRepository(BOMContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }
        
        public virtual async void Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Delete(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}