using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IPlayerStateService
    {
        Task<PlayerState> GetPlayerStateByGameID(int gameID, int userID);
        Task<List<PlayerState>> GetPlayersInGame(int gameID);
        Task<PlayerState> GetPlayerStateWithUserData(int gameID, int userID);
    }
}