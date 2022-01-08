using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IUserMageGameRepository : IBaseRepository<UserMageGame>
    {
        Task<UserMageGame> GetByGameIDAndUserID(int gameID, int userID);
    }
}