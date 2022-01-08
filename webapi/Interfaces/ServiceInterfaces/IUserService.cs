using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> UserValidating(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByID(int userID);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByTag(string tag);
        Task<string> GetUserMageType(int userID, int gameID);
        //Task<int> GetUserGameID(int userID);
    }
}