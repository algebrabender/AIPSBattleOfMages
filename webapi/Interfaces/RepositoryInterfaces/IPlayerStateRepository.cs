using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;
using webapi.DataLayer.Models.Cards;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IPlayerStateRepository : IBaseRepository<PlayerState>
    {
        Task<PlayerState> GetByGameIDAndUserID(int gameID, int userID);
        Task<PlayerState> GetByGameID(int gameID);

        Task<PlayerState> GetWithUserData(int gameID, int userID);
        Task<string> GetUserMageType(int userID, int gameID);
        Task<Deck> GetUserDeck(int userID, int gameID);
        void Delete(int gameID, int userID);
    }
}