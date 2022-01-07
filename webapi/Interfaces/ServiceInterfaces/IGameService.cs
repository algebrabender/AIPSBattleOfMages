using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IGameService
    {
        Task<Game> CreateGame(Game game, int userID);
        Task<Game> AddUserToGame(int gameID, int userID);
        Task<Game> RemoveUserFromGame(int gameID, int userID);
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameByID(int gameID);
        Task<string> GetGameTerrainType(int gameID);
    }
}