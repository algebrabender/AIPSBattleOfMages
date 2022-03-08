using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IPlayerStateService
    {
        Task<PlayerState> GetPlayerStateByGameID(int gameID);
        Task<List<PlayerState>> GetPlayersInGame(int gameID);
    }
}