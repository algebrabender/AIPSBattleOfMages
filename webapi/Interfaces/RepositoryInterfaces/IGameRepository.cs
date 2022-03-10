using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<Game> GetGameWithTerrain(int gameID);
        Task<Game> GetGameWithPlayerStates(int gameID);
        Task<int> GetRandomGameID();
    }
}