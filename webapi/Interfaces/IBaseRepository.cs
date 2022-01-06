using System.Collections.Generic;
using System.Threading.Tasks;

namespace webapi.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id); //ili Guid type
        void Create(T entity); //ili return type Task
        void Update(T entity);
        void Delete(int id);

        
    }
}