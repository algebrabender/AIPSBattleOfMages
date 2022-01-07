using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByTag(string tag);
        Task<User> GetUserByUsernameAndTag(string username, string tag);
    }
}